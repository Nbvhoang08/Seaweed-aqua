using UnityEngine;

public class WeedLine : MonoBehaviour
{
   public Transform pointA; // Điểm đầu
    public Transform pointB; // Điểm cuối
    public LineRenderer lineRenderer;
    [SerializeField] private BoxCollider2D boxCollider;
    public LineRenderer spriteRenderer;
    void Start()
    {
        // Thêm thành phần BoxCollider2D nếu chưa có
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            boxCollider = gameObject.AddComponent<BoxCollider2D>();
        }
    }

    void Update()
    {
        if (pointA != null && pointB != null && lineRenderer != null)
        {
            // Lấy vị trí của các điểm và đặt tọa độ Z bằng 0
            Vector3 positionA = new Vector3(pointA.position.x, pointA.position.y, 0);
            Vector3 positionB = new Vector3(pointB.position.x, pointB.position.y, 0);

            // Đặt vị trí cho Line Renderer
            lineRenderer.SetPosition(0, positionA);
            lineRenderer.SetPosition(1, positionB);

            // Lấy độ dày của đường line từ LineRenderer
            float lineWidth = lineRenderer.startWidth;

            // Tính toán vị trí và kích thước cho BoxCollider2D
            float length = Vector3.Distance(positionA, positionB);
            boxCollider.size = new Vector2(length, lineWidth);
            // Tính toán offset cho BoxCollider2D
            Vector3 midPoint = (positionA + positionB) / 2;
            Vector2 offset = boxCollider.transform.InverseTransformPoint(midPoint);
            boxCollider.offset = offset;
        }
    }
    /*void OnDrawGizmos()
    {
        Vector3 positionA = new Vector3(pointA.position.x, pointA.position.y, 0);
        Vector3 positionB = new Vector3(pointB.position.x, pointB.position.y, 0);

        // Đặt vị trí cho Line Renderer
        lineRenderer.SetPosition(0, positionA);
        lineRenderer.SetPosition(1, positionB);
        if (spriteRenderer != null)
        {
            Vector3 spritePosition = spriteRenderer.transform.position;
            Handles.Label(spritePosition, $"Order: {spriteRenderer.sortingOrder}", new GUIStyle
            {
                fontSize = 24,
                normal = new GUIStyleState { textColor = Color.white }
            });
        }
    }*/
}
