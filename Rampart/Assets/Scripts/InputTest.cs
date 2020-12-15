using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    public float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var horiz = Input.GetAxis("TestHorizontal");
        var vert = Input.GetAxis("TestVertical");

        transform.position += horiz * speed * Vector3.right *Time.deltaTime;
        transform.position += vert * speed * Vector3.back * Time.deltaTime;
    }
}
