using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodFillCastle : MonoBehaviour
{
    // lets assume this goes from 0, 0 to size.x, size.y
    public Vector2Int size;
    public Transform[] playerAreaCenters;
    public LayerMask unusableTerrain;
    public LayerMask walls;
    public LayerMask castle;
    public LayerMask inside;
    public LayerMask turret;
    public LayerMask indicators;
    public GameObject indicator;
    Vector3 boxsize = Vector3.one * 0.5f;
    public bool checkOnorOff = false;

    int[][] playerAreas;
    List<PlayerMain> pms;

 

    /// Returns -1 for unusable area, 0..2 for different players.
    public int AreaOwnerAtPoint(Vector2Int point) {
        if (IsOutside(point))
            return -1; // off map, so unusable area
        return playerAreas[point.x][point.y];
    }

    bool IsOutside(Vector2Int point) {
        return point.x < 0 || point.x >= size.x || point.y < 0 || point.y >= size.y;
    }

    Vector3 WorldPos(int x, int y) {
        return new Vector3(x, 0, y);
    }

    void FloodFillPlayerArea(Vector2Int startingPoint, int playerID) {
        // This is where we store the found points that we'll fill
        HashSet<Vector2Int> filled = new HashSet<Vector2Int>();
        // This is where we store the places that still need to be
        // checked for expanding the floodfill
        var fringe = new List<Vector2Int>();
        fringe.Add(startingPoint);

        var dirOffsets = new Vector2Int[] {
            Vector2Int.left, Vector2Int.up, Vector2Int.right, Vector2Int.down };



        // process remaining points as long as there are any
        while (fringe.Count > 0) {
            // we always take out the last one first (more efficient)
            var point = fringe[fringe.Count - 1];
            fringe.RemoveAt(fringe.Count - 1);

            // check if this point should be disqualified for fill for any reason:
            // if this was already filled, move on to checking the next point
            if (filled.Contains(point)) continue;
            // if this is outside the grid or in unusable space, also disqualified
            if (IsOutside(point) || playerAreas[point.x][point.y] == -1) {
                foreach(var script in pms) {
                    var id = script.id;
                    if (id == playerID) {
                        script.loosing = true;
                    } else continue;
                }
                print(playerID + " your walls have been breached! Fix them!");
                break;
            }

            var checkForCastle = Physics.OverlapBox(new Vector3(point.x, 0, point.y),
                boxsize, Quaternion.identity, castle);
            if (checkForCastle.Length > 0) {
                for (int i = 0; i < 4; i++) {
                    var neighbor = point + dirOffsets[i];
                    fringe.Add(neighbor);
                }
                continue;
            }

            var checkForWalls = Physics.OverlapBox(new Vector3(point.x, 0, point.y), boxsize,
                                    Quaternion.identity, walls);
            if (checkForWalls.Length > 0) {
                continue;
            }

            // add point to filled, then queue up the next points to try to expand
            filled.Add(point);
            for (int i = 0; i < 4; i++) {
                var neighbor = point + dirOffsets[i];
                fringe.Add(neighbor);
            }
        }

        // now we can do whatever with the points we found
        foreach (var point in filled) {
            var checkForIndicator = Physics.OverlapBox(new Vector3(point.x, 0, point.y), boxsize,
                                    Quaternion.identity, indicators);
            if (checkForIndicator.Length > 0) {
                continue;
            } else {
                var indi = Instantiate(indicator, new Vector3(point.x, 0, point.y), Quaternion.identity);
                var indiScript = indi.GetComponent<IndicatorScript>();
                indiScript.inside = true;
            }
        }

        //foreach (var script in pms) {
        //    var id = script.id;
        //    if (id == playerID) {
        //        script.loosing = false;
        //    }continue;
        //}
    }

    void Awake() {
        playerAreas = new int[size.x][];
        for (int i = 0; i < size.x; i++) {
            playerAreas[i] = new int[size.y];
        }
        ScanForUnusable();
        var playerScripts = FindObjectsOfType<PlayerMain>();
        foreach (var script in playerScripts) {
            pms.Add(script);
        }
    }

    void ScanForUnusable() {
        var boxSize = Vector3.one * 0.5f;
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                var c = Physics.OverlapBox(new Vector3(i, 0, j),
                                            boxSize,
                                            Quaternion.identity,
                                            unusableTerrain);
                if (c.Length > 0)
                    playerAreas[i][j] = -1;
            }
        }
    }

    void OnDrawGizmosSelected() {
        // debugging visualization
        if (playerAreas == null) return;
        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                var value = playerAreas[i][j];
                var color = Color.gray;
                if (value == -1) color = Color.black;
                if (value == 0) color = Color.red;
                if (value == 1) color = Color.cyan;
                //if (value == 2) color = Color.red;
                Gizmos.color = color;
                Gizmos.DrawWireCube(WorldPos(i, j), Vector3.one);
            }
        }
    }


    private void Update() {
        if (checkOnorOff == true) {
            for (int i = 0; i < 2; i++) {
                FloodFillPlayerArea(
                    Utility.GetNearestPointOnGrid(playerAreaCenters[i].position),
                    i);
            }
            checkOnorOff = false;
        }
    }
}
