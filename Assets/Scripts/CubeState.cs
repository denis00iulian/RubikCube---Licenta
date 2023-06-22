using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeState : MonoBehaviour
{
    // faces
    public List<GameObject> up = new();
    public List<GameObject> down = new();
    public List<GameObject> front = new();
    public List<GameObject> back = new();
    public List<GameObject> left = new();
    public List<GameObject> right = new();

    public static bool autoRotating = false;
    public static bool started = false;

    public Transform tUp;
    public Transform tDown;
    public Transform tLeft;
    public Transform tRight;
    public Transform tFront;
    public Transform tBack;

    private List<GameObject> frontRays = new();
    private List<GameObject> backRays = new();
    private List<GameObject> upRays = new();
    private List<GameObject> downRays = new();
    private List<GameObject> leftRays = new();
    private List<GameObject> rightRays = new();

    private readonly int layerMask = 1 << 8;
    CubeMap cubeMap;
    CubeState cubeState;
    public GameObject emptyGO;

    // Start is called before the first frame update
    void Start()
    {
        SetRayTransforms();
        cubeMap = FindObjectOfType<CubeMap>();
        ReadState();
        CubeState.started = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReadState()
    {
        cubeMap = FindObjectOfType<CubeMap>();
        cubeState = FindObjectOfType<CubeState>();

        cubeState.up = ReadFace(upRays, tUp);
        cubeState.down = ReadFace(downRays, tDown);
        cubeState.left = ReadFace(leftRays, tLeft);
        cubeState.right = ReadFace(rightRays, tRight);
        cubeState.front = ReadFace(frontRays, tFront);
        cubeState.back = ReadFace(backRays, tBack);
        if (cubeMap != null)
            cubeMap.Set();
    }

    void SetRayTransforms()
    {
        upRays = BuildRays(tUp, new Vector3(90, 0, 0));
        downRays = BuildRays(tDown, new Vector3(270, 0, 0));
        leftRays = BuildRays(tLeft, new Vector3(0, 90, 0));
        rightRays = BuildRays(tRight, new Vector3(0, 270, 0));
        frontRays = BuildRays(tFront, new Vector3(0, 0, 0));
        backRays = BuildRays(tBack, new Vector3(0, 180, 0));
    }

    List<GameObject> BuildRays(Transform rayTransform, Vector3 direction)
    {
        int rayCount = 0;
        List<GameObject> rays = new();

        for (int y = 1; y >= -1; y--)
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector3 startPos = new(rayTransform.localPosition.x + x, rayTransform.localPosition.y + y, rayTransform.localPosition.z);
                GameObject rayStart = Instantiate(emptyGO, startPos, Quaternion.identity, rayTransform);
                rayStart.name = rayCount.ToString();
                rays.Add(rayStart);
                rayCount++;
            }
        }
        rayTransform.localRotation = Quaternion.Euler(direction);

        return rays;
    }

    public List<GameObject> ReadFace(List<GameObject> rayStarts, Transform rayTransform)
    {
        List<GameObject> facesHit = new();

        foreach (GameObject rayStart in rayStarts)
        {
            Vector3 ray = rayStart.transform.position;
            if (Physics.Raycast(ray, rayTransform.forward, out RaycastHit hit, Mathf.Infinity, layerMask))
            {
                Debug.DrawRay(ray, rayTransform.forward * hit.distance, Color.red);
                facesHit.Add(hit.collider.gameObject);
            }
            else
            {
                Debug.DrawRay(ray, rayTransform.forward * 1000, Color.green);
            }
        }


        return facesHit;
    }
}
