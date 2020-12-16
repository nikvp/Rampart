﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuild : MonoBehaviour
{
    PlayerMain pm;
    [SerializeField] Transform cursor;
    public float cursorSpeed = 3f;
    public List<GameObject> tetrisPieces = new List<GameObject>();
    Vector3 targetPosition;
    public LayerMask unusableTerrain;
    public LayerMask walls;
    public LayerMask castle;
    public LayerMask inside;
    public LayerMask turret;
    bool clear = false;
    public bool placingTime = true;
    public float timeBetweenBlocks = 0.2f;
    GameObject tetrisPiece;
    GameObject currentObject;
    List<Vector3> checkSurroundingSpots = new List<Vector3>();
    public GameObject actualWallPiece;


    void Awake() {
        pm = GetComponent<PlayerMain>();

    }
    void OnEnable() {
        print("Enabled" + pm.id);
        //enable cursor object
    }
    private void Start() {
        cursor.position += new Vector3(0, 0, 0);
        NewTetrisPiece();

    }

    void OnDisable() {
        print("Disabled" + pm.id);
        //disable cursor
    }

    void NewTetrisPiece() {
        tetrisPiece = tetrisPieces[Random.Range(0, tetrisPieces.Count)];
        currentObject = Instantiate(tetrisPiece, transform.position, Quaternion.identity);

        //put all the other tetris vectors in the vector list for checks
        if (tetrisPiece = tetrisPieces[0]) {
            checkSurroundingSpots.Add(new Vector3(-1, 0, 0));

        } else if (tetrisPiece = tetrisPieces[1]) {
            checkSurroundingSpots.Add(new Vector3(-1, 0, 0));
            checkSurroundingSpots.Add(new Vector3(1, 0, 0));

        } else if (tetrisPiece = tetrisPieces[2]) {
            checkSurroundingSpots.Add(new Vector3(-1, 0, 0));
            checkSurroundingSpots.Add(new Vector3(0, 0, 1));

        } else if (tetrisPiece = tetrisPieces[3]) {
            checkSurroundingSpots.Add(new Vector3(1, 0, 0));
            checkSurroundingSpots.Add(new Vector3(0, 1, 0));
            checkSurroundingSpots.Add(new Vector3(0, 2, 0));

            //nr 4 has no neighbouring wallpieces

        } else if (tetrisPiece = tetrisPieces[5]) {
            checkSurroundingSpots.Add(new Vector3(-1, 0, 1));
            checkSurroundingSpots.Add(new Vector3(0, 0, 1));
            checkSurroundingSpots.Add(new Vector3(1, 0, 0));

        } else if (tetrisPiece = tetrisPieces[6]) {
            checkSurroundingSpots.Add(new Vector3(-1, 0, 0));
            checkSurroundingSpots.Add(new Vector3(0, 0, 1));
            checkSurroundingSpots.Add(new Vector3(1, 0, 1));

        } else if (tetrisPiece = tetrisPieces[7]) {
            checkSurroundingSpots.Add(new Vector3(-1, 0, 0));
            checkSurroundingSpots.Add(new Vector3(1, 0, 0));
            checkSurroundingSpots.Add(new Vector3(0, 0, -1));
        }
    }
    

    // Update is called once per frame
    void Update() {
        //movement
        var horiz = Input.GetAxis(pm.horizontalAxisName);
        var vert = Input.GetAxis(pm.verticalAxisName);
        cursor.position += horiz * Vector3.right * cursorSpeed * Time.deltaTime;
        cursor.position += vert * Vector3.back * cursorSpeed * Time.deltaTime;
        currentObject.transform.position += cursor.position;

        while (placingTime == true) {
            if (Input.GetButtonDown(pm.actionButton)) {
                //check target location for clear space
                var t = Utility.GetNearestPointOnGrid(cursor.position);
                targetPosition = new Vector3(t.x, 0, t.y);
                CheckGridPoint(targetPosition);
                foreach(Vector3 nextV in checkSurroundingSpots) {
                    CheckGridPoint(targetPosition += nextV);
                }
                if (clear == true) {

                    //spawn 1x1 walls on every gridpoint the tetrispiece occupies
                    //spawn an indicator aswell
                    Instantiate(actualWallPiece, targetPosition, Quaternion.identity);
                    if (checkSurroundingSpots.Count > 0) {
                        foreach (Vector3 vector in checkSurroundingSpots) {
                            Instantiate(actualWallPiece, vector, Quaternion.identity);
                        }
                    }
                    // destroy tetrispiece and clear tetris vectorlist
                    Destroy(currentObject);
                    checkSurroundingSpots.Clear();
                    //get new tetris piece and  vectors
                    NewTetrisPiece();
                    clear = false;
                } else {
                    print("Can't place wall here");
                }
                
            }
        }
    }

    void CheckGridPoint(Vector3 target) {

        //check if out of bounds
        var b = Physics.OverlapBox(target, (Vector3.one * 0.5f), Quaternion.identity, unusableTerrain);
        //first check if own side

        //check if there is a wall
        var w = Physics.OverlapBox(target, (Vector3.one * 0.5f), Quaternion.identity, walls);
        // check if there is a turret
        var t = Physics.OverlapBox(target, (Vector3.one * 0.5f), Quaternion.identity, turret);
        // checkk if there is the castle
        var c = Physics.OverlapBox(target, (Vector3.one * 0.5f), Quaternion.identity, castle);

        if ((b.Length > 0) || (w.Length > 0) || (t.Length > 0) || (c.Length > 0)) {
            clear = false;
        } else {
            clear = true; 
        }
    }
}
