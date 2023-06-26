using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTwoLayers : MonoBehaviour
{
    private CubeMovement cubeMovement;

    private List<string> rightMoves = new();
    private int right_index;

    private List<string> leftMoves = new();
    private int left_index;

    private List<string> wrongOrientationMoves = new();
    private int wrongOrientation_index;

    // Start is called before the first frame update
    void Start()
    {
        cubeMovement = FindObjectOfType<CubeMovement>();
        rightMoves = new() { "U", "R", "U'", "R'", "U'", "F'", "U", "F" };
        right_index = 0;

        leftMoves = new() { "U'", "L'", "U", "L", "U", "F", "U'", "F'" };
        left_index = 0;

        wrongOrientationMoves = new() { "U", "R", "U'", "R'", "U'", "F'", "U", "F", "U2", "U", "R", "U'", "R'", "U'", "F'", "U", "F" };
        wrongOrientation_index = 0;
    }

    public void Click_NextRightMove()
    {
        if (!(right_index < rightMoves.Count))
        {
            return;
        }
        StartCoroutine(NextRightMove());
    }

    public IEnumerator NextRightMove()
    {
        string move = rightMoves[right_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("D"), move));
        right_index++;
    }

    public void Click_PreviousRightMove()
    {
        if (!(right_index > 0))
        {
            return;
        }
        right_index--;
        StartCoroutine(PreviousRightMove());
    }

    public IEnumerator PreviousRightMove()
    {
        string move = rightMoves[right_index];
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

    public void Click_NextLeftMove()
    {
        if (!(left_index < leftMoves.Count))
        {
            return;
        }
        StartCoroutine(NextLeftMove());
    }

    public IEnumerator NextLeftMove()
    {
        string move = leftMoves[left_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("D"), move));
        left_index++;
    }

    public void Click_PreviousLeftMove()
    {
        if (!(left_index > 0))
        {
            return;
        }
        left_index--;
        StartCoroutine(PreviousLeftMove());
    }

    public IEnumerator PreviousLeftMove()
    {
        string move = leftMoves[left_index];
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

    public void Click_NextWrongOrientationMove()
    {
        if (!(wrongOrientation_index < wrongOrientationMoves.Count))
        {
            return;
        }
        StartCoroutine(NextWrongOrientationMove());
    }

    public IEnumerator NextWrongOrientationMove()
    {
        string move = wrongOrientationMoves[wrongOrientation_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("D"), move));
        wrongOrientation_index++;
    }

    public void Click_PreviousWrongOrientationMove()
    {
        if (!(wrongOrientation_index > 0))
        {
            return;
        }
        wrongOrientation_index--;
        StartCoroutine(PreviousWrongOrientationMove());
    }

    public IEnumerator PreviousWrongOrientationMove()
    {
        string move = wrongOrientationMoves[wrongOrientation_index];
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
