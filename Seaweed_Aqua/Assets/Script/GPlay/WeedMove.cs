using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeedMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform targetPosition; // Vị trí mục tiêu theo localPosition
    public float moveSpeed = 10f; // Tốc độ di chuyển
    public float detectionRadius = 0.7f; // Bán kính phát hiện đối tượng "Screw"
    public GameObject effect;
    public GameManager _gameManager;
    public LayerMask snailLayer; 
    public LineCheck lineChecker;
    public bool isMoving = false;
    [SerializeField] private bool varLine = false;
    [SerializeField] Vector2 startPos;
    [SerializeField] private bool hasCollided = false;
    void Start()
    {
        varLine = false;
        startPos = new Vector2(transform.position.x, transform.position.y);
    }



    // Update is called once per frame
    void Update()
    {
        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }


        if (!isMoving)
        {
            // Kiểm tra xem có đối tượng "Screw" trong phạm vi phát hiện không
            Collider2D[] snails = Physics2D.OverlapCircleAll(transform.position, detectionRadius,snailLayer);

            if (snails.Length == 0)
            {
                // Nếu không phát hiện thấy đối tượng "Screw", bắt đầu di chuyển
                isMoving = true;
            }
        }

        if (isMoving)
        {
            // Kiểm tra nếu varLine là true thì quay về startPos
            if (varLine)
            {

                transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);

                // Kiểm tra nếu đã đến vị trí startPos
                if (Vector2.Distance(transform.position, startPos) < 0.01f)
                {
                    // Dừng lại khi đến nơi
                    isMoving = false;
                    varLine = false;
                    hasCollided = false;

                }
            }
            else
            {
                // Di chuyển về vị trí mục tiêu
                Vector2 targetLocalPosition = transform.parent.TransformPoint(targetPosition.localPosition);
                transform.position = Vector2.MoveTowards(transform.position, targetLocalPosition, moveSpeed * Time.deltaTime);

                // Kiểm tra nếu đã đến vị trí mục tiêu
                if (Vector2.Distance(transform.position, targetLocalPosition) < 0.1f)
                {
                    // Dừng lại khi đến nơi
                    isMoving = false;
                    SoundManager.Instance.PlayVFXSound(1);
                    StartCoroutine(DeactivateParentAfterDelay(0.2f));
                }
            }
        }
    }

    private bool IsIntersectingLine(Collider2D line)
    {
        if (line.gameObject.GetComponent<LineRenderer>() != null)
        {
            return lineChecker.intersectingLines.Contains(line.gameObject.GetComponent<LineRenderer>());
        }
        else
        {
            return false;
        }

    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsIntersectingLine(other))
        {
            varLine = true;
            if (!hasCollided && isMoving)
            {
                _gameManager.hp -= 1;
                hasCollided = true;
                SoundManager.Instance.PlayVFXSound(0);
            }

        }
    }

    IEnumerator DeactivateParentAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        transform.parent.gameObject.SetActive(false);
        Instantiate(effect, new Vector3(transform.position.x, transform.position.y, -3), Quaternion.identity);
    }
    // Vẽ bán kính phát hiện trong chế độ Scene để dễ dàng kiểm tra
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

}
