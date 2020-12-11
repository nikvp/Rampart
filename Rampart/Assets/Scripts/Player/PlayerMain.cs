﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public int id;
    [SerializeField] MonoBehaviour[] phaseControllers;
    public Transform startingPosition;
    public GameObject gm;


    public string horizontalAxisName = "Horizontal";
    public string verticalAxisName = "Vertical";
    public string startButton = "Start";
    public string rotateButton = "Rotate";
    public string actionButton = "Action";


    public void StartPhase(GamePhase phase) {
        foreach (var script in phaseControllers)
            script.enabled = false;
        phaseControllers[(int)phase].enabled = true;
    }


    public void Start() {

        var gms = gm.GetComponent<GameManagerScript>();
        startingPosition = gms.playerposition[id];

        horizontalAxisName += id;
        verticalAxisName += id;
        startButton += id;
        rotateButton += id;
        actionButton += id;

    }

}
