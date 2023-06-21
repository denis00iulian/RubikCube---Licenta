using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCorners : MonoBehaviour
{
    private CubeMovement cubeMovement;

    private List<string> whiteRightMoves = new();
    private int whiteRight_index;

    private List<string> whiteYouMoves = new();
    private int whiteYou_index;

    private List<string> whiteDownMoves = new();
    private int whiteDown_index;

    private List<string> whiteWrongPosMoves = new();
    private int whiteWrongPos_index;

    // Start is called before the first frame update
    void Start()
    {
        cubeMovement = FindObjectOfType<CubeMovement>();
        whiteRightMoves = new() { "R'", "D'", "R" };
        whiteRight_index = 0;

        whiteYouMoves = new() { "F", "D", "F'" };
        whiteYou_index = 0;

        whiteDownMoves = new() { "R2", "D'", "R2", "D", "R2"};
        whiteDown_index = 0;

        whiteWrongPosMoves = new() { "L", "D", "L'", "R'", "D'", "R" };
        whiteWrongPos_index = 0;
    }

    public void Click_NextWhiteRightMove()
    {
        if (!(whiteRight_index < whiteRightMoves.Count))
        {
            return;
        }
        StartCoroutine(NextWhiteRightMove());
    }

    public IEnumerator NextWhiteRightMove()
    {
        string move = whiteRightMoves[whiteRight_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("U"), move));
        whiteRight_index++;
    }

    public void Click_PreviousWhiteRightMove()
    {
        if (!(whiteRight_index > 0))
        {
            return;
        }
        whiteRight_index--;
        StartCoroutine(PreviousWhiteRightMove());
    }

    public IEnumerator PreviousWhiteRightMove()
    {
        string move = whiteRightMoves[whiteRight_index];
        if (move.Contains("'"))
        {
            move = move[0].ToString();
        }
        else
        {
            move += "'";
        }
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("U"), move));
    }

    public void Click_NextWhiteYouMove()
    {
        if (!(whiteYou_index < whiteYouMoves.Count))
        {
            return;
        }
        StartCoroutine(NextWhiteYouMove());
    }

    public IEnumerator NextWhiteYouMove()
    {
        string move = whiteYouMoves[whiteYou_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("U"), move));
        whiteYou_index++;
    }

    public void Click_PreviousWhiteYouMove()
    {
        if (!(whiteYou_index > 0))
        {
            return;
        }
        whiteYou_index--;
        StartCoroutine(PreviousWhiteYouMove());
    }

    public IEnumerator PreviousWhiteYouMove()
    {
        string move = whiteYouMoves[whiteYou_index];
        if (move.Contains("'"))
        {
            move = move[0].ToString();
        }
        else
        {
            move += "'";
        }
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("U"), move));
    }

    public void Click_NextWhiteDownMove()
    {
        if (!(whiteDown_index < whiteDownMoves.Count))
        {
            return;
        }
        StartCoroutine(NextWhiteDownMove());
    }

    public IEnumerator NextWhiteDownMove()
    {
        string move = whiteDownMoves[whiteDown_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("U"), move));
        whiteDown_index++;
    }

    public void Click_PreviousWhiteDownMove()
    {
        if (!(whiteDown_index > 0))
        {
            return;
        }
        whiteDown_index--;
        StartCoroutine(PreviousWhiteDownMove());
    }

    public IEnumerator PreviousWhiteDownMove()
    {
        string move = whiteDownMoves[whiteDown_index];
        if (move.Contains("'"))
        {
            move = move[0].ToString();
        }
        else if (!move.Contains("2"))
        {
            move += "'";
        }
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("U"), move));
    }

    public void Click_NextWrongPosMove()
    {
        if (!(whiteWrongPos_index < whiteWrongPosMoves.Count))
        {
            return;
        }
        StartCoroutine(NextWhiteWrongPosMove());
    }

    public IEnumerator NextWhiteWrongPosMove()
    {
        string move = whiteWrongPosMoves[whiteWrongPos_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("U"), move));
        whiteWrongPos_index++;
    }

    public void Click_PreviousWhiteWrongPosMove()
    {
        if (!(whiteWrongPos_index > 0))
        {
            return;
        }
        whiteWrongPos_index--;
        StartCoroutine(PreviousWhiteWrongPosMove());
    }

    public IEnumerator PreviousWhiteWrongPosMove()
    {
        string move = whiteWrongPosMoves[whiteWrongPos_index];
        if (move.Contains("'"))
        {
            move = move[0].ToString();
        }
        else if (!move.Contains("2"))
        {
            move += "'";
        }
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("U"), move));
    }
}
