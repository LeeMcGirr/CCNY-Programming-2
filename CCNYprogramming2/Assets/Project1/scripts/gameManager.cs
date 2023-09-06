using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class gameManager : MonoBehaviour
{
    public float timer;
    public GameObject enemyPrefab;
    public GameObject myPlayer;
    public TextMeshProUGUI myText;
    float enemyTimer;
    bool enemySpawned;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        enemySpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        enemyTimer += Time.deltaTime;

        // modulus (%) can be used to check for a remainder, letting us run actions on fixed intervals 
        //in this case we're checking the game time and spawning enemies on a fixed interval
        if (timer % 5 <= 0.05f && enemySpawned)
        {
            //coroutines run on a separate loop from update and can be used for cooldowns or to stop double-inputs
            StartCoroutine(enemySpawn(.2f)); 
        }

        //an alternative approach is to just set multiple timers up but this can get hard to track once you have
        //a more complex game scene with lots of objects and behaviors to track
        if (enemyTimer >= 5f)
        {
            //enemyTimer = 0f;
            //Debug.Log("timer fired off");
            //Instantiate(enemyPrefab, new Vector3(1, 0, 0), Quaternion.identity);
        }

        //GUI debug in scene - this updates our textmeshpro object to track gameTime
        myText.text = timer.ToString();

    }


    private IEnumerator enemySpawn(float waitTime)
    {
        //flip our enemySpawned boolean to false to disable additional spawns until after this one completes
        enemySpawned = false;
        Debug.Log("timer fired off");

        //create the newEnemy GameObject, then use the SetPlayer() function we wrote in the enemyController.cs to assign its target
        //use the Random.Range(min,max) function to create a random spawn point
        Vector3 newPos = new Vector3(Random.Range(-5f,5f), Random.Range(-5f,5f), 0f);
        
        //instantiate a new enemy prefab, be sure to assign it to a gameObject so we can reference it and change it properties next
        GameObject newEnemy = Instantiate(enemyPrefab, newPos, Quaternion.identity);

        //rename the new enemy and set its target to our player
        newEnemy.name = "enemy";
        newEnemy.GetComponent<enemyController>().SetPlayer(myPlayer);

        // run our wait time
        yield return new WaitForSeconds(waitTime);

        //re-enable the enemy spawn function
        enemySpawned = true;
    }
}
