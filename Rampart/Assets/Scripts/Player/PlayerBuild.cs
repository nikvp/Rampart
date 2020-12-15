using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuild : MonoBehaviour
{
    PlayerMain pm;
    [SerializeField] Transform cursor;
    public float cursorSpeed = 3f;
    public List<GameObject> tetrisPieces = new List<GameObject>();
    Vector3 targetPosition;


    void Awake() {
        pm = GetComponent<PlayerMain>();

    }
    void OnEnable() {
        print("Enabled" + pm.id);
        //enable cursor object
    }
    private void Start() {
        transform.position += pm.startingPosition.position;

    }

    void OnDisable() {
        print("Disabled" + pm.id);
        //disable cursor
    }

    // Update is called once per frame
    void Update() {
        //movement
        var horiz = Input.GetAxis(pm.horizontalAxisName);
        var vert = Input.GetAxis(pm.verticalAxisName);
        cursor.position += horiz * Vector3.right * cursorSpeed * Time.deltaTime;
        cursor.position += vert * Vector3.back * cursorSpeed * Time.deltaTime;


    }

    void PickRandomTetris() {

    }

    void CheckForPlacement(Vector3 target) {

        //check if out of bounds

        //first check if own side

        //check if there is a wall

        // check if there is a turret

        // checkk if there is the castle


    }
}
