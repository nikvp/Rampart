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
    public float timerOne = 5f;
    public float timerTwo = 10f;
    public float timerThree = 15f;
    public float timerFour = 20f;
    public float timerPause = 1f;

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
            timerOne -= Time.deltaTime;
            
            if (Input.GetButtonDown("Space")) {
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
            timerTwo -= Time.deltaTime;
            //var c = floodFill.GetComponent<FloodFillCastle>();
            //c.checkOnorOff = true;
            
            //check if someone has lost
            //foreach(PlayerMain pms in players) {
            //    var l = pms.loosing;
            //    if (l == true) {
            //        StartPhase(GamePhase.GameOver);
            //    }
            //}
            if (Input.GetButtonDown("Space")) {
                StartPhase(GamePhase.Battle);
            }
            else if (Input.GetButtonDown("R")) {
                StartPhase(GamePhase.GameOver);
            }

        } else if (phase == GamePhase.Battle) {
            //run battlephasespritechanger
            timerPause -= Time.deltaTime;
            spriteChanger.SetActive(true);
            if (Input.GetButtonDown("Space")) {
                //start the battle
                timerThree -= Time.deltaTime;
            }
            if (Input.GetButtonDown("Space")) {
                //run battlephasespritechanger
                spriteChanger.SetActive(true);
                StartPhase(GamePhase.Rebuild);
                
            }

        } else if (phase == GamePhase.Rebuild) {
            timerFour -= Time.deltaTime;
            //var c = floodFill.GetComponent<FloodFillCastle>();
            //c.checkOnorOff = true;
            //if (timerFour > 0.5f) {
            //    foreach (PlayerMain pms in players) {
            //        var l = pms.GetComponent<PlayerBuild>().placingTime;
            //        l = false;
            //    }
            //}
            if (Input.GetButtonDown("Space")) {
                //c.checkOnorOff = true;
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
