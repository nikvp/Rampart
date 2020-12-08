using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallReplacer : MonoBehaviour
{
    //scriptin tarkoitus on vaihtaa placetut wallipalat tuhottaviin wallipaloihin
    //scriptin tarkoitus on myös puhdistaa ylimääräiset ulokkeet

    // Start is called before the first frame update
    private Grid grid;
    public float size = 1f;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Replacement()
    {
        for (float x = 0; x < 40; x += size)
        {
            for (float z = 0; z < 40; z += size)
            {
                var point = GetNearestPointOnGrid(new Vector3(x, 0f, z));
                //katso onko siinä kohdassa collider
                //jos on niin delete object 
                //lisää siihen yksittäinen pala
                
            }
        }
    }
}
