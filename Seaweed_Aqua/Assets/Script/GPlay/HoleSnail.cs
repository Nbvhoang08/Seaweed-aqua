using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSnail : MonoBehaviour
{
    public GameObject Snail; // Đối tượng screw
    [SerializeField] private Collider2D holeCollider; // Collider của đối tượng Hole
    private bool playerPassed = false; // Cờ để theo dõi trạng thái của player

    void Start()
    {
        // Lấy Collider2D của đối tượng Hole
        holeCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        // Kiểm tra nếu screw không hoạt động thì bật Collider2D của Hole
        if (Snail != null && !Snail.activeInHierarchy)
        {
            holeCollider.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Kiểm tra va chạm với player
        if (other.CompareTag("Player"))
        {
            if (!playerPassed)
            {
                // Lần đầu tiên player đi qua
                playerPassed = true;
            }
            else
            {
                // Player quay lại
                if (Snail != null)
                {
                    Snail.SetActive(true);
                    holeCollider.enabled = false;
                    playerPassed = false; // Reset cờ để có thể kích hoạt lại khi player đi qua lần nữa
                }
            }
        }
    }
}
