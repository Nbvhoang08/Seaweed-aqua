using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCutLine : MonoBehaviour
{
    [SerializeField] private LineCheck lineChecker;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Line"))
        {
            if (!lineChecker.lineCut.Contains(other.GetComponent<LineRenderer>()))
            {
                lineChecker.lineCut.Add(other.GetComponent<LineRenderer>());
            }

        }
    }
}
