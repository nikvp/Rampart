using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuild : MonoBehaviour
{
    PlayerMain pm;
    [SerializeField] Transform cursor;
    public float cursorSpeed = 3f;
    public List<GameObject> tetrisPieces;
    Vector3 targetPosition;
    public LayerMask walls;
    public LayerMask castle;
    public LayerMask inside;
    public LayerMask turret;
    GameObject tetrisPiece;
    GameObject currentObject;
    List<Vector3> checkSurroundingSpots = new List<Vector3>();
    public GameObject actualWallPiece;
    Vector3 eulerVector;
    MapData map;
    List<Vector3> obstructedVectors;
    bool clear = false;

    void OnEnable() {
        pm = GetComponent<PlayerMain>();
        map = FindObjectOfType<MapData>();
        cursor.position = pm.startingPosition.position;
        NewTetrisPiece();

    }

    private void Update() {
        var horiz = Input.GetAxis(pm.horizontalAxisName);
        var vert = Input.GetAxis(pm.verticalAxisName);
        cursor.position += horiz * Vector3.right * cursorSpeed * Time.deltaTime;
        cursor.position += vert * Vector3.forward * cursorSpeed * Time.deltaTime;

        currentObject.transform.position = cursor.position;

        if (Input.GetButtonDown(pm.actionButton)) {
            //check target location for clear space
            var t = Utility.GetNearestPointOnGrid(cursor.position);
            targetPosition = new Vector3(t.x, 0, t.y);
            CheckGridPoint(targetPosition);
            foreach (Vector3 nextV in checkSurroundingSpots) {
                CheckGridPoint(targetPosition += nextV);

            }
            if (clear == true) {
                currentObject.transform.position = targetPosition;
                //spawn 1x1 walls on every gridpoint the tetrispiece occupies
                //spawn an indicator aswell
                //Instantiate(tetrisPiece, targetPosition, Quaternion.Euler(-90, 0, 90));
                // destroy tetrispiece and clear tetris vectorlist
                NewTetrisPiece();
                checkSurroundingSpots.Clear();
                obstructedVectors.Clear();
                //get new tetris piece and  vectors
            } else {
                print("Can't place wall here");
            }

        }
    }



    void NewTetrisPiece() {
        tetrisPiece = tetrisPieces[Random.Range(0, tetrisPieces.Count)];
        //put all the other tetris vectors in the vector list for checks
        if (tetrisPiece == tetrisPieces[0]) {
            checkSurroundingSpots.Add(new Vector3(-1, 0, 0));
            eulerVector = new Vector3(-90, 0, 90);

        } else if (tetrisPiece == tetrisPieces[1]) {
            checkSurroundingSpots.Add(new Vector3(-1, 0, 0));
            checkSurroundingSpots.Add(new Vector3(1, 0, 0));
            eulerVector = new Vector3(-90, 0, 90);

        } else if (tetrisPiece == tetrisPieces[2]) {
            checkSurroundingSpots.Add(new Vector3(-1, 0, 0));
            checkSurroundingSpots.Add(new Vector3(0, 0, 1));
            eulerVector = new Vector3(-90, 0, 90);

        } else if (tetrisPiece == tetrisPieces[3]) {
            checkSurroundingSpots.Add(new Vector3(1, 0, 0));
            checkSurroundingSpots.Add(new Vector3(0, 1, 0));
            checkSurroundingSpots.Add(new Vector3(0, 2, 0));
            eulerVector = new Vector3(-90, 0, -180);

        } else if (tetrisPiece == tetrisPieces[4]) {
            //nr 4 has no neighbouring wallpieces
            eulerVector = new Vector3(-90, 0, 90);

        } else if (tetrisPiece == tetrisPieces[5]) {
            checkSurroundingSpots.Add(new Vector3(-1, 0, 1));
            checkSurroundingSpots.Add(new Vector3(0, 0, 1));
            checkSurroundingSpots.Add(new Vector3(1, 0, 0));
            eulerVector = new Vector3(-90, 0, -180);

        } else if (tetrisPiece == tetrisPieces[6]) {
            checkSurroundingSpots.Add(new Vector3(-1, 0, 0));
            checkSurroundingSpots.Add(new Vector3(0, 0, 1));
            checkSurroundingSpots.Add(new Vector3(1, 0, 1));
            eulerVector = new Vector3(-90, 0, -180);

        } else if (tetrisPiece == tetrisPieces[7]) {
            checkSurroundingSpots.Add(new Vector3(-1, 0, 0));
            checkSurroundingSpots.Add(new Vector3(1, 0, 0));
            checkSurroundingSpots.Add(new Vector3(0, 0, -1));
            eulerVector = new Vector3(-90, 0, 90);
        }
        
        currentObject = Instantiate(tetrisPiece, cursor.position, Quaternion.Euler(eulerVector));

    }

    void CheckGridPoint(Vector3 target) {

        //check if out of bounds
        //first check if own side
        var b = map.AreaOwnerAtPoint(Utility.GetNearestPointOnGrid(target));
        //check if there is a wall
        var w = Physics.OverlapBox(target, (Vector3.one * 0.1f), Quaternion.identity, walls);
        // check if there is a turret
        var t = Physics.OverlapBox(target, (Vector3.one * 0.1f), Quaternion.identity, turret);
        // checkk if there is the castle
        var c = Physics.OverlapBox(target, (Vector3.one * 0.1f), Quaternion.identity, castle);

        if ((w.Length > 0) || (t.Length > 0) || (c.Length > 0) || (b != pm.id)) {
            clear = false;
        } else {
            clear = true;
        }
    }
}
