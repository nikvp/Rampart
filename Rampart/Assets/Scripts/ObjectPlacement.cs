using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacement : MonoBehaviour
{
    private Grid grid;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hitInfo))
            {
                PlaceObjectNear(hitInfo.point);
            }
        }
    }
    private void PlaceObjectNear(Vector3 nearPoint)
    {
        var finalPosition = Utility.GetNearestPointOnGrid(nearPoint);
        //place Gameobject
        // place predetermined pieces from wallrandomizer
    }
}
