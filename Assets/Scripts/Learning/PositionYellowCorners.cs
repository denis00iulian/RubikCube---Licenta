using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionYellowCorners : MonoBehaviour
{
    private CubeMovement cubeMovement;

    private List<string> yellowCornersOneMoves = new();
    private int yellowCornersOne_index;

    private List<string> yellowCornersTwoMoves = new();
    private int yellowCornersTwo_index;

    // Start is called before the first frame update
    void Start()
    {
        cubeMovement = FindObjectOfType<CubeMovement>();
        yellowCornersOneMoves = new() { "U", "R", "U'", "L'", "U", "R'", "U'", "L" };
        yellowCornersOne_index = 0;

        yellowCornersTwoMoves = new() { "U", "R", "U'", "L'", "U", "R'", "U'", "L" };
        yellowCornersTwo_index = 0;
    }

    public void Click_NextOneMove()
    {
        if (!(yellowCornersOne_index < yellowCornersOneMoves.Count))
        {
            return;
        }
        StartCoroutine(NextOneMove());
    }

    public IEnumerator NextOneMove()
    {
        string move = yellowCornersOneMoves[yellowCornersOne_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("D"), move));
        yellowCornersOne_index++;
    }

    public void Click_PreviousOneMove()
    {
        if (!(yellowCornersOne_index > 0))
        {
            return;
        }
        yellowCornersOne_index--;
        StartCoroutine(PreviousOneMove());
    }

    public IEnumerator PreviousOneMove()
    {
        string move = yellowCornersOneMoves[yellowCornersOne_index];
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

    public void Click_NextTwoMove()
    {
        if (!(yellowCornersTwo_index < yellowCornersTwoMoves.Count))
        {
            return;
        }
        StartCoroutine(NextTwoMove());
    }

    public IEnumerator NextTwoMove()
    {
        string move = yellowCornersTwoMoves[yellowCornersTwo_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("D"), move));
        yellowCornersTwo_index++;
    }

    public void Click_PreviousTwoMove()
    {
        if (!(yellowCornersTwo_index > 0))
        {
            return;
        }
        yellowCornersTwo_index--;
        StartCoroutine(PreviousTwoMove());
    }

    public IEnumerator PreviousTwoMove()
    {
        string move = yellowCornersTwoMoves[yellowCornersTwo_index];
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
