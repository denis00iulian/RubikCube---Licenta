using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<GameObject> list = new();

    // Start is called before the first frame update
    void Start()
    {
        list = new() { cube, canvas, helpCanvas, 
            whiteCrossCanvas_1, cubeWhiteCross_1, whiteCrossCanvas_2, cubeWhiteCross_2, whiteCrossCanvas_3, cubeWhiteCross_3, whiteCrossCanvas_4, cubeWhiteCross_4,
            whiteCornersCanvas_1, whiteCornersCanvas_2, whiteCornersCanvas_3, whiteCornersCanvas_4, cubeWhiteCorners_1, cubeWhiteCorners_2, cubeWhiteCorners_3, cubeWhiteCorners_4
        };
    }

    // Update is called once per frame
    void Update()
    {

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

    public void BackToHelp()
    {
        foreach (var item in list)
        {
            item.SetActive(false);
        }
        helpCanvas.SetActive(true);
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

    public void ResetCamera()
    {
        Camera.main.transform.position = new Vector3(-7.5f, 4.5f, -7.5f);
        Vector3 targetRotation = new(23, 45, 0);
        Camera.main.transform.rotation = Quaternion.Euler(targetRotation);
    }
}
