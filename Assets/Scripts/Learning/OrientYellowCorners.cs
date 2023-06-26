using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientYellowCorners : MonoBehaviour
{
    private CubeMovement cubeMovement;

    private List<string> yellowCornersOrientMoves = new();
    private int yellowCornersOrient_index;

    // Start is called before the first frame update
    void Start()
    {
        cubeMovement = FindObjectOfType<CubeMovement>();
        yellowCornersOrientMoves = new() { "R'", "D'", "R", "D", "R'", "D'", "R", "D", "U'", "R'", "D'", "R", "D", "R'", "D'", "R", "D", "R'", "D'", "R", "D", "R'", "D'", "R", "D", "U" };
        yellowCornersOrient_index = 0;
    }

    public void Click_NextCornersMove()
    {
        if (!(yellowCornersOrient_index < yellowCornersOrientMoves.Count))
        {
            return;
        }
        StartCoroutine(NextCornersMove());
    }

    public IEnumerator NextCornersMove()
    {
        string move = yellowCornersOrientMoves[yellowCornersOrient_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("D"), move));
        yellowCornersOrient_index++;
    }

    public void Click_PreviousCornersMove()
    {
        if (!(yellowCornersOrient_index > 0))
        {
            return;
        }
        yellowCornersOrient_index--;
        StartCoroutine(PreviousCornersMove());
    }

    public IEnumerator PreviousCornersMove()
    {
        string move = yellowCornersOrientMoves[yellowCornersOrient_index];
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
