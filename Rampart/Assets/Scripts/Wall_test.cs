using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall_test : MonoBehaviour {
    public int health = 3;

    private void OnTriggerEnter(Collider other) {
        health -= 1;
        print("-1 health");
    }
    private void Update() {
        if (health < 1) {
            GameObject.Destroy(gameObject);
        }
    }
    
}
