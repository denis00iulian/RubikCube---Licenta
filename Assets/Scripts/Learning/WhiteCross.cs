using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCross : MonoBehaviour
{
    private CubeMovement cubeMovement;

    private List<string> flippingEdgeMoves = new();
    private int flippingEdge_index;
    private List<string> bottomLayerMoves = new();
    private int bottomLayer_index;
    private List<string> middleLayerMoves = new();
    private int middleLayer_index;
    private List<string> mirroredBottomLayerMoves = new();
    private int mirroredBottomLayer_index;

    // Start is called before the first frame update
    void Start()
    {
        cubeMovement = FindObjectOfType<CubeMovement>();
        flippingEdgeMoves = new() { "F", "U'", "R", "U" };
        flippingEdge_index = 0;

        bottomLayerMoves = new() { "F'", "U'", "R", "U" };
        bottomLayer_index = 0;

        middleLayerMoves = new() { "U'", "R", "U" };
        middleLayer_index = 0;  

        mirroredBottomLayerMoves = new() { "U", "L'", "U'" };
        mirroredBottomLayer_index = 0;
    }

    public void Click_NextFlippingEdgeMove()
    {
        if (!(flippingEdge_index < flippingEdgeMoves.Count))
        {
            return;
        }
        StartCoroutine(NextFlippingEdgeMove());
    }

    public IEnumerator NextFlippingEdgeMove()
    {
        string move = flippingEdgeMoves[flippingEdge_index];
        print(move);
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("U"), move));
        flippingEdge_index++;
    }

    public void Click_PreviousFlippingEdgeMove()
    {
        flippingEdge_index--;
        if (!(flippingEdge_index > -1))
        {
            return;
        }
        StartCoroutine(PreviousFlippingEdgeMove());
    }

    public IEnumerator PreviousFlippingEdgeMove()
    {
        string move = flippingEdgeMoves[flippingEdge_index];
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

    public void Click_NextBottomLayerMove()
    {
        if (!(bottomLayer_index < bottomLayerMoves.Count))
        {
            return;
        }
        StartCoroutine(NextBottomLayerMove());
    }

    public IEnumerator NextBottomLayerMove()
    {
        string move = bottomLayerMoves[bottomLayer_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("U"), move));
        bottomLayer_index++;
    }

    public void Click_PreviousBottomLayerMove()
    {
        bottomLayer_index--;
        if (!(bottomLayer_index > -1))
        {
            return;
        }
        StartCoroutine(PreviousBottomLayerMove());
    }

    public IEnumerator PreviousBottomLayerMove()
    {
        string move = bottomLayerMoves[bottomLayer_index];
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

    public void Click_NextMiddleLayerMove()
    {
        if (!(middleLayer_index < middleLayerMoves.Count))
        {
            return;
        }
        StartCoroutine(NextMiddleLayerMove());
    }

    public IEnumerator NextMiddleLayerMove()
    {
        string move = middleLayerMoves[middleLayer_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("U"), move));
        middleLayer_index++;
    }

    public void Click_PreviousMiddleLayerMove()
    {
        middleLayer_index--;
        if (!(middleLayer_index > -1))
        {
            return;
        }
        StartCoroutine(PreviousMiddleLayerMove());
    }

    public IEnumerator PreviousMiddleLayerMove()
    {
        string move = middleLayerMoves[middleLayer_index];
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

    public void Click_NextMirroredMiddleLayerMove()
    {
        if (!(mirroredBottomLayer_index < mirroredBottomLayerMoves.Count))
        {
            return;
        }
        StartCoroutine(NextMirroredMiddleLayerMove());
    }

    public IEnumerator NextMirroredMiddleLayerMove()
    {
        string move = mirroredBottomLayerMoves[mirroredBottomLayer_index];
        yield return StartCoroutine(cubeMovement.PerformMove_Learning(GameObject.Find("L"), GameObject.Find("U"), move));
        mirroredBottomLayer_index++;
    }

    public void Click_PreviousMirroredMiddleLayerMove()
    {
        mirroredBottomLayer_index--;
        if (!(mirroredBottomLayer_index > -1))
        {
            return;
        }
        StartCoroutine(PreviousMirroredMiddleLayerMove());
    }

    public IEnumerator PreviousMirroredMiddleLayerMove()
    {
        string move = mirroredBottomLayerMoves[mirroredBottomLayer_index];
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


}
