using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var horiz = Input.GetAxis("TestHorizontal");
        transform.position += horiz * Vector3.right * Time.deltaTime;
    }
}
