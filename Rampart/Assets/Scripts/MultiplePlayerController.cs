using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplePlayerController : MonoBehaviour {
    //scriptin tarkoitus on pelaajien ohjainten jakaminen ja käytettävien resurssien merkkaamimen
    public int maxPlayerCount = 3;
    public int currentPlayerCount = 0;
    public int[] playerIndex = new int[3];
    public string[] controlButtons;
    public bool[] playerInputAlreadyInUse;
    //public int n;
    //public int m;
    //public int[] playerNumber = new int[3];
    public float t = 30;
    private void Awake() {
        DontDestroyOnLoad(gameObject);
        int maxInputCount = 5; // 2 KB + 3 pad
        playerInputAlreadyInUse = new bool[maxInputCount];
        controlButtons = new string[maxInputCount];

        for (int i = 0; i < maxInputCount; i++) {
            controlButtons[i] = "Start" + i;
        }
    }
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        for (int i = 0; i < controlButtons.Length; i++) {
            if (Input.GetButtonDown(controlButtons[i])) {
                //PlayerSpawn(i);
                PlayerJoin(i);
                //print(i);
            }
        }
        if (currentPlayerCount >= 2) {
            Countdown();
        }
    }

    public void Countdown() {
        t -= Time.deltaTime;
        if (t <= 0) {
            //start game
            print("Go time");
            SceneManager.LoadScene("MainGameRampart");
            enabled = false;
        }
    }

    public void PlayerJoin(int inputIndex) {
        print("PlayerJoin " + inputIndex);
        if (playerInputAlreadyInUse[inputIndex]) return; //player allready joined
        if (currentPlayerCount == maxPlayerCount) return; //max players
        playerInputAlreadyInUse[inputIndex] = true;
        playerIndex[currentPlayerCount] = inputIndex;
        currentPlayerCount++;
        //playerNumber[n] = m;
        //n++;
        //m++;
    }
}
