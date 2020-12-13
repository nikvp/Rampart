using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    PlayerMain pm;
    [SerializeField] Transform cursor;
    public float cursorSpeed = 10f;
    public GameObject canonball;




    void Awake()
    {
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
    void Update()
    {
        var horiz = Input.GetAxis(pm.horizontalAxisName);

        //if (Input.GetButton(pm.horizontalAxisName)) {
        //    horiz = 1;
        //}
        cursor.position += horiz * Vector3.right * cursorSpeed * Time.deltaTime;

        

        if (Input.GetButtonDown(pm.actionButton)) {
            var Cannon = FindObjectOfType<Turret>();
            var target = Utility.GetNearestPointOnGrid(cursor.position);
            if (Cannon.playerID == pm.id && Cannon.CanFire()) {
                Cannon.Fire(target);
            }
            
            //var hits = Physics.OverlapSphere(new Vector3(target.x, 0, target.y), 0.2f);
            //foreach (var h in hits) {
            //    var d = h.GetComponent<IDamageable>();
            //    if (d != null) {
            //        d.TakeDamage();
            //    }
            //}
        }
    }
}
