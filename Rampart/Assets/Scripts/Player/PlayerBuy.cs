﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuy : MonoBehaviour
{
    PlayerMain pm;
    public int maxTurretCount;
    public int currentTurretCount;
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
        
            }
        }
    



