using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuy : MonoBehaviour
{
    int id;

    public GameObject indicator;
    public GameObject turret;
    public int maxTurretCount;
    public int currentAmountOfTurrets;


    void Start()
    {
        GameAnouncment();
        id = GetComponent<PlayerMain>().id;

    }

    // Update is called once per frame
    void Update()
    {
         
    }
    public void CheckIfPlacableTile() {

    }
    private void GameAnouncment() {
        print("Place your Turrets inside your walls!");
    }
    void TurretPlacement() {
        if (Input.GetKeyDown("Action" + id)) {
            CheckIfPlacableTile();
            if (currentAmountOfTurrets > maxTurretCount) {
                Instantiate(turret, transform.position, Quaternion.identity);
                Instantiate(turret, transform.position, Quaternion.identity);
                currentAmountOfTurrets++;
            } else {
                print("Max Turretcount reached!");

            }
        }
    }
}
