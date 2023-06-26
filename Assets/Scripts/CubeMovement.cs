using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GridBrushBase;

public class CubeMovement : MonoBehaviour
{
    private const float minimumDragPixels = 3.0f;
    private const float dragSensitivity = 0.2f;
    public static float snapSpeed = 400.0f;
    public static bool moving = false;

    private readonly int layerMask = 1 << 6;

    Camera _camera;
    readonly Collider[] _subCubes = new Collider[9];
    readonly Vector3[] _originalPositions = new Vector3[9];
    readonly Quaternion[] _originalOrientations = new Quaternion[9];

    private CubeState cubeState;

    IEnumerator Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        

        while (true)
        {
            yield return null;

            // Keep waiting until the player presses the mouse button.
            if (!Input.GetMouseButton(0))
                continue;
            
            // Step 1: Fire a ray through the mouse position.
            Vector2 clickPosition = Input.mousePosition;
            _camera = Camera.main;
            var ray = _camera.ScreenPointToRay(clickPosition);

            // If we didn't hit anything, try again next frame.
            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask) || !hit.collider.transform.parent.name.Equals("Cube"))
                continue;

            // Step 2: find the two potential axes of rotation.
            int normalAxis = Mathf.Abs(Mathf.RoundToInt(Vector3.Dot(
                    hit.normal,
                    new Vector3(0, 1, 2)
                )));

            Vector3 rotationAxis = Vector3.zero;
            Vector3 alternativeAxis = Vector3.zero;

            rotationAxis[(normalAxis + 1) % 3] = 1;
            alternativeAxis[(normalAxis + 2) % 3] = 1;

            Vector3 camForward = _camera.transform.forward;
            float axisSign = Mathf.Sign(camForward.x * camForward.y * camForward.z);

            // This sign fiddling just ensures our drag direction matches the visible rotation.
            float signFlip = axisSign * Mathf.Sign(Vector3.Dot(rotationAxis, camForward) * Mathf.Sign(Vector3.Dot(alternativeAxis, camForward)));
            Vector2 rotationDirection = signFlip * ScreenDirection(clickPosition, hit.point, alternativeAxis);
            Vector2 alternativeDirection = -signFlip * ScreenDirection(clickPosition, hit.point, rotationAxis);

            // Step 3: wait until we've dragged far enough to pick an axis.
            float signedDistance;
            do
            {
                yield return null;

                Vector2 mousePosition = Input.mousePosition;
                signedDistance = DistanceAlong(clickPosition, mousePosition, rotationDirection);
                if (Mathf.Abs(signedDistance) > minimumDragPixels)
                    break;

                signedDistance = DistanceAlong(clickPosition, mousePosition, alternativeDirection);
                if (Mathf.Abs(signedDistance) > minimumDragPixels)
                {
                    rotationAxis = alternativeAxis;
                    rotationDirection = alternativeDirection;
                    break;
                }

            } while (Input.GetMouseButton(0));

            // Step 4: Gather the 8-9 sub-cubes we need to rotate.
            Vector3 extents = Vector3.one - 0.9f * rotationAxis;
            extents *= 2.0f;
            int subCubeCount = Physics.OverlapBoxNonAlloc(hit.collider.transform.position, extents, _subCubes, Quaternion.identity, layerMask);

            for (int i = 0; i < subCubeCount; i++)
            {
                var subCube = _subCubes[i].transform;
                _originalPositions[i] = subCube.position;
                _originalOrientations[i] = subCube.rotation;
            }

            // Step 5-6: Rotate the group by the input angle each frame.
            float angle = 0.0f;
            while (Input.GetMouseButton(0))
            {
                angle = signedDistance * dragSensitivity;
                RotateGroup(angle, rotationAxis, subCubeCount);

                yield return null;
                Vector2 mousePosition = Input.mousePosition;
                signedDistance = DistanceAlong(clickPosition, mousePosition, rotationDirection);
            }

            // Step 7: After release, snap the angle to a multiple of 90.
            float snappedAngle = Mathf.Round(angle / 90.0f) * 90.0f;

            while (angle != snappedAngle)
            {
                angle = Mathf.MoveTowards(angle, snappedAngle, snapSpeed * Time.deltaTime);

                RotateGroup(angle, rotationAxis, subCubeCount);
                yield return null;
            }

            // Step 8: Loop back and wait for the next drag.
            if (SceneManager.GetActiveScene().buildIndex == 0)
                cubeState.ReadState();
        }
    }

    Vector2 ScreenDirection(Vector2 screenPoint, Vector3 worldPoint, Vector3 worldDirection)
    {
        Vector2 shifted = _camera.WorldToScreenPoint(worldPoint + worldDirection);

        return (shifted - screenPoint).normalized;
    }

    float DistanceAlong(Vector2 clickPosition, Vector2 currentPosition, Vector2 direction)
    {
        return Vector2.Dot(currentPosition - clickPosition, direction);
    }

    void RotateGroup(float angle, Vector3 axis, int count)
    {

        Quaternion rotation = Quaternion.AngleAxis(angle, axis);

        for (int i = 0; i < count; i++)
        {
            var subCube = _subCubes[i].transform;
            subCube.SetPositionAndRotation(rotation * _originalPositions[i], rotation * _originalOrientations[i]);
        }
    }

    public IEnumerator PerformMove(List<GameObject> face, List<GameObject> upperFace, string move)
    {
        if (!move.Equals("F") && !move.Equals("F'") && !move.Equals("F2")) 
        {
            (List<GameObject> newFace, List<GameObject> newUpperFace, string newMove) = GetNewParams(face, upperFace, move);
            yield return StartCoroutine(PerformMove(newFace, newUpperFace, newMove));
            yield break;
        }
        // Step 4: Gather the 8-9 sub-cubes we need to rotate.
        (int sign, Vector3 rotationAxis) = GetRotationSpecs(face, move);
        Vector3 extents = Vector3.one - 0.9f * rotationAxis;
        extents *= 2.0f;
        int subCubeCount = Physics.OverlapBoxNonAlloc(face[4].transform.position, extents, _subCubes, Quaternion.identity, layerMask);

        for (int i = 0; i < subCubeCount; i++)
        {
            var subCube = _subCubes[i].transform;
            _originalPositions[i] = subCube.position;
            _originalOrientations[i] = subCube.rotation;
        }
        float angle = 0.0f;
        float snappedAngle = sign * 90.0f;

        while (angle != snappedAngle)
        {
            angle = Mathf.MoveTowards(angle, snappedAngle, snapSpeed * Time.deltaTime);

            RotateGroup(angle, rotationAxis, subCubeCount);
            yield return null;
        }
        cubeState.ReadState();
    }

    (List<GameObject>, List<GameObject>, string) GetNewParams(List<GameObject> face, List<GameObject> upperFace, string move)
    {
        if (move.Equals("U"))
            return (upperFace, face, "F'");
        if (move.Equals("U2"))
            return (upperFace, face, "F2");
        if (move.Equals("U'"))
            return (upperFace, face, "F");
        if (move.Equals("D"))
            return (OppositeFace(upperFace), face, "F'");
        if (move.Equals("D2"))
            return (OppositeFace(upperFace), face, "F2");
        if (move.Equals("D'"))
            return (OppositeFace(upperFace), face, "F");
        if (move.Equals("L"))
            return (LeftNeighbourFace(face, upperFace), upperFace, "F");
        if (move.Equals("L2"))
            return (LeftNeighbourFace(face, upperFace), upperFace, "F2");
        if (move.Equals("L'"))
            return (LeftNeighbourFace(face, upperFace), upperFace, "F'");
        if (move.Equals("R"))
            return (RightNeighbourFace(face, upperFace), upperFace, "F");
        if (move.Equals("R2"))
            return (RightNeighbourFace(face, upperFace), upperFace, "F2");
        if (move.Equals("R'"))
            return (RightNeighbourFace(face, upperFace), upperFace, "F'");
        return (null, null, null);
    }

    (int, Vector3) GetRotationSpecs(List<GameObject> face, string move)
    {
        if (move.Equals("F"))
            return (GetDirection(face) * -1, GetRotationVector(face));
        if (move.Equals("F2"))
            return (GetDirection(face) * -2, GetRotationVector(face));
        if (move.Equals("F'"))
            return (GetDirection(face) * 1, GetRotationVector(face));

        return (0, Vector3.zero);
    }

    Vector3 GetRotationVector(List<GameObject> face)
    {
        if (face[4].name.Equals("Up") || face[4].name.Equals("Down"))
        {
            return new Vector3(0, 1, 0);
        }
        if (face[4].name.Equals("Left") || face[4].name.Equals("Right"))
        {
            return new Vector3(1, 0, 0);
        }
        if (face[4].name.Equals("Front") || face[4].name.Equals("Back"))
        {
            return new Vector3(0, 0, 1);
        }
        return Vector3.zero;
    }

    int GetDirection(List<GameObject> face)
    {
        if (face[4].name.Equals("Up") || face[4].name.Equals("Front") || face[4].name.Equals("Left"))
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public IEnumerator Shuffle()
    {
        if (moving)
        {
            yield break;
        }
        moving = true;
        List<string> moves = new() { "F", "F'", "L", "L'", "R", "R'", "U", "U'", "D", "D'" }; //{ "F", "F'", "B", "B'", "L", "L'", "R", "R'", "U", "U'", "D", "D'" };
        int moveCount = 30;

        for (int i = 0; i < moveCount; i++)
        {
            int randomIndex = Random.Range(0, moves.Count); // change moves.count to movecount and i to randomindex
            string randomMove = moves[randomIndex];

            yield return StartCoroutine(PerformMove(cubeState.left, cubeState.down, randomMove));
        }
        moving = false;
    }

    public void ShuffleOnClick()
    {
        StartCoroutine(Shuffle());
    }

    public List<GameObject> OppositeFace(List<GameObject> targetFace)
    {
        string name = targetFace[4].name;
        if (name == "Front")
            return cubeState.back;
        if (name == "Back")
            return cubeState.front;
        if (name == "Left")
            return cubeState.right;
        if (name == "Right")
            return cubeState.left;
        if (name == "Up")
            return cubeState.down;
        return cubeState.up;
    }
    public List<GameObject> LeftNeighbourFace(List<GameObject> targetFace, List<GameObject> upperFace)
    {
        string faceName = targetFace[4].name;
        string upperFaceName = upperFace[4].name;
        if (faceName == "Front") //verde
        {
            if (upperFaceName == "Up")
                return cubeState.left;
            if (upperFaceName == "Right")
                return cubeState.up;
            if (upperFaceName == "Down")
                return cubeState.right;
            if (upperFaceName == "Left")
                return cubeState.down;
        }

        if (faceName == "Left") //portocaliu
        {
            if (upperFaceName == "Up")
                return cubeState.back;
            if (upperFaceName == "Front")
                return cubeState.up;
            if (upperFaceName == "Down")
                return cubeState.front;
            if (upperFaceName == "Back")
                return cubeState.down;
        }

        if (faceName == "Back") //albastru
        {
            if (upperFaceName == "Up")
                return cubeState.right;
            if (upperFaceName == "Right")
                return cubeState.down;
            if (upperFaceName == "Down")
                return cubeState.left;
            if (upperFaceName == "Left")
                return cubeState.up;
        }

        if (faceName == "Right") //Rosu
        {
            if (upperFaceName == "Up")
                return cubeState.front;
            if (upperFaceName == "Front")
                return cubeState.down;
            if (upperFaceName == "Down")
                return cubeState.back;
            if (upperFaceName == "Back")
                return cubeState.up;
        }

        if (faceName == "Up")
        {
            if (upperFaceName == "Front")
                return cubeState.right;
            if (upperFaceName == "Left")
                return cubeState.front;
            if (upperFaceName == "Back")
                return cubeState.left;
            if (upperFaceName == "Right")
                return cubeState.back;
        }

        if (faceName == "Down")
        {
            if (upperFaceName == "Front")
                return cubeState.left;
            if (upperFaceName == "Left")
                return cubeState.back;
            if (upperFaceName == "Back")
                return cubeState.right;
            if (upperFaceName == "Right")
                return cubeState.front;
        }
        return null;
    }

    public List<GameObject> RightNeighbourFace(List<GameObject> targetFace, List<GameObject> upperFace)
    {
        return OppositeFace(LeftNeighbourFace(targetFace, upperFace));
    }

    public void HandleCubeSpeedDropdown(int value)
    {
        switch (value)
        {
            case 0: snapSpeed = 200; break;
            case 1: snapSpeed = 400; break;
            case 2: snapSpeed = 600; break;
        }
    }

    void RotateGroup_Learning(float angle, Vector3 axis, int count)
    {

        Quaternion rotation = Quaternion.AngleAxis(angle, axis);

        for (int i = 0; i < count; i++)
        {
            var subCube = _subCubes[i].transform;
            subCube.SetPositionAndRotation(rotation * _originalPositions[i], rotation * _originalOrientations[i]);
        }
    }

    public IEnumerator PerformMove_Learning(GameObject face, GameObject upperFace, string move)
    {
        if (!move.Equals("F") && !move.Equals("F'") && !move.Equals("F2"))
        {
            (GameObject newFace, GameObject newUpperFace, string newMove) = GetNewParams_Learning(face, upperFace, move);
            yield return StartCoroutine(PerformMove_Learning(newFace, newUpperFace, newMove));
            yield break;
        }
        // Step 4: Gather the 8-9 sub-cubes we need to rotate.
        (int sign, Vector3 rotationAxis) = GetRotationSpecs_Learning(face, move);
        Vector3 extents = Vector3.one - 0.9f * rotationAxis;
        extents *= 2.0f;
        int subCubeCount = Physics.OverlapBoxNonAlloc(face.transform.position, extents, _subCubes, Quaternion.identity, layerMask);

        for (int i = 0; i < subCubeCount; i++)
        {
            var subCube = _subCubes[i].transform;
            _originalPositions[i] = subCube.position;
            _originalOrientations[i] = subCube.rotation;
        }
        float angle = 0.0f;
        float snappedAngle = sign * 90.0f;

        while (angle != snappedAngle)
        {
            angle = Mathf.MoveTowards(angle, snappedAngle, snapSpeed * Time.deltaTime);

            RotateGroup_Learning(angle, rotationAxis, subCubeCount);
            yield return null;
        }
    }

    (GameObject, GameObject, string) GetNewParams_Learning(GameObject face, GameObject upperFace, string move)
    {
        if (move.Equals("U"))
            return (upperFace, face, "F'");
        if (move.Equals("U2"))
            return (upperFace, face, "F2");
        if (move.Equals("U'"))
            return (upperFace, face, "F");
        if (move.Equals("D"))
            return (OppositeFace_Learning(upperFace), face, "F'");
        if (move.Equals("D2"))
            return (OppositeFace_Learning(upperFace), face, "F2");
        if (move.Equals("D'"))
            return (OppositeFace_Learning(upperFace), face, "F");
        if (move.Equals("L"))
            return (LeftNeighbourFace_Learning(face, upperFace), upperFace, "F");
        if (move.Equals("L2"))
            return (LeftNeighbourFace_Learning(face, upperFace), upperFace, "F2");
        if (move.Equals("L'"))
            return (LeftNeighbourFace_Learning(face, upperFace), upperFace, "F'");
        if (move.Equals("R"))
            return (RightNeighbourFace_Learning(face, upperFace), upperFace, "F");
        if (move.Equals("R2"))
            return (RightNeighbourFace_Learning(face, upperFace), upperFace, "F2");
        if (move.Equals("R'"))
            return (RightNeighbourFace_Learning(face, upperFace), upperFace, "F'");
        return (null, null, null);
    }

    (int, Vector3) GetRotationSpecs_Learning(GameObject face, string move)
    {
        if (move.Equals("F"))
            return (GetDirection_Learning(face) * -1, GetRotationVector_Learning(face));
        if (move.Equals("F2"))
            return (GetDirection_Learning(face) * -2, GetRotationVector_Learning(face));
        if (move.Equals("F'"))
            return (GetDirection_Learning(face) * 1, GetRotationVector_Learning(face));

        return (0, Vector3.zero);
    }

    Vector3 GetRotationVector_Learning(GameObject face)
    {
        if (face.name.Equals("U") || face.name.Equals("D"))
        {
            return new Vector3(0, 1, 0);
        }
        if (face.name.Equals("L") || face.name.Equals("R"))
        {
            return new Vector3(1, 0, 0);
        }
        if (face.name.Equals("F") || face.name.Equals("B"))
        {
            return new Vector3(0, 0, 1);
        }
        return Vector3.zero;
    }

    int GetDirection_Learning(GameObject face)
    {
        if (face.name.Equals("U") || face.name.Equals("F") || face.name.Equals("L"))
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public GameObject OppositeFace_Learning(GameObject targetFace)
    {
        string name = targetFace.name;
        if (name == "F")
            return GameObject.Find("B");
        if (name == "B")
            return GameObject.Find("F");
        if (name == "L")
            return GameObject.Find("R");
        if (name == "R")
            return GameObject.Find("L");
        if (name == "U")
            return GameObject.Find("D");
        if (name == "D")
            return GameObject.Find("U");
        return null;
    }
    public GameObject LeftNeighbourFace_Learning(GameObject targetFace, GameObject upperFace)
    {
        string faceName = targetFace.name;
        string upperFaceName = upperFace.name;
        if (faceName == "F") //verde
        {
            if (upperFaceName == "U")
                return GameObject.Find("L");
            if (upperFaceName == "R")
                return GameObject.Find("U");
            if (upperFaceName == "D")
                return GameObject.Find("R");
            if (upperFaceName == "L")
                return GameObject.Find("D");
        }

        if (faceName == "L") //portocaliu
        {
            if (upperFaceName == "U")
                return GameObject.Find("B");
            if (upperFaceName == "F")
                return GameObject.Find("U");
            if (upperFaceName == "D")
                return GameObject.Find("F");
            if (upperFaceName == "B")
                return GameObject.Find("D");
        }

        if (faceName == "B") //albastru
        {
            if (upperFaceName == "U")
                return GameObject.Find("R");
            if (upperFaceName == "R")
                return GameObject.Find("D");
            if (upperFaceName == "D")
                return GameObject.Find("L");
            if (upperFaceName == "L")
                return GameObject.Find("U");
        }

        if (faceName == "R") //Rosu
        {
            if (upperFaceName == "U")
                return GameObject.Find("F");
            if (upperFaceName == "F")
                return GameObject.Find("D");
            if (upperFaceName == "D")
                return GameObject.Find("B");
            if (upperFaceName == "B")
                return GameObject.Find("U");
        }

        if (faceName == "U")
        {
            if (upperFaceName == "F")
                return GameObject.Find("R");
            if (upperFaceName == "L")
                return GameObject.Find("F");
            if (upperFaceName == "B")
                return GameObject.Find("L");
            if (upperFaceName == "R")
                return GameObject.Find("B");
        }

        if (faceName == "D")
        {
            if (upperFaceName == "F")
                return GameObject.Find("L");
            if (upperFaceName == "L")
                return GameObject.Find("B");
            if (upperFaceName == "B")
                return GameObject.Find("R");
            if (upperFaceName == "R")
                return GameObject.Find("F");
        }
        return null;
    }

    public GameObject RightNeighbourFace_Learning(GameObject targetFace, GameObject upperFace)
    {
        return OppositeFace_Learning(LeftNeighbourFace_Learning(targetFace, upperFace));
    }
}