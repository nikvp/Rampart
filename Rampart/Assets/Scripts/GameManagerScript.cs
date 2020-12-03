using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase { PickCastle, Buy, Battle, Rebuild, GameOver };

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] GamePhase phase = GamePhase.PickCastle;
    public GameObject playerPrefab;
    List<PlayerMain> players = new List<PlayerMain>();
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
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePlayers() {
        var mpc = FindObjectOfType<MultiplePlayerController>();

    }

}
