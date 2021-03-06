﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GamePhase { PickCastle, Buy, Battle, Rebuild, GameOver };

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] GamePhase phase = GamePhase.PickCastle;
    public GameObject playerPrefab;
    List<PlayerMain> players = new List<PlayerMain>();
    public List<Transform> playerPositions;
    public GameObject spriteChanger;
    public float timerOne = 5f;
    public float timerTwo = 10f;
    public float timerThree = 15f;
    public float timerFour = 20f;
    public float timerPause = 1f;
    FloodFillCastle filler;
    List<bool> loosing = new List<bool>();
    private void Awake()
    {
        filler = FindObjectOfType<FloodFillCastle>();
    }
    void Start()
    {
        CreatePlayers();
        StartPhase(GamePhase.PickCastle);
    }

    void StartPhase(GamePhase p) {
        foreach (var player in players) {
            player.StartPhase(p);
        }
        phase = p;  
        print(phase);
    }
    // Update is called once per frame
    void Update() {
        if (phase == GamePhase.PickCastle) {
            timerOne -= Time.deltaTime;

            if (timerOne < 0) {
                // check if everyone built their castle, otherwise gameover
                //foreach (PlayerMain pms in players) {
                //    var l = pms.GetComponent<PlayerPickCastle>().castleBuilt;
                //    if (l == false) {
                //        StartPhase(GamePhase.GameOver);
                //    }
                //}
                StartPhase(GamePhase.Buy);
            }

        } else if (phase == GamePhase.Buy) {
            timerTwo = 10f;
            timerTwo -= Time.deltaTime;    
            //check if someone has lost
            //foreach(PlayerMain pms in players) {
            //    var l = pms.loosing;
            //    if (l == true) {
            //        StartPhase(GamePhase.GameOver);
            //    }
            //}
            foreach (PlayerMain pm in players) {
                var l = pm.loosing;
                if (l == true) {
                    loosing.Add(l);
                }
            }
            if (timerTwo < 0) {
                StartPhase(GamePhase.Battle);
            }
            else if (loosing.Count > 0) {
                StartPhase(GamePhase.GameOver);
            }

        } else if (phase == GamePhase.Battle) {
            timerThree = 15f;
            timerThree -= Time.deltaTime;
            if (Input.GetButtonDown("Space")) {
                //run battlephasespritechanger
                spriteChanger.SetActive(true);

            } else if (timerThree < 0) {
                StartPhase(GamePhase.Rebuild);
            }

        } else if (phase == GamePhase.Rebuild) {
            timerFour = 20f;
            timerFour -= Time.deltaTime;
            if (timerFour < 0) {

                StartPhase(GamePhase.Buy);
            }

        } else if (phase == GamePhase.GameOver) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }


    void CreatePlayers() {
        var mpc = FindObjectOfType<MultiplePlayerController>();
            foreach (int i in mpc.playerIndex) {
            var p = Instantiate(playerPrefab);
            var pm = p.GetComponent<PlayerMain>();
            pm.id = i;
            pm.startingPosition = playerPositions[i];
            players.Add(pm);
        }
    }

}
