using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickCastle : MonoBehaviour
{

    PlayerMain pm;
    Vector3 startingposition;
    public bool castleBuilt = false;
    public GameObject castle;


    private void Awake() {

        pm = GetComponent<PlayerMain>();


    }

    private void Start() {

        startingposition = pm.GetComponent<PlayerMain>().startingPosition.position;
        transform.position = startingposition;

    }

    private void Update() {
        if (Input.GetButtonDown(pm.startButton)) {
            if (castleBuilt == true) {
                print("Castle allready built");
            } else {
                Instantiate(castle, startingposition, Quaternion.identity);
                castleBuilt = true;

            }
        }
    }
}


