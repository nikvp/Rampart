using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickCastle : MonoBehaviour
{
    int id;
    void Start()
    {
        id = GetComponent<PlayerMain>().id;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Start" + id)) {
            print("Player " + id + " start pressed");
        }
    }
}
