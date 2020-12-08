using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    public int id;
    [SerializeField] MonoBehaviour[] phaseControllers;
    Vector3 targetPosition;
    Vector3 startingPosition;
    public float speed = 1f;
    private float size = 1f;

    private string horizontalAxisName = "Horizontal";
    private string verticalAxisName = "Vertical";
    private string startButton = "Start";
    private string rotateButton = "Rotate";
    private string actionButton = "Action";





    public void StartPhase(GamePhase phase) {
        foreach (var script in phaseControllers)
            script.enabled = false;
        phaseControllers[(int)phase].enabled = true;
    }


    public void Start() {
        horizontalAxisName = "Horizontal" + id;
        verticalAxisName = "Vertical" + id;
        startButton = "Start" + id;
        rotateButton = "Rotate" + id;
        actionButton = "Action" + id;

    }

    public void FixedUpdate() {
        Movement();
    }

    public void Movement() {
        if (Input.GetAxisRaw(horizontalAxisName) != 0) {
            targetPosition = transform.position + new Vector3(transform.position.x, transform.position.y,
                Input.GetAxisRaw(horizontalAxisName) * speed * Time.deltaTime);
            GetNearestPointOnGrid(targetPosition);
            transform.position += targetPosition;
        }
        if (Input.GetAxisRaw(verticalAxisName) != 0) {
            targetPosition = transform.position + new Vector3(Input.GetAxisRaw(horizontalAxisName) * speed * Time.deltaTime,
                transform.position.y, transform.position.z);
            GetNearestPointOnGrid(targetPosition);
            transform.position += targetPosition;

        }
    }

    public Vector3 GetNearestPointOnGrid(Vector3 position) {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);
        result += transform.position;

        return result;

    }

}
