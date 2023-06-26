using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCross : MonoBehaviour
{
    private CubeMovement cubeMovement;

    private List<string> yellowCrossOneMoves = new();
    private int yellowCrossOne_index;

    private List<string> yellowCrossTwoMoves = new();
    private int yellowCrossTwo_index;

    private List<string> yellowCrossThreeMoves = new();
    private int yellowCrossThree_index;

    // Start is called before the first frame update
    void Start()
    {
        cubeMovement = FindObjectOfType<CubeMovement>();
        yellowCrossOneMoves = new() { "F", "R", "U", "R'", "U'", "F'" };
        yellowCrossOne_index = 0;

        yellowCrossTwoMoves = new() { "F", "R", "U", "R'", "U'", "F'" };
        yellowCrossTwo_index = 0;

        yellowCrossThreeMoves = new() { "F", "R", "U", "R'", "U'", "F'" };
        yellowCrossThree_index = 0;
    }

    public void Click_NextStateOneMove()
    {
        if (!(yellowCrossOne_index < yellowCrossOneMoves.Count))
        {
            return;
        }
        StartCoroutine(NextStateOneMove());
    }

    public IEnumerator NextStateOneMove()
    {
        string move = yellowCrossOneMoves[yellowCrossOne_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("D"), move));
        yellowCrossOne_index++;
    }

    public void Click_PreviousStateOneMove()
    {
        if (!(yellowCrossOne_index > 0))
        {
            return;
        }
        yellowCrossOne_index--;
        StartCoroutine(PreviousStateOneMove());
    }

    public IEnumerator PreviousStateOneMove()
    {
        string move = yellowCrossOneMoves[yellowCrossOne_index];
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

    public void Click_NextStateTwoMove()
    {
        if (!(yellowCrossTwo_index < yellowCrossTwoMoves.Count))
        {
            return;
        }
        StartCoroutine(NextStateTwoMove());
    }

    public IEnumerator NextStateTwoMove()
    {
        string move = yellowCrossTwoMoves[yellowCrossTwo_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("D"), move));
        yellowCrossTwo_index++;
    }

    public void Click_PreviousStateTwoMove()
    {
        if (!(yellowCrossTwo_index > 0))
        {
            return;
        }
        yellowCrossTwo_index--;
        StartCoroutine(PreviousStateTwoMove());
    }

    public IEnumerator PreviousStateTwoMove()
    {
        string move = yellowCrossTwoMoves[yellowCrossTwo_index];
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

    public void Click_NextStateThreeMove()
    {
        if (!(yellowCrossThree_index < yellowCrossThreeMoves.Count))
        {
            return;
        }
        StartCoroutine(NextStateThreeMove());
    }

    public IEnumerator NextStateThreeMove()
    {
        string move = yellowCrossThreeMoves[yellowCrossThree_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("D"), move));
        yellowCrossThree_index++;
    }

    public void Click_PreviousStateThreeMove()
    {
        if (!(yellowCrossThree_index > 0))
        {
            return;
        }
        yellowCrossThree_index--;
        StartCoroutine(PreviousStateThreeMove());
    }

    public IEnumerator PreviousStateThreeMove()
    {
        string move = yellowCrossThreeMoves[yellowCrossThree_index];
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
