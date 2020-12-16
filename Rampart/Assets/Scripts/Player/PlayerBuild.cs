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
    public LayerMask unusableTerrain;
    public LayerMask walls;
    public LayerMask castle;
    public LayerMask inside;
    public LayerMask turret;
    bool clear = false;
    public bool placingTime = true;
    GameObject tetrisPiece;
    GameObject currentObject;
    Vector3 boxSize = new Vector3(3, 1, 3);


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
        currentObject = Instantiate(tetrisPiece, cursor.position, Quaternion.Euler(tetrisPiece.transform.rotation.x, 
                        tetrisPiece.transform.rotation.y, tetrisPiece.transform.rotation.z));
    }
    
    // Update is called once per frame
    void Update() {
        //movement
        var horiz = Input.GetAxis(pm.horizontalAxisName);
        var vert = Input.GetAxis(pm.verticalAxisName);
        cursor.position += horiz * Vector3.right * cursorSpeed * Time.deltaTime;
        cursor.position += vert * Vector3.forward * cursorSpeed * Time.deltaTime;
        currentObject.transform.position += cursor.position;

        while (placingTime == true) {
            if (Input.GetButtonDown(pm.actionButton)) {
                //check target location for clear space
                var t = Utility.GetNearestPointOnGrid(cursor.position);
                targetPosition = new Vector3(t.x, 0, t.y);
                CheckGridPoint(targetPosition);     
                if (clear == true) {
                    Instantiate(tetrisPiece, targetPosition, Quaternion.Euler(tetrisPiece.transform.rotation.x,
                        tetrisPiece.transform.rotation.y, tetrisPiece.transform.rotation.z));
                    // destroy tetrispiece and clear tetris vectorlist
                    Destroy(currentObject);
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
