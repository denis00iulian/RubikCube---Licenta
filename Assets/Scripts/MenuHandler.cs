using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MenuHandler : MonoBehaviour
{
    public GameObject cube;
    public GameObject canvas;
    public GameObject helpCanvas;

    public GameObject whiteCrossCanvas_1;
    public GameObject cubeWhiteCross_1;
    public GameObject whiteCrossCanvas_2;
    public GameObject cubeWhiteCross_2;
    public GameObject whiteCrossCanvas_3;
    public GameObject cubeWhiteCross_3;
    public GameObject whiteCrossCanvas_4;
    public GameObject cubeWhiteCross_4;

    public GameObject whiteCornersCanvas_1;
    public GameObject cubeWhiteCorners_1;
    public GameObject whiteCornersCanvas_2;
    public GameObject cubeWhiteCorners_2;
    public GameObject whiteCornersCanvas_3;
    public GameObject cubeWhiteCorners_3;
    public GameObject whiteCornersCanvas_4;
    public GameObject cubeWhiteCorners_4;

    public GameObject f2lCanvas_1;
    public GameObject cubeF2L_1;
    public GameObject f2lCanvas_2;
    public GameObject cubeF2L_2;
    public GameObject f2lCanvas_3;
    public GameObject cubeF2L_3;

    public GameObject yellowCrossCanvas_1;
    public GameObject cubeYellowCross_1;
    public GameObject yellowCrossCanvas_2;
    public GameObject cubeYellowCross_2;
    public GameObject yellowCrossCanvas_3;
    public GameObject cubeYellowCross_3;

    public GameObject yellowEdgesCanvas_1;
    public GameObject cubeYellowEdges_1;

    public GameObject yellowCornersPositionCanvas_1;
    public GameObject cubeYellowCornersPosition_1;
    public GameObject yellowCornersPositionCanvas_2;
    public GameObject cubeYellowCornersPosition_2;

    public GameObject yellowCornersOrientCanvas_1;
    public GameObject cubeYellowCornersOrient_1;

    public List<GameObject> list = new();

    // Start is called before the first frame update
    void Start()
    {
        list = new() { cube, canvas, helpCanvas, 
            whiteCrossCanvas_1, cubeWhiteCross_1, whiteCrossCanvas_2, cubeWhiteCross_2, whiteCrossCanvas_3, cubeWhiteCross_3, whiteCrossCanvas_4, cubeWhiteCross_4,
            whiteCornersCanvas_1, whiteCornersCanvas_2, whiteCornersCanvas_3, whiteCornersCanvas_4, cubeWhiteCorners_1, cubeWhiteCorners_2, cubeWhiteCorners_3, cubeWhiteCorners_4,
            f2lCanvas_1, f2lCanvas_2, f2lCanvas_3, cubeF2L_1, cubeF2L_2, cubeF2L_3,
            yellowCrossCanvas_1, yellowCrossCanvas_2, yellowCrossCanvas_3, cubeYellowCross_1, cubeYellowCross_2, cubeYellowCross_3,
            yellowEdgesCanvas_1, cubeYellowEdges_1,
            yellowCornersPositionCanvas_1, yellowCornersPositionCanvas_2, cubeYellowCornersPosition_1, cubeYellowCornersPosition_2,
            yellowCornersOrientCanvas_1, cubeYellowCornersOrient_1
        };
    }

    public void ShowHelp()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        helpCanvas.SetActive(true);
        ResetCamera();
    }

    public void HideHelp()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        canvas.SetActive(true);
        cube.SetActive(true);
        ResetCamera();
    }

    public void ShowWhiteCross_1_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        whiteCrossCanvas_1.SetActive(true);
        cubeWhiteCross_1.SetActive(true);
        ResetCamera();
    }

    public void ShowWhiteCross_2_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        whiteCrossCanvas_2.SetActive(true);
        cubeWhiteCross_2.SetActive(true);
        ResetCamera();
    }

    public void ShowWhiteCross_3_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        whiteCrossCanvas_3.SetActive(true);
        cubeWhiteCross_3.SetActive(true);
        ResetCamera();
    }

    public void ShowWhiteCross_4_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        whiteCrossCanvas_4.SetActive(true);
        cubeWhiteCross_4.SetActive(true);
        ResetCamera();
    }

    public void ShowWhiteCorners_1_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        whiteCornersCanvas_1.SetActive(true);
        cubeWhiteCorners_1.SetActive(true);
        ResetCamera();
    }
    public void ShowWhiteCorners_2_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        whiteCornersCanvas_2.SetActive(true);
        cubeWhiteCorners_2.SetActive(true);
        ResetCamera();
    }
    public void ShowWhiteCorners_3_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        whiteCornersCanvas_3.SetActive(true);
        cubeWhiteCorners_3.SetActive(true);
        ResetCamera();
    }

    public void ShowWhiteCorners_4_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        whiteCornersCanvas_4.SetActive(true);
        cubeWhiteCorners_4.SetActive(true);
        ResetCamera();
    }

    public void ShowF2L_1_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        f2lCanvas_1.SetActive(true);
        cubeF2L_1.SetActive(true);
        RotateCamera();
    }
    public void ShowF2L_2_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        f2lCanvas_2.SetActive(true);
        cubeF2L_2.SetActive(true);
        RotateCamera();
    }

    public void ShowF2L_3_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        f2lCanvas_3.SetActive(true);
        cubeF2L_3.SetActive(true);
        RotateCamera();
    }

    public void ShowYellowCross_1_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        yellowCrossCanvas_1.SetActive(true);
        cubeYellowCross_1.SetActive(true);
        RotateCamera();
    }

    public void ShowYellowCross_2_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        yellowCrossCanvas_2.SetActive(true);
        cubeYellowCross_2.SetActive(true);
        RotateCamera();
    }

    public void ShowYellowCross_3_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        yellowCrossCanvas_3.SetActive(true);
        cubeYellowCross_3.SetActive(true);
        RotateCamera();
    }

    public void ShowYellowEdges_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        yellowEdgesCanvas_1.SetActive(true);
        cubeYellowEdges_1.SetActive(true);
        RotateCamera();
    }

    public void ShowYellowCornersPosition_1_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        yellowCornersPositionCanvas_1.SetActive(true);
        cubeYellowCornersPosition_1.SetActive(true);
        RotateCamera();
    }

    public void ShowYellowCornersPosition_2_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        yellowCornersPositionCanvas_2.SetActive(true);
        cubeYellowCornersPosition_2.SetActive(true);
        RotateCamera();
    }

    public void ShowYellowCornersOrient_1_Canvas()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        yellowCornersOrientCanvas_1.SetActive(true);
        cubeYellowCornersOrient_1.SetActive(true);
        RotateCamera();
    }

    public void ResetCamera()
    {
        Camera.main.transform.position = new Vector3(-7.5f, 4.5f, -7.5f);
        Vector3 targetRotation = new(23, 45, 0);
        Camera.main.transform.rotation = Quaternion.Euler(targetRotation);
    }
    
    public void RotateCamera()
    {
        Camera.main.transform.position = new Vector3(-7.5f, -4.5f, 7.5f);
        Vector3 targetRotation = new(-23, 135, 180);
        Camera.main.transform.rotation = Quaternion.Euler(targetRotation);
    }
}
