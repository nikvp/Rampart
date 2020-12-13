using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{

   


     Vector3 target;
    public float threshold = 0.5f;
    public float speed = 10f;
    public Turret turret;

    public void SetTarget(Vector2Int t) {
        target = new Vector3(t.x, 0, t.y);
    }
    void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(Vector3.Distance(target, transform.position)< threshold) {
            //TODO: try to hit something
            turret.Reset();
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
        turret.Reset();
    }

}


