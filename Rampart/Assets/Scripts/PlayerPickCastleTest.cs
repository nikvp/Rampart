using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickCastleTest : MonoBehaviour
{
    int id;
    Vector3 startingPosition;  //is the middle point of the castle
    public GameObject castle;
    public GameObject castleWalls;
    public GameObject indicator;
    private Vector3 sw;
    private Vector3 se;
    private Vector3 nw;
    private Vector3 ne;
    public bool castleSpawned = false;

    private List<Vector3> castleWallsPositions = new List<Vector3>();

    void Start() {
        startingPosition = GetComponent<Transform>().position;
        //transform should be picked from given positions
        id = GetComponent<PlayerMain>().id;
        sw = startingPosition + new Vector3(-3.5f, 0, -3.5f);
        se = startingPosition + new Vector3(3.5f, 0, -3.5f);
        nw = startingPosition + new Vector3(-3, 0, 3);
        ne = startingPosition + new Vector3(3, 0, 3);

        WestWall();
        NorthWall();
        EastWall();
        SouthhWall();


    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Start" + id)) {
            print("Player " + id + " start pressed");
            if (castleSpawned == false) {
                Instantiate(castle, startingPosition, Quaternion.identity);
                foreach (Vector3 position in castleWallsPositions) {
                    Instantiate(castleWalls, position, Quaternion.identity);
                    Instantiate(indicator, position, Quaternion.identity);

                }
            } else {
                print("Castle already spawned. Waiting for countdown.");
            }
        }
    }

    private void WestWall() {
        //locate all positions of the southwest wall
        for (int i = 0; i < 7; i++) {
            Vector3 position = sw + new Vector3(0, 0, i);
            castleWallsPositions.Add(position);
        }
    }
    private void NorthWall() {
        //locate all positions of the southwest wall
        for (int i = 0; i < 7; i++) {
            Vector3 position = nw + new Vector3(i, 0, 0);
            castleWallsPositions.Add(position);
        }
    }
    private void EastWall() {
        //locate all positions of the southwest wall
        for (int i = 0; i < 7; i++) {
            Vector3 position = ne + new Vector3(0, 0, -i);
            castleWallsPositions.Add(position);
        }
    }
    private void SouthhWall() {
        //locate all positions of the southwest wall
        for (int i = 0; i < 7; i++) {
            Vector3 position = se + new Vector3(-i, 0, 0);
            castleWallsPositions.Add(position);
        }
    }
}
//starting position has to be set so that its at grid point (x-1, 0, z-1)
//that way the castle sprite is correctly positioned and takes 4 squares
//the wall have to be placed 7 units away from the starting positions so that they
//occupy one square correctly
