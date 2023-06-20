using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CubeMap : MonoBehaviour
{
    public Transform up;
    public Transform down;
    public Transform left;
    public Transform right;
    public Transform front;
    public Transform back;

    private CubeState cubeState;

    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Set()
    {

        UpdateMap(cubeState.front, front);
        UpdateMap(cubeState.back, back);
        UpdateMap(cubeState.left, left);
        UpdateMap(cubeState.right, right);
        UpdateMap(cubeState.up, up);
        UpdateMap(cubeState.down, down);
    }

    void UpdateMap(List<GameObject> face, Transform side)
    {
        int i = 0;
        foreach (Transform map in side)
        {
            if (face[i].name.Equals("Front"))
            {
                map.GetComponent<Image>().color = Color.green;
            }
            if (face[i].name.Equals("Back"))
            {
                map.GetComponent<Image>().color = Color.blue;
            }
            if (face[i].name.Equals("Up"))
            {
                map.GetComponent<Image>().color = Color.white;
            }
            if (face[i].name.Equals("Down"))
            {
                map.GetComponent<Image>().color = Color.yellow;
            }
            if (face[i].name.Equals("Left"))
            {
                map.GetComponent<Image>().color = new Color(1, 0.5f, 0, 1);
            }
            if (face[i].name.Equals("Right"))
            {
                map.GetComponent<Image>().color = Color.red;
            }
            i++;
        }

    }
}
