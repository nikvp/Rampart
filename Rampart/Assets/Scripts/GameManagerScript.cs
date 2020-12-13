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
    public GameObject floodFiller;
    public Transform[] playerposition;
    public List<bool> destroyedCastle = new List<bool>();

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
            floodFiller.SetActive(true);
            var d = floodFiller.GetComponent<FloodFIllCastle>();
            destroyedCastle = d.holeInWall;
            if (destroyedCastle.Contains(true)) {
                StartPhase(GamePhase.GameOver);
            }

            else if (Input.GetKeyDown(KeyCode.Space)) {
                floodFiller.SetActive(false);                
                StartPhase(GamePhase.Battle);

            } else if (Input.GetKeyDown(KeyCode.R)) {
                floodFiller.SetActive(false);   
                StartPhase(GamePhase.GameOver);
            }

        } else if (phase == GamePhase.Battle) {
            var indicators = FindObjectsOfType<IndicatorScript>();
            foreach (var indicator in indicators) {
                if (indicator.occupied == false) {
                    Destroy(indicator.gameObject);
                }

            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                StartPhase(GamePhase.Rebuild);
            }
        } else if (phase == GamePhase.Rebuild) {
            floodFiller.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space)) {
                floodFiller.SetActive(false);
                StartPhase(GamePhase.Buy);

            } 

        } else if (phase == GamePhase.GameOver) {
            foreach (var destroyed in destroyedCastle) {
                if (destroyed == true) {
                    print("Player" + (destroyedCastle.IndexOf(destroyed) +1) + " your castlewalls have been breached, you lose!");
                }
                else {
                    print("Player" + (destroyedCastle.IndexOf(destroyed) + 1) + "your castle is still safe. Congratulations!" );
                }
            }
            print("Press Escape on the keyboard to get to the MainMenu");
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
            players.Add(pm);
        }
    }

}
