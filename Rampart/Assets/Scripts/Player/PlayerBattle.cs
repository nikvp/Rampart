using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    PlayerMain pm;
    [SerializeField] Transform cursor;
    public float cursorSpeed = 3f;
    public GameObject canonball;
    List<Turret> turrets = new List<Turret>();

    void Awake()
    {
        pm = GetComponent<PlayerMain>();

    }
    void OnEnable() {
        print("Enabled" + pm.id);
        //enable cursor object
        var t = FindObjectsOfType<Turret>();    
        foreach(Turret ts in t) {
            if (ts.playerID == pm.id) {
                turrets.Add(ts);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var horiz = Input.GetAxis(pm.horizontalAxisName);
        var vert = Input.GetAxis(pm.verticalAxisName);

        cursor.position += horiz * Vector3.right * cursorSpeed * Time.deltaTime;
        cursor.position += vert * Vector3.forward * cursorSpeed * Time.deltaTime;

        if (Input.GetButtonDown(pm.actionButton)) {
            TryShoot();
            
        }
    }
    void TryShoot() {
        int i = 0;

        foreach(Turret turret in turrets) {
            if (turret.CanFire()) {
                var target = Utility.GetNearestPointOnGrid(cursor.position);
                turret.Fire(target);
                
                break;
            }
            i++;
        }
        if (i < turrets.Count) {
            var tur = turrets[i];
            turrets.RemoveAt(i);
            turrets.Add(tur);
        }
    }
}
