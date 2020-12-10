using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject cannonBallPrefab;
    bool canFire = true;
    GameObject cannonBall;
    public int playerID = -1;

    public bool CanFire() {
        return canFire;
    }

    public void Fire(Vector2Int target) {
        if (!canFire) 
            return;
        canFire = false;
        if(cannonBall == null) {
           var c = Instantiate(cannonBallPrefab);
            cannonBall = c;
        }
        cannonBall.transform.position = transform.position;
        var cB = cannonBall.GetComponent<CannonBall>();
        cannonBall.SetActive(true);
        cB.turret = this;
        cB.SetTarget(target);
    }
    public void Reset() {
        canFire = true;
    }
}
