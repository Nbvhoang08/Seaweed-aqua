using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rubber;
    public GameObject Lock;
    public float moveSpeed;
    public bool IsArrived;
    private Vector2 initialOffset;
    private float initialZ;

    void Start()
    {
        // Calculate the initial offset between the object and the rubber
        initialOffset = transform.position - rubber.transform.position;
        // Store the initial Z-coordinate
        initialZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (follow())
        {
            // Calculate the target position based on the initial offset
            Vector2 targetPosition = (Vector2)rubber.transform.position + initialOffset;
            Vector3 newPosition = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            // Maintain the initial Z-coordinate
            transform.position = new Vector3(newPosition.x, newPosition.y, initialZ);
            IsArrived = false;
        }
        else
        {
            // Move towards the target position
            Vector3 newPosition = Vector2.MoveTowards(transform.position, Lock.transform.position, moveSpeed * Time.deltaTime);
            // Maintain the initial Z-coordinate
            transform.position = new Vector3(newPosition.x, newPosition.y, initialZ);
            // Check if the object has arrived at the lock position
            if (Vector2.Distance(transform.position, Lock.transform.position) < 0.01f && !IsArrived)
            {
                IsArrived = true;
                /*SoundManager.Instance.PlayVFXSound(3);*/
                StartCoroutine(UnActive());
            }
        }
    }

    public bool follow()
    {
        return rubber.activeInHierarchy;
    }

    IEnumerator UnActive()
    {
        yield return new WaitForSeconds(0.3f);
        Lock.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
