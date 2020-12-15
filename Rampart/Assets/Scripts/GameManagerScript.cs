using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GamePhase { PickCastle, Buy, Battle, Rebuild, GameOver };

public class GameManagerScript : MonoBehaviour
{

    [SerializeField] GamePhase phase = GamePhase.PickCastle;
    public GameObject playerPrefab;
    List<PlayerMain> players = new List<PlayerMain>();
    public List<Transform> playerPositions = new List<Transform>();
    public GameObject floodFill;
    public GameObject spriteChanger;

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
        phase = p;  
        print(phase);
    }
    // Update is called once per frame
    void Update() {
        if (phase == GamePhase.PickCastle) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                StartPhase(GamePhase.Buy);
            }

        } else if (phase == GamePhase.Buy) {
            var c = floodFill.GetComponent<FloodFillCastle>();
            c.checkOnorOff = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                StartPhase(GamePhase.Battle);
            } else if (Input.GetKeyDown(KeyCode.R)) {
                StartPhase(GamePhase.GameOver);
            }

        } else if (phase == GamePhase.Battle) {
            //run battlephasespritechanger
            spriteChanger.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)) {
                //run battlephasespritechanger
                spriteChanger.SetActive(true);
                StartPhase(GamePhase.Rebuild);
            }

        } else if (phase == GamePhase.Rebuild) {
            var c = floodFill.GetComponent<FloodFillCastle>();
            c.checkOnorOff = true;
            if (Input.GetKeyDown(KeyCode.Space)) {
                c.checkOnorOff = true;
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
