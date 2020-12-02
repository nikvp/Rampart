using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //scriptin tarkoitus on pelaajien ohjainten jakaminen ja käytettävien resurssien merkkaamimen

    private PlayerController pc;
    public GameObject[] playerprefabs;
    GameManagerScript gm;

    public int maxPlayerCount = 3;
    public int currentPlayerCount = 0;
    public int[] playerIndex = new int[3];
    public string[] controlButtons;
    public bool[] playerInputAlreadyInUse;
    public int n;
    public int m;
    public int[] playerNumber = new int[3];

    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        int maxInputCount = maxPlayerCount + 1;
        playerInputAlreadyInUse = new bool[maxPlayerCount];
        controlButtons = new string[maxInputCount];
        controlButtons[0] = "Start";

        for (int i;i <=maxPlayerCount; i++)
        {
            controlButtons[i] = "button " + i;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    
            for (int i = 0; i < controlButtons.Length; i++)
            {
                if (Input.GetButtonDown(controlButtons[i]))
                {
                    //PlayerSpawn(i);
                    PlayerJoin(i);
                    //print(i);
                }
            }
    
    }



    public void PlayerJoin(int inputIndex)
    {
        if (playerInputAlreadyInUse[inputIndex]) return; //player allready joined
        if (currentPlayerCount == maxPlayerCount) return; //max players
        playerInputAlreadyInUse[inputIndex] = true;
        playerIndex[currentPlayerCount] = inputIndex;
        currentPlayerCount++;
        playerNumber[n] = m;
        n++;
        m++;
    }
}
