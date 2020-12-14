using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    [SerializeField]
    private float size = 1f;
    public GameObject indicator;
    List<Vector3> indicatorList = new List<Vector3>();
    public bool indicatorActivity = false;

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        for (float x = 0; x < 40; x += size) {
            for (float z = 0; z < 25; z += size) {
                var point = Utility.GetNearestPointOnGrid(new Vector3(x, 0f, z));
                Gizmos.DrawSphere(new Vector3(point.x, 0, point.y), 0.1f);

            }
        }
    }


}
            

