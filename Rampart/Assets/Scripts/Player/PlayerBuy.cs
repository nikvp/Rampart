using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuy : MonoBehaviour
{
    PlayerMain pm;
    [SerializeField] Transform cursor;
    public float cursorSpeed = 3f;
    public int maxTurretcount;
    private int currentTurretcount = 0;
    public GameObject Cannon;



    void Awake() {
        pm = GetComponent<PlayerMain>();

    }
    void OnEnable() {
        print("Enabled" + pm.id);
        //enable cursor object
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
        cursor.position += vert * Vector3.up * cursorSpeed * Time.deltaTime;

        if (Input.GetButtonDown(pm.actionButton)) {
            var target = Utility.GetNearestPointOnGrid(cursor.position);
            var boxsize = new Vector3(2, 2, 2);
            var placement = Physics.OverlapBox(new Vector3(target.x, 0, target.y), boxsize,
                                                    Quaternion.identity);
            if(placement.Length > 0) {
                print("Can't place turret here");
            }
            else {
                if(currentTurretcount < maxTurretcount) {
                    Instantiate(Cannon, new Vector3(target.x, 0, target.y), Quaternion.identity);
                    currentTurretcount++;
                }
                else {
                    print("Max turret count allready achieved!");
                }
            }
        }



    }
}
    



