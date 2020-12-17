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

    void Replace() {
        if (normal == true) {
            //replace with slanted
            foreach (GameObject castlePiece in castlePieces) {
                Instantiate(castleSlanted, castlePiece.transform.position, Quaternion.Euler(-90,90,90));
            }
            foreach (GameObject wallPiece in wallPieces) {
                Instantiate(wallSlanted, wallPiece.transform.position, Quaternion.Euler(-90,90,-180));
            }
            normal = false;
        } else if (normal == false){
            //replace with normal
            foreach (GameObject castlePiece in castlePieces) {
                Instantiate(castleNormal, castlePiece.transform.position, Quaternion.Euler(-90,180,0));
            }
            foreach (GameObject wallPiece in wallPieces) {
                Instantiate(wallNormal, wallPiece.transform.position, Quaternion.Euler(-90,0,90));
            }
            normal = true;
        }
        foreach(var i in castlePieces) {
            Destroy(i);
        }
        foreach(var i in wallPieces) {
            Destroy(i);
        }
        walls.Clear();
        castle.Clear();
        wallPieces.Clear();
        castlePieces.Clear();
    }

    void CheckPlayingField() {
        for (int x = 0; x < 40; x++) {
            for (int y = 0; y < 25; y++) {
                var w = Physics.OverlapBox(new Vector3(x, 0, y), new Vector3(1,1,1),
                                Quaternion.identity, wallsLayer);

                foreach(var i in w) {
                    walls.Add(i.gameObject.transform.position);
                    wallPieces.Add(i.gameObject);

                }
                //if (w.Length > 0) {
                //    var vector = w[0].gameObject.transform.position;
                //    walls.Add(vector);
                //    var wallPiece = w[0].gameObject;
                //    wallPieces.Add(wallPiece);
                //    Destroy(w[0].gameObject);

                //}
            }
        }
    }
    void GetTheCastles() {
        castle = gm.GetComponent<GameManagerScript>().playerPositions;
        foreach (Transform castleT in castle) {
            var c = Physics.OverlapBox(castleT.position, new Vector3(1,1,1),
                                    Quaternion.identity, castleLayer);
            foreach(var i in c) {
                castlePieces.Add(i.gameObject);

            }
            //var castlePiece = c[0].gameObject;
            //castlePieces.Add(castlePiece);
            //Destroy(c[0].gameObject);
        }
    }
}
