using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatllePhaseSpriteChanger : MonoBehaviour
{
    public GameObject wallNormal;
    public GameObject wallSlanted;
    public GameObject castleNormal;
    public GameObject castleSlanted;
    public bool normal = true;
    List<Vector3> walls = new List<Vector3>();
    List<Transform> castle = new List<Transform>();
    List<GameObject> wallPieces = new List<GameObject>();
    List<GameObject> castlePieces = new List<GameObject>();
    public LayerMask wallsLayer;
    public LayerMask castleLayer;

    public GameObject gm;


    private void OnEnable() {
        CheckPlayingField();
        GetTheCastles();
        Replace();
        gameObject.SetActive(false);
    }
    private void OnDisable() {
        walls.Clear();
        castle.Clear();
        wallPieces.Clear();
        castlePieces.Clear();
    }

    void Replace() {
        if (normal == true) {
            //replace with slanted
            foreach (GameObject castlePiece in castlePieces) {
                Instantiate(castleSlanted, castlePiece.transform.position, Quaternion.identity);
                Destroy(castlePiece);
            }
            foreach (GameObject wallPiece in wallPieces) {
                Instantiate(wallSlanted, wallPiece.transform.position, Quaternion.identity);
                Destroy(wallPiece);
            }
        } else {
            //replace with normal
            foreach (GameObject castlePiece in castlePieces) {
                Instantiate(castleNormal, castlePiece.transform.position, Quaternion.identity);
                Destroy(castlePiece);
            }
            foreach (GameObject wallPiece in wallPieces) {
                Instantiate(wallNormal, wallPiece.transform.position, Quaternion.identity);
                Destroy(wallPiece);
            }
        }
    }

    void CheckPlayingField() {
        for (int x = 0; x < 40; x++) {
            for (int y = 0; y < 25; y++) {
                var w = Physics.OverlapBox(new Vector3(x, 0, y), (Vector3.one * 0.5f),
                                Quaternion.identity, wallsLayer);
                if (w.Length > 0) {
                    var vector = w[0].gameObject.transform.position;
                    walls.Add(vector);
                    var wallPiece = w[0].gameObject;
                    wallPieces.Add(wallPiece);

                }
            }
        }
    }
    void GetTheCastles() {
        castle = gm.GetComponent<GameManagerScript>().playerPositions;
        foreach (Transform castleT in castle) {
            var c = Physics.OverlapBox(castleT.position, (Vector3.one * 0.5f),
                                    Quaternion.identity, castleLayer);
            var castlePiece = c[0].gameObject;
            castlePieces.Add(castlePiece);
        }
    }
}
