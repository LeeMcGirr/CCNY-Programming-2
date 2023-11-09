using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.UIElements;

public class physicsGameManager : MonoBehaviour
{

    // --------------------------------- code for instancing a singleton and loading before scene ----------------------------------------//

    private static physicsGameManager _MgrInstance; //declaring an instance of this script itself

    public static physicsGameManager myInstance //describes a property you can use to find or create an instance of this script if it does not already exist
    {
        get // this get function will run whenever we declare physicsGameManager.myInstance in any of our code
        {
            if(_MgrInstance == null ) // first we define what to do if there is no instance - we create one
            {
                GameObject myGO = new GameObject("GameManager"); //all scripts need a gameobject in unity so we make one then attach the script
                myGO.AddComponent<physicsGameManager>();
                DontDestroyOnLoad(myGO.gameObject);
            }
            return _MgrInstance; //finally, return the _MgrInstance, either the existing one, or if there was none, the new one created in our if statement
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)] //this spawns the game manager before the scene loads
    public static void InitializeFramework() //we need to declare a function to run in .BeforeSceneLoad
    {
        physicsGameManager newMgr = physicsGameManager.myInstance; //finally, we have a new instance of our gameManager
    }

// ---------------------------- variables in the game manager ------------------------------------------ //

    float timer = 60f;

    public GameObject myPlayer;
    public player3D myPlayerCon;

    public GameObject goalie;
    public GameObject striker;


    [Header("scenes")]
    public string introScene = "IntroScene";
    public string gameScene = "physicsGame";
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
        findStartButton();
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


    public void findStartButton()
    {
        GameObject myButton = GameObject.Find("Button");
        if (myGameState == GameState.GAMESTART) { myButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SceneChanger(gameScene)); }
        else if (myGameState == GameState.GAMEOVER) { myButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SceneChanger(introScene)); }
    }
}
