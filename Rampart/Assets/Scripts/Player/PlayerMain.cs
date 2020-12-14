using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public int id;
    [SerializeField] MonoBehaviour[] phaseControllers;
    public Transform startingPosition;
    public float speed = 1f;
    private float size = 2f;
    public bool loosing = false;

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
        horizontalAxisName += id;
        verticalAxisName = "Vertical" + id;
        startButton = "Start" + id;
        rotateButton = "Rotate" + id;
        actionButton = "Action" + id;

    }

    public void FixedUpdate() {
        //Movement();
    }

    //public void Movement() {
    //    if (Input.GetAxisRaw(horizontalAxisName) != 0) {
    //        targetPosition = transform.position + new Vector3(transform.position.x, transform.position.y,
    //            Input.GetAxisRaw(horizontalAxisName) * speed * Time.deltaTime);
    //        GetNearestPointOnGrid(targetPosition);
    //        transform.position += targetPosition;
    //    }
    //    else if (Input.GetAxisRaw(verticalAxisName) != 0) {
    //        targetPosition = transform.position + new Vector3(Input.GetAxisRaw(horizontalAxisName) * speed * Time.deltaTime,
    //            transform.position.y, transform.position.z);
    //        GetNearestPointOnGrid(targetPosition);
    //        transform.position += targetPosition;

    //    }
    //}

 

}
