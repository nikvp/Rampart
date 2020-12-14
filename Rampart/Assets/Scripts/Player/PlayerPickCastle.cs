using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickCastle : MonoBehaviour
{
    public GameObject castle;
    public Vector3 startingposition;
    PlayerMain pm;
    public bool castleAllreadySpawned = false;

    private void Awake() {
        pm = GetComponent<PlayerMain>();
    }
    private void Start() {
        startingposition = pm.GetComponent<PlayerMain>().startingPosition.position;
    }



    private void Update() {
        
        if (Input.GetButtonDown(pm.startButton)) {
            if (castleAllreadySpawned == true) {
                print("Castle allready spawned");
            } else {
                Instantiate(castle, startingposition, Quaternion.identity);
                castleAllreadySpawned = true;
                
            }

        }


    }





}


