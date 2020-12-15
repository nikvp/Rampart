﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuild : MonoBehaviour
{
    PlayerMain pm;
    [SerializeField] Transform cursor;
    public float cursorSpeed = 3f;

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
        cursor.position += vert * Vector3.back * cursorSpeed * Time.deltaTime;


    }
}
