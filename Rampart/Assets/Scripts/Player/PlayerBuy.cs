using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuy : MonoBehaviour {
    PlayerMain pm;
    public int maxTurretCount;
    int currentTurretCount = 0;
    [SerializeField] Transform cursor;
    public float cursorSpeed = 3f;
    public GameObject turret;
    bool clear = false;
    public LayerMask indicators;
    public bool maxTurretsSet = false;




    void Awake() {
        pm = GetComponent<PlayerMain>();

    }
    void OnEnable() {
        print("Enabled" + pm.id);
        //enable cursor object
    }

    private void Start() {
        cursor.position += new Vector3(0,0,0);
    }

    void OnDisable() {
        print("Disabled" + pm.id);
        //disable cursor
    }

    // Update is called once per frame
    void Update() {
        var horiz = Input.GetAxis(pm.horizontalAxisName);
        var vert = Input.GetAxis(pm.verticalAxisName);
        cursor.position += horiz * Vector3.right * cursorSpeed * Time.deltaTime;
        cursor.position += vert * Vector3.forward * cursorSpeed * Time.deltaTime;
        
        if (Input.GetButtonDown(pm.actionButton)) {
            var t = Utility.GetNearestPointOnGrid(cursor.position);
            Vector3 target = new Vector3(t.x, 0, t.y);
            if (currentTurretCount <= maxTurretCount) {
                CheckForClear(target);
                if (clear == true) {
                    Instantiate(turret, target, Quaternion.identity);
                    currentTurretCount++;
                } else {
                    print("Position not clear");
                }
            } else {
                print("Max turret count achieved");
            }
        }
    }

    void CheckForClear(Vector3 target) {
        //if there are four indicators found you can place the turret
        //otherwise there should be a wall or castle or outside the castle
        var boxSize = new Vector3(2, 1, 2);
        var indicatorCount = Physics.OverlapBox(target, boxSize, Quaternion.identity, indicators);
        if (indicatorCount.Length > 3) {
            clear = true;
        }else {
            clear = false;
        }

    }

    
}



