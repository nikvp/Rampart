using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Utility  {
    public static Vector2Int GetNearestPointOnGrid(Vector3 position) {
        //position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x);
        int yCount = Mathf.RoundToInt(position.z);
        //int zCount = Mathf.RoundToInt(position.z / size);

        //Vector3 result = new Vector3(
        //    (float)xCount * size,
        //    (float)yCount * size,
        //    (float)zCount * size);
        //result += transform.position;

        return new Vector2Int(xCount, yCount);

    }
}