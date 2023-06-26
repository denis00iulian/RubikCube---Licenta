using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowEdges : MonoBehaviour
{
    private CubeMovement cubeMovement;

    private List<string> yellowEdgesMoves = new();
    private int yellowEdges_index;

    // Start is called before the first frame update
    void Start()
    {
        cubeMovement = FindObjectOfType<CubeMovement>();
        yellowEdgesMoves = new() { "R", "U", "R'", "U", "R", "U2", "R'", "U" };
        yellowEdges_index = 0;
    }

    public void Click_NextEdgesMove()
    {
        if (!(yellowEdges_index < yellowEdgesMoves.Count))
        {
            return;
        }
        StartCoroutine(NextEdgesMove());
    }

    public IEnumerator NextEdgesMove()
    {
        string move = yellowEdgesMoves[yellowEdges_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("D"), move));
        yellowEdges_index++;
    }

    public void Click_PreviousEdgesMove()
    {
        if (!(yellowEdges_index > 0))
        {
            return;
        }
        yellowEdges_index--;
        StartCoroutine(PreviousEdgesMove());
    }

    public IEnumerator PreviousEdgesMove()
    {
        string move = yellowEdgesMoves[yellowEdges_index];
        if (move.Contains("'"))
        {
            move = move[0].ToString();
        }
        else if (!move.Contains("2"))
        {
            move += "'";
        }
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("D"), move));
    }
}
