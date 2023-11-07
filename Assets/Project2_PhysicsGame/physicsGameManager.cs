using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class physicsGameManager : MonoBehaviour
{

    private static physicsGameManager _MgrInstance; //declaring an instance of the mgr script

    public static physicsGameManager myInstance
    {
        get
        {
            if(_MgrInstance == null )
            {
                GameObject myGO = new GameObject("GameManager");
                myGO.AddComponent<physicsGameManager>();
                DontDestroyOnLoad(myGO.gameObject);
            }
            return _MgrInstance;
        }
    }





    float timer = 60f;

    public GameObject myPlayer;
    public player3D myPlayerCon;

    public GameObject goalie;
    public GameObject striker;


    [Header("scenes")]
    public string introScene;
    public string gameScene;
    public string finaleScene;

    public enum GameState { GAMESTART,PLAYING,GAMEOVER };
    public GameState myGameState;


    void Awake()
    {
        //make sure our gameManager is persistent & doesn't die on scene change
        myGameState = GameState.PLAYING;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        switch (myGameState)
        {
                case GameState.GAMESTART: //code for the start menu goes here

                break;

                case GameState.PLAYING: //code for the game playing scene
                
                if(myPlayer == null)
                {
                    findPlayer();
                }

                timer -= Time.deltaTime;

                break;

                case GameState.GAMEOVER: //code for our game over screen/scene

                Destroy(this.gameObject);

                break;
        }

    }

    void ChangeMode(GameState state) //call this to change the GameState enum, takes a state as an argument
    {
        myGameState = state;
    }

    void EnterPlaying()
    {
        ChangeMode(GameState.PLAYING);
    }

    void EnterFinale()
    {
        ChangeMode(GameState.GAMEOVER);
    }

    void EnterStart()
    {
        ChangeMode(GameState.GAMESTART);
    }

    //method to call whenever the scene needs to be changed
    public void SceneChanger(string sceneName)
    {
        //built in Unity function to load a new scene
        SceneManager.LoadScene(sceneName);
    }

    public void findPlayer()
    {
        //uses a string to find a specific object, be sure to correctly name your player
        myPlayer = GameObject.Find("playerMesh");
        myPlayerCon = myPlayer.GetComponent<player3D>();
    }

    //this is a coroutine - a snippet of code that runs on its own time frame / loop when called
    //in this case we're using it to make sure our game scene loads before searching for the player
    //otherwise we'd risk searching before the player is loaded into the active game scene & failing to find
    public IEnumerator setPlayer(float myTime)
    {
        //wait for a given amount of time
        yield return new WaitForSeconds(myTime);
        //call our findPlayer function
        findPlayer();
    }
}
