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
    public int[] joinedPlayers = new int[3];
    private void Awake()
    {
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
        print(phase);
    }
    // Update is called once per frame
    void Update()
    {
        if (phase == GamePhase.PickCastle) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                StartPhase(GamePhase.Buy);
        }
        if (phase == GamePhase.Buy) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    StartPhase(GamePhase.Battle);
                }
            }
        if (phase == GamePhase.Battle) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    StartPhase(GamePhase.Rebuild);
                }
            }
        if (phase == GamePhase.Rebuild) {
                if (Input.GetKeyDown(KeyCode.Space)) {
                    StartPhase(GamePhase.Battle);
                } else if (Input.GetKeyDown(KeyCode.R)) {
                    StartPhase(GamePhase.GameOver);
                }
                
            }
        if (phase == GamePhase.GameOver) {
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
    }

    void CreatePlayers() {
        var mpc = FindObjectOfType<MultiplePlayerController>();
        foreach (int joinedPlayer in mpc.playerIndex) {
            Instantiate(playerPrefab);
            players.Add(playerPrefab.GetComponent<PlayerMain>());

        }
    }

}
