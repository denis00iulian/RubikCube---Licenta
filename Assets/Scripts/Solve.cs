using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;

public class Solve : MonoBehaviour
{
    private CubeState cubeState;
    private CubeMovement cubeMovement;

    // Start is called before the first frame update
    void Start()
    {
        cubeState = FindObjectOfType<CubeState>();
        cubeMovement = FindObjectOfType<CubeMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SolveCube()
    {
        if (CubeMovement.moving) yield break;
        CubeMovement.moving = true;
        /*while (true)
        {
            yield return StartCoroutine(cubeMovement.Shuffle());
            yield return StartCoroutine(GetPieceToUpCross("U F", 7, cubeState.front));
            yield return StartCoroutine(GetPieceToUpCross("U L", 3, cubeState.left));
            yield return StartCoroutine(GetPieceToUpCross("U B", 1, cubeState.back));
            yield return StartCoroutine(GetPieceToUpCross("U R", 5, cubeState.right));
            if (!WhiteCrossDone())
            {
                //yield return StartCoroutine(SolveCube());
                yield break;
            }
            //yield return new WaitForSeconds(5);
            yield return StartCoroutine(GetPieceToUpCorner("U F R", 8, cubeState.front));
            yield return StartCoroutine(GetPieceToUpCorner("U F L", 6, cubeState.left));
            yield return StartCoroutine(GetPieceToUpCorner("U B L", 0, cubeState.back));
            yield return StartCoroutine(GetPieceToUpCorner("U B R", 2, cubeState.right));
            if (!FirstLayerDone())
            {
                //yield return StartCoroutine(SolveCube());
                yield break;
            }
            yield return StartCoroutine(SolveCube());
        }*/
        //yield return new WaitForSeconds(5);
        yield return StartCoroutine(GetPieceToUpCross("U F", 7, cubeState.front));
        yield return StartCoroutine(GetPieceToUpCross("U L", 3, cubeState.left));
        yield return StartCoroutine(GetPieceToUpCross("U B", 1, cubeState.back));
        yield return StartCoroutine(GetPieceToUpCross("U R", 5, cubeState.right));

        yield return StartCoroutine(GetPieceToUpCorner("U F R", 8, cubeState.front));
        yield return StartCoroutine(GetPieceToUpCorner("U F L", 6, cubeState.left));
        yield return StartCoroutine(GetPieceToUpCorner("U B L", 0, cubeState.back));
        yield return StartCoroutine(GetPieceToUpCorner("U B R", 2, cubeState.right));
        if (!FirstLayerDone())
        {
            //yield return StartCoroutine(SolveCube());
            yield break;
        }
        yield return StartCoroutine(SecondLayer());
        if (!FirstTwoLayersDone())
        {
            //yield return StartCoroutine(SolveCube()); 
            yield break;
        }
        yield return StartCoroutine(DownCross());
        yield return StartCoroutine(DownCorners());
        if (!DownCornersDone())
        {
            //yield return StartCoroutine(SolveCube()); 
            yield break;
        }
        //CubeMovement.snapSpeed = 200;
        //yield return new WaitForSeconds(10);
        yield return StartCoroutine(OrientDownCorners());
        //}*/
        CubeMovement.moving = false;
    }

    public IEnumerator GetPieceToUpCross(string pieceName, int whiteIndex, List<GameObject> targetFace)
    {
        cubeState.ReadState();
        //yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "D'")); //DEBUG
        yield return BringPieceToTargetFace(pieceName, whiteIndex, targetFace);
        targetFace = ReloadFace(targetFace);
        //bring it up
        if (targetFace[7].transform.parent.name.Equals(pieceName))
        {
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "F2"));
        }
        if (targetFace[5].transform.parent.name.Equals(pieceName))
        {
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "F'"));
        }
        if (targetFace[3].transform.parent.name.Equals(pieceName))
        {
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "F"));
        }
        targetFace = ReloadFace(targetFace);
        //orient if necessary
        if (targetFace[1].name.Equals("Up"))
        {
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "F"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "U'"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "R"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "U"));
        }
    }

    public IEnumerator BringPieceToTargetFace(string pieceName, int whiteIndex, List<GameObject> targetFace)
    {
        for (int i = 1; i < 9; i += 2)
        {
            if (targetFace[i].transform.parent.name.Equals(pieceName))
            {
                yield break;
            }
        }
        //the upper piece of the opposite of the target face
        if (cubeState.up[8 - whiteIndex].transform.parent.name.Equals(pieceName)) 
        {
            //bring it on the right neighbour face and continue
            yield return StartCoroutine(cubeMovement.PerformMove(cubeMovement.RightNeighbourFace(targetFace, cubeState.up), cubeState.up, "R'"));
        }
        targetFace = ReloadFace(targetFace);
        //the right edge piece of the right side
        if (cubeMovement.RightNeighbourFace(targetFace, cubeState.up)[5].transform.parent.name.Equals(pieceName))
        {
            //bring it on the target face and stop
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "U'"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "R'"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "U"));
            yield break;
        }
        //the left edge piece of the left side
        if (cubeMovement.LeftNeighbourFace(targetFace, cubeState.up)[3].transform.parent.name.Equals(pieceName))
        {
            //bring it on the target face  and stop
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "U"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "L"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "U'"));
            yield break;
        }
        //the top piece of the right side
        if (cubeMovement.RightNeighbourFace(targetFace, cubeState.up)[1].transform.parent.name.Equals(pieceName))
        {
            //bring it on the target face  and stop
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "R'"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "F'"));
            yield break;
        }
        //the top piece of the left side
        if (cubeMovement.LeftNeighbourFace(targetFace, cubeState.up)[1].transform.parent.name.Equals(pieceName))
        {
            //bring it on the target face  and stop
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "L"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "F"));
            yield break;
        }
        int targetIndex = (whiteIndex == 7 || whiteIndex == 1)? 8 - whiteIndex : whiteIndex;
        //down piece of the down side
        while (!cubeState.down[targetIndex].transform.parent.name.Equals(pieceName)) 
        {
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "D"));
        }
    }

    public IEnumerator GetPieceToUpCorner(string pieceName, int whiteIndex, List<GameObject> targetFace)
    {
        if (targetFace[2].name.Equals(targetFace[4].name)
            && cubeState.up[whiteIndex].name.Equals("Up"))
        {
            yield break;
        }
        if (cubeState.up[whiteIndex].transform.parent.name.Equals(pieceName))
        {
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "R'"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "D'"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "R"));
        }
        if (targetFace[0].transform.parent.name.Equals(pieceName))
        {
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "L"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "D"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "L'"));
        }
        if (cubeMovement.RightNeighbourFace(targetFace, cubeState.up)[2].transform.parent.name.Equals(pieceName))
        {
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "R"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "D'"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "R'"));
        }
        if (cubeMovement.LeftNeighbourFace(targetFace, cubeState.up)[0].transform.parent.name.Equals(pieceName))
        {
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "L'"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "D"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "L"));
        }
        targetFace = ReloadFace(targetFace);
        while (!targetFace[8].transform.parent.name.Equals(pieceName))
        {
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "D"));
            targetFace = ReloadFace(targetFace);
        }

        if (targetFace[8].name.Equals(targetFace[4].name))
        {
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "R'"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "D'"));
            yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "R"));
        }
        else
        {
            if (targetFace[8].name.Equals("Up"))
            {
                yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "F"));
                yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "D"));
                yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "F'"));
            }
            else
            {
                yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "R2"));
                yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "D'"));
                yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "R2"));
                yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "D"));
                yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.up, "R2"));
            }
        }
    }

    public IEnumerator SecondLayer()
    {
        List<string> sideFaces = new() { "Front", "Left", "Right", "Back" };
        while (!FirstTwoLayersDone(true))
        {
            for (int i = 1; i < 9; i+=2)
            {
                int sideFaceIndex = (i - 1) / 2; //coresponding face from list for each down index
                string sideFacePosition = sideFaces[sideFaceIndex];
                string downFacePosition = cubeState.down[i].name;
                string targetPiecePosition = GetFaceByPosition(sideFacePosition)[7].name;
                List<GameObject> targetFace = GetFaceByPosition(targetPiecePosition); // only for moves; we need to get the reference of the cubestate.face so that it is updated after a move
                if (!targetPiecePosition.Equals("Down") && !downFacePosition.Equals("Down"))
                {
                    string pieceName = cubeState.down[i].transform.parent.name;
                    //position on the right spot
                    while (!GetFaceByPosition(targetPiecePosition)[7].transform.parent.name.Equals(pieceName))
                    {
                        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetPiecePosition), cubeState.down, "U"));
                    }
                    if (AreRightNeighbours(targetPiecePosition, downFacePosition))
                    {
                        yield return SecondLayer_PlaceToRight(targetFace);
                    }
                    else if (AreLeftNeighbours(targetPiecePosition, downFacePosition))
                    {
                        yield return SecondLayer_PlaceToLeft(targetFace);
                    }
                    i = 1; //start over, cubeState.down was modified
                }
            }
            // no piece on the 3rd layer that can be moved
            if (FirstTwoLayersDone(true))
            {
                yield break;
            }
            else
            {
                //search for wrong placed pieces on the 2nd layer edges
                List<GameObject> targetFace = null;
                if (!(cubeState.front[3].name.Equals("Front") && cubeState.left[5].name.Equals("Left")))
                {
                    targetFace = cubeState.front; //facem mutarea de scos piesa din dreapta
                }
                else if (!(cubeState.left[3].name.Equals("Left") && cubeState.back[5].name.Equals("Back")))
                {
                    targetFace = cubeState.left; //facem mutarea de scos piesa din dreapta
                }
                else if (!(cubeState.back[3].name.Equals("Back") && cubeState.right[5].name.Equals("Right")))
                {
                    targetFace = cubeState.back; //facem mutarea de scos piesa din dreapta
                }
                else if (!(cubeState.right[3].name.Equals("Right") && cubeState.front[5].name.Equals("Front")))
                {
                    targetFace = cubeState.right; //facem mutarea de scos piesa din dreapta
                }
                if (!(targetFace == null))
                {
                    yield return SecondLayer_PlaceToRight(targetFace);
                }
            }
        }
    }

    public IEnumerator DownCorners()
    {
        //key = face, value = pieceName-ul care ar trebui sa se afle la indexul 6 pe acea fata (in coltul din dreapta sus, daca avem fata galbena deasupra
        List<(string face, string pieceName)> corners = new() { 
            ("Front", "D F L"), 
            ( "Right", "D F R" ), 
            ( "Back", "D B R" ),
            ( "Left", "D B L" ) };
        int noOfCorrectCorners = 0;
        for (int i = 0; i < corners.Count; i++)
        {
            if (GetFaceByPosition(corners[i].face)[6].transform.parent.name.Equals(corners[i].pieceName))
            {
                noOfCorrectCorners++;
            }
        }
        if (noOfCorrectCorners == 4) yield break;
        if (noOfCorrectCorners == 0)
        {
            yield return StartCoroutine(DownCornerIteration("Front"));
        }
        //at this point, there is a single correct piece, and with one iteration, the cube will have all the pieces correctly placed, but not necessarly correctly oriented
        //search for the correctly placed piece
        for (int i = 0; i < corners.Count; i++)
        {
            if (GetFaceByPosition(corners[i].face)[6].transform.parent.name.Equals(corners[i].pieceName))
            {
                while (!DownCornersDone(true))
                    yield return StartCoroutine(DownCornerIteration(corners[i].face));
                yield break;
            }
        }
        yield break;
    }

    public IEnumerator DownCornerIteration(string targetFace)
    {
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "U"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "R"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "U'"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "L'"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "U"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "R'"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "U'"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "L"));
    }

    public IEnumerator DownCross()
    {
        int state = GetDownCrossState();
        for (int i = state; i < 3; i++)
        {
            string targetFacePosition = GetTargetFaceForDownCross(i);
            yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFacePosition), cubeState.down, "F"));
            yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFacePosition), cubeState.down, "R"));
            yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFacePosition), cubeState.down, "U"));
            yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFacePosition), cubeState.down, "R'"));
            yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFacePosition), cubeState.down, "U'"));
            yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFacePosition), cubeState.down, "F'"));
        }
        //CubeMovement.snapSpeed = 30;
        //yield return new WaitForSeconds(20);
        //position it correctly
        while (GetCorrectDownEdges() < 2)
        {
            yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition("Front"), cubeState.down, "U"));
        }
        if (GetFaceByPosition("Back")[7].name.Equals("Back")
            && GetFaceByPosition("Left")[7].name.Equals("Left")
            && GetFaceByPosition("Right")[7].name.Equals("Right"))
        {
            yield break;
        }
        List<string> wrongFaces = GetWrongDownEdges();
        if (cubeMovement.OppositeFace(GetFaceByPosition(wrongFaces[0]))[4].name.Equals(wrongFaces[1]))
        {
            if (wrongFaces.Contains("Front"))
            {
                yield return StartCoroutine(SwapDownEdge("Back"));
                yield return StartCoroutine(SwapDownEdge("Right"));
                yield return StartCoroutine(SwapDownEdge("Back"));
            }
            else
            {
                yield return StartCoroutine(SwapDownEdge("Right"));
                yield return StartCoroutine(SwapDownEdge("Front"));
                yield return StartCoroutine(SwapDownEdge("Right"));
            }
        }
        else if (wrongFaces[0].Equals("Front"))
        {
            if (wrongFaces[1].Equals("Right"))
            {
                yield return StartCoroutine(SwapDownEdge("Front"));
            }
            else
            {
                yield return StartCoroutine(SwapDownEdge(wrongFaces[1]));
            }
        }
        while (GetCorrectDownEdges() != 4)
        {
            yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition("Front"), cubeState.down, "U"));
        }
    }

    public int GetCorrectDownEdges()
    {
        int correctFaces = 0;
        if (cubeState.front[7].name.Equals("Front")) correctFaces++;
        if (cubeState.back[7].name.Equals("Back")) correctFaces++;
        if (cubeState.left[7].name.Equals("Left")) correctFaces++;
        if (cubeState.right[7].name.Equals("Right")) correctFaces++;
        return correctFaces;
    }

    public List<string> GetWrongDownEdges()
    {
        List<string> wrongFaces = new();
        if (!cubeState.front[7].name.Equals("Front")) wrongFaces.Add("Front"); 
        if (!cubeState.left[7].name.Equals("Left")) wrongFaces.Add("Left");
        if (!cubeState.back[7].name.Equals("Back")) wrongFaces.Add("Back");
        if (!cubeState.right[7].name.Equals("Right")) wrongFaces.Add("Right");
        return wrongFaces;
    }

    public IEnumerator SwapDownEdge(string targetFace)
    {
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "R"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "U"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "R'"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "U"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "R"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "U2"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "R'"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "U"));
    }

    public int GetDownCrossState()
    {
        if (cubeState.down[1].name.Equals("Down") 
            && cubeState.down[3].name.Equals("Down") 
            && cubeState.down[5].name.Equals("Down") 
            && cubeState.down[7].name.Equals("Down"))
        {
            return 3;
        }
        if ((cubeState.down[1].name.Equals("Down") && cubeState.down[7].name.Equals("Down"))
            || (cubeState.down[3].name.Equals("Down") && cubeState.down[5].name.Equals("Down"))
            )
        {
            return 2;
        }
        for (int i = 1; i < 9; i+=2)
        {
            if (cubeState.down[i].name.Equals("Down"))
            {
                return 1;
            }
        }
        return 0;
    }

    public string GetTargetFaceForDownCross(int state)
    {
        if (state == 0)
        {
            return "Front"; //could be any face
        }
        else if (state == 1)
        {
            if (cubeState.down[1].name.Equals("Down") && cubeState.down[3].name.Equals("Down")) return "Back";
            if (cubeState.down[1].name.Equals("Down") && cubeState.down[5].name.Equals("Down")) return "Left";
            if (cubeState.down[5].name.Equals("Down") && cubeState.down[7].name.Equals("Down")) return "Front";
            if (cubeState.down[7].name.Equals("Down") && cubeState.down[3].name.Equals("Down")) return "Right";
        }
        if ( state == 2)
        {
            if (cubeState.down[1].name.Equals("Down")) return "Left"; //left or right
            if (cubeState.down[3].name.Equals("Down")) return "Front"; //front or back
        }
        return null;
    }

    public IEnumerator SecondLayer_PlaceToRight(List<GameObject> targetFace)
    {
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "U"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "R"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "U'"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "R'"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "U'"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "F'"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "U"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "F"));
    }

    public IEnumerator SecondLayer_PlaceToLeft(List<GameObject> targetFace)
    {
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "U'"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "L'"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "U"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "L"));

        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "U"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "F"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "U'"));
        yield return StartCoroutine(cubeMovement.PerformMove(targetFace, cubeState.down, "F'"));
    }

    public IEnumerator OrientDownCorners()
    {
        List<(string face, int downIndex, string pieceName)> wrongCorners = FindWrongOrientedDownCorner();
        string face = wrongCorners[0].face;
        for (int i = 0;  i < wrongCorners.Count - 1; i++)
        {
            while(!cubeState.down[wrongCorners[0].downIndex].name.Equals("Down"))
            {
                yield return StartCoroutine(OrientDownCornersIteration(face));
            }
            while (!GetFaceByPosition(face)[6].transform.parent.name.Equals(wrongCorners[i + 1].pieceName))
            {
                print("side " + GetFaceByPosition(face)[6].transform.parent.name);
                print("down" + wrongCorners[i + 1].pieceName);
                //yield return new WaitForSeconds(5);
                yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(face), cubeState.down, "U"));
            }
        }
        while (!cubeState.down[wrongCorners[0].downIndex].name.Equals("Down"))
        {
            yield return StartCoroutine(OrientDownCornersIteration(face));
        }
        while (!CubeDone())
        {
            yield return StartCoroutine(cubeMovement.PerformMove(cubeState.front, cubeState.down, "U"));
        }
        yield break;
    }

    public IEnumerator OrientDownCornersIteration(string targetFace)
    {
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "R'"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "D'"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "R"));
        yield return StartCoroutine(cubeMovement.PerformMove(GetFaceByPosition(targetFace), cubeState.down, "D"));
    }

    public List<(string, int, string)> FindWrongOrientedDownCorner()
    {
        List<(string face, int downIndex, string pieceName)> wrongCorners = new();
        if (!(cubeState.front[6].name.Equals("Front") && cubeState.left[8].name.Equals("Left"))) wrongCorners.Add(("Front", 0, cubeState.front[6].transform.parent.name));
        if (!(cubeState.left[6].name.Equals("Left") && cubeState.back[8].name.Equals("Back"))) wrongCorners.Add(("Left", 6, cubeState.left[6].transform.parent.name));
        if (!(cubeState.back[6].name.Equals("Back") && cubeState.right[8].name.Equals("Right"))) wrongCorners.Add(("Back", 8, cubeState.back[6].transform.parent.name));
        if (!(cubeState.right[6].name.Equals("Right") && cubeState.front[8].name.Equals("Front"))) wrongCorners.Add(("Right", 2, cubeState.right[6].transform.parent.name));
        return wrongCorners;
    }

    public void SolveCubeOnClick()
    {
        StartCoroutine(SolveCube());
    }

    List<GameObject> ReloadFace(List<GameObject> face)
    {
        if (face[4].name.Equals("Front"))
            return cubeState.front;
        if (face[4].name.Equals("Left"))
            return cubeState.left;
        if (face[4].name.Equals("Back"))
            return cubeState.back;
        if (face[4].name.Equals("Right"))
            return cubeState.right;
        if (face[4].name.Equals("Up"))
            return cubeState.up;
        return cubeState.down;
    }

    public bool AreRightNeighbours(string firstFace, string secondFace)
    {
        if ((firstFace.Equals("Front") && secondFace.Equals("Left"))
            || (firstFace.Equals("Left") && secondFace.Equals("Back"))
            || (firstFace.Equals("Back") && secondFace.Equals("Right"))
            || (firstFace.Equals("Right") && secondFace.Equals("Front")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AreLeftNeighbours(string firstFace, string secondFace)
    {
        if ((firstFace.Equals("Left") && secondFace.Equals("Front"))
            || (firstFace.Equals("Front") && secondFace.Equals("Right"))
            || (firstFace.Equals("Right") && secondFace.Equals("Back"))
            || (firstFace.Equals("Back") && secondFace.Equals("Left")))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool WhiteCrossDone()
    {
        if (!(cubeState.front[1].name.Equals(cubeState.front[4].name) && cubeState.up[7].name.Equals("Up"))
            || !(cubeState.right[1].name.Equals(cubeState.right[4].name) && cubeState.up[5].name.Equals("Up"))
            || !(cubeState.left[1].name.Equals(cubeState.left[4].name) && cubeState.up[3].name.Equals("Up"))
            || !(cubeState.back[1].name.Equals(cubeState.back[4].name) && cubeState.up[1].name.Equals("Up")))
        {
            print("Something went wrong the white cross");
            return false;
        }
        return true;
    }

    public bool FirstLayerDone()
    {
        List<List<GameObject>> sideFaces = new() { cubeState.front, cubeState.left, cubeState.back, cubeState.right };
        foreach (var face in sideFaces)
        {
            for (int i = 0; i < 3; i++)
            {
                if (!face[i].name.Equals(face[4].name))
                {
                    print("Something went wrong on the first layer");
                    return false;
                }
            }
        }
        for (int i = 0; i < cubeState.up.Count; i++)
        {
            if (!cubeState.up[i].name.Equals("Up"))
            {
                print("Something went wrong on the first layer");
                return false;
            }
        }
        return true;
    }

    public bool FirstTwoLayersDone(bool inProgress = false)
    {
        if (!FirstLayerDone()) { return false; }
        List<List<GameObject>> sideFaces = new() { cubeState.front, cubeState.left, cubeState.back, cubeState.right };
        foreach (var face in sideFaces)
        {
            for (int i = 3; i < 6; i++)
            {
                if (!face[i].name.Equals(face[4].name))
                {
                    if (!inProgress)
                        print("Something went wrong on the second layer");
                    return false;
                }
            }
        }
        return true;
    }

    public bool DownCornersDone(bool inProgress = false)
    {
        List<(string face, string pieceName)> corners = new() {
            ("Front", "D F L"),
            ( "Right", "D F R" ),
            ( "Back", "D B R" ),
            ( "Left", "D B L" ) };
        int noOfCorrectCorners = 0;
        for (int i = 0; i < corners.Count; i++)
        {
            if (GetFaceByPosition(corners[i].face)[6].transform.parent.name.Equals(corners[i].pieceName))
            {
                noOfCorrectCorners++;
            }
        }
        if (noOfCorrectCorners != 4 && !inProgress) print("Something went wrong when placing the down corners");
        return noOfCorrectCorners == 4;
    }

    public bool CubeDone()
    {
        if (!FirstTwoLayersDone()) return false;
        for (int i = 6;i < 9; i++)
        {
            if (!cubeState.front[i].name.Equals("Front")
                || !cubeState.left[i].name.Equals("Left")
                || !cubeState.back[i].name.Equals("Back")
                || !cubeState.right[i].name.Equals("Right"))
                return false;
        }
        return true;
    }

    public List<GameObject> GetFaceByPosition(string position)
    {
        if (position.Equals("Up")) return cubeState.up;
        if (position.Equals("Front")) return cubeState.front;
        if (position.Equals("Left")) return cubeState.left;
        if (position.Equals("Back")) return cubeState.back;
        if (position.Equals("Right")) return cubeState.right;
        if (position.Equals("Down")) return cubeState.down;
        return null;
    }
}
