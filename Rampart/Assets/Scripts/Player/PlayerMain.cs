using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public int id;
    [SerializeField] MonoBehaviour[] phaseControllers;
    public void StartPhase(GamePhase phase) {
        foreach (var script in phaseControllers)
            script.enabled = false;
        phaseControllers[(int)phase].enabled = true;
    }
    private string horizontalAxisName = "Horizontal";
    private string verticalAxisName = "Vertical";
    private string startButton = "Start";
    private string rotateButton = "Rotate";
    private string actionButton = "Action";

    public void Start() {
        horizontalAxisName = "Horizontal" + id;
        verticalAxisName = "Vertical" + id;
        startButton = "Start" + id;
        rotateButton = "Rotate" + id;
        actionButton = "Action" + id;
    }
}
