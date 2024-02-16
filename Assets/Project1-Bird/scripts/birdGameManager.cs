using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class birdGameManager : MonoBehaviour
{
    [Header("Global vars")]
    public GameObject myPlayer;
    public float timer;
    public float timeLimit;
    public int score;
    public birdController myController;

    [Header("NPC vars")]
    public GameObject collectible1;
    public float spawnInterval;
    public float spawnTimer;
    public Vector2 spawnXBounds;
    public Vector2 spawnYBounds;

    [Header("UI/UX Vars")]
    public TextMeshProUGUI TitleText;

    //to design a state machine, first we need to define a subclass of enum - GameState
    public enum GameState
    {
        GAMESTART, PLAYING, GAMEOVER //we can pass it as many states as we want here
    };
    //declare an actual instance of GameState for the gameManager to use
    public GameState myGameState;

    // Start is called before the first frame update
    void Start()
    {
        myController = myPlayer.GetComponent<birdController>();
        myPlayer.SetActive(false);
        myGameState = GameState.GAMESTART;  

    }

    // Update is called once per frame
    void Update()
    {

        //switch statements work kind of like a lightswitch with 2 or more positions
        switch (myGameState)
        {
            //each case can be seen as a single unique position of the lightswitch
            case GameState.GAMESTART: 
                if(Input.GetKey(KeyCode.Space))
                {
                    EnterPlaying();
                }
                break;

            //this code only executes when our myGameState enum is currently in the matching state
            //to the case in question
            case GameState.PLAYING:
                //timer is global, spawnTimer tracks collectibles
                #region PLAYING_code
                timer += Time.deltaTime;
                spawnTimer += Time.deltaTime;

                //check against timeLimit, end the game if we're at time
                if(timer > timeLimit)
                {
                    EnterFinale();
                }

                //this is the world position where our collectible spawns
                float x = Random.Range(spawnXBounds.x, spawnXBounds.y);
                float y = Random.Range(spawnYBounds.x, spawnYBounds.y);
                Vector3 targetPos = new Vector3(x, y, 0);

                //instantiate and reset timer when condition is met
                if (spawnTimer > spawnInterval)
                {
                    spawnTimer = 0;
                    Debug.Log("spawnTimer: " + spawnTimer);
                    GameObject newObj = Instantiate(collectible1, targetPos, Quaternion.identity);
                }

                if (myController.myHealth < 0)
                {
                    EnterFinale();
                }

                #endregion
                break;

            case GameState.GAMEOVER:
                //code for the ending goes here
                if (Input.GetKey(KeyCode.Space))
                {
                    EnterPlaying();
                }

                break;

        }
    }

    //state change for playing mode, turn on players, disable any start menu logic
    void EnterPlaying()
    {
        //first we use the Unity method "FindGameObjectsWithTag()" to find all enemies or objects with our tag
        GameObject[] enemyObj = GameObject.FindGameObjectsWithTag("enemy");

        //now we use a for loop to run the Destroy() method on each
        for(int i = 0; i < enemyObj.Length; i += 1)
        {
            Destroy(enemyObj[i]);
        }

        myPlayer.transform.position = Vector3.zero;
        timer = 0f;
        spawnTimer = 0f;
        myGameState = GameState.PLAYING;
        myPlayer.SetActive(true);
        myController.myHealth = 1000f;
        TitleText.enabled = false;
    }

    public void EnterFinale()
    {
        myGameState = GameState.GAMEOVER;
        myPlayer.SetActive(false);
        TitleText.enabled = true;
        TitleText.text = "GAME OVER. Press [SPACE] to restart";

    }
}
