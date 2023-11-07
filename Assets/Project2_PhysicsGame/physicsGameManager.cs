using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class physicsGameManager : MonoBehaviour
{

    private static physicsGameManager _MgrInstance; //first we declare a private instance of this script
    public static physicsGameManager myInstance //now we declare a public property Instance
    {
        get //we can use a get function to find the private instance _MgrInstance 
        {
            if (_MgrInstance == null) //if there is none, we can create one on the spot!
            {
                GameObject go = new GameObject("GameManager"); //first we create a game object
                go.AddComponent<physicsGameManager>(); //now we attach the game manager script to it
            }

            return _MgrInstance;
        }
    }

    float timer = 60f;

    public GameObject myPlayer;
    player3D myPlayerCon;

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
        myGameState = GameState.GAMESTART;
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
                if (Input.GetKeyUp(KeyCode.Return))
                {
                    sceneChanger(gameScene); //sceneChanger takes a string as an argument and loads a new scene
                    StartCoroutine(setPlayer(1f)); //we delay the search for our player to avoid running find() before the player loads
                    EnterPlaying();
                }
                break;

                case GameState.PLAYING: //code for the game playing scene

                timer -= Time.deltaTime;
                break;

                case GameState.GAMEOVER: //code for our game over screen/scene

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
    void sceneChanger(string sceneName)
    {
        //built in Unity function to load a new scene
        SceneManager.LoadScene(sceneName);
    }

    void findPlayer()
    {
        //uses a string to find a specific object, be sure to correctly name your player
        myPlayer = GameObject.Find("playerMesh");
        myPlayerCon = myPlayer.GetComponent<player3D>();
    }

    //this is a coroutine - a snippet of code that runs on its own time frame / loop when called
    //in this case we're using it to make sure our game scene loads before searching for the player
    //otherwise we'd risk searching before the player is loaded into the active game scene & failing to find
    IEnumerator setPlayer(float myTime)
    {
        //wait for a given amount of time
        yield return new WaitForSeconds(myTime);
        //call our findPlayer function
        findPlayer();
    }
}
