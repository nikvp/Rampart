using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickCastle : MonoBehaviour
{

    PlayerMain pm;
    Vector3 startingposition;
    public bool castleBuilt = false;
    public GameObject castle;
    public GameObject indicator;
    Vector3 southwestCorner;
    public LayerMask castleLayer;


    private void Awake() {

        pm = GetComponent<PlayerMain>();


    }

    private void Start() {

        startingposition = pm.GetComponent<PlayerMain>().startingPosition.position;
        transform.position = startingposition;
        southwestCorner = (startingposition + new Vector3(-2, 0, -3));
    }

    private void Update() {
        if (Input.GetButtonDown(pm.startButton)) {
            if (castleBuilt == true) {
                print("Castle allready built");
            } else {
                Instantiate(castle, startingposition, Quaternion.identity);
                castleBuilt = true;
                SpawnIndicators();

            }
        }
    }

    void SpawnIndicators() {
        for (int x = 0; x < 6; x++) {
            for (int y = 0; y < 6; y++) {
                Vector3 pos = (southwestCorner + new Vector3(x, 0, y));
                var check = Physics.OverlapBox(pos, new Vector3(0.2f, 0.2f, 0.2f),
                                        Quaternion.identity, castleLayer);
                if (check.Length > 0) { continue; } else {
                    Instantiate(indicator, pos, Quaternion.identity);
                }
            }
        }
    }
}


