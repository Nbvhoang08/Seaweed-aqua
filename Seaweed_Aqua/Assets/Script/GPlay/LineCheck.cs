using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCheck : MonoBehaviour
{
    // Start is called before the first frame update
    public LineRenderer Line1;
    public LineRenderer Line2;
    public List<LineRenderer> intersectingLines = new List<LineRenderer>();
    public List<LineRenderer> lineCut = new List<LineRenderer>();

    [SerializeField] private bool finishedCheckingIntersections = false;
    [SerializeField] private LineRenderer[] allLines;

    void Start()
    {
        StartCoroutine(TimeTocheck());
    }
    void Update()
    {
        if (!finishedCheckingIntersections)
        {
            CheckForIntersectingLines();
        }


    }

    void CheckForIntersectingLines()
    {
        // Lấy danh sách các LineRenderer trong phạm vi
        allLines = FindObjectsOfType<LineRenderer>();

        for (int i = 0; i < allLines.Length; i++)
        {

            if (allLines[i] != Line1 && allLines[i] != Line2)
            {
                // Kiểm tra nếu đường này cắt qua cả 2 đường LineRenderer của đối tượng chính
                if (IntersectsWithBothLines(allLines[i]))
                {
                    // Kiểm tra điều kiện về sorting order
                    int line1SortOrder = Line1.sortingOrder;
                    int line2SortOrder = Line2.sortingOrder;
                    int lineSortOrder = allLines[i].sortingOrder;

                    if ((line1SortOrder < lineSortOrder && lineSortOrder < line2SortOrder) ||
                        (line2SortOrder < lineSortOrder && lineSortOrder < line1SortOrder))
                    {
                        if (!intersectingLines.Contains(allLines[i]))
                        {
                            intersectingLines.Add(allLines[i]);
                        }
                    }
                }
            }
        }
    }

    bool IntersectsWithBothLines(LineRenderer line)
    {
        return lineCut.Contains(line);
    }
    IEnumerator TimeTocheck()
    {
        yield return new WaitForSeconds(3f);
        finishedCheckingIntersections = true;
    }
}
