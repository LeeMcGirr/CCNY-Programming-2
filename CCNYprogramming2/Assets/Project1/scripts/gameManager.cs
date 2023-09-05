using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class gameManager : MonoBehaviour
{
    public float timer;
    public GameObject enemyPrefab;
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

        if (timer % 5 <= 0.05f && enemySpawned)
        {
            StartCoroutine(enemySpawn(.2f));

        }
        if (enemyTimer >= 5f)
        {
            //enemyTimer = 0f;
            //Debug.Log("timer fired off");
            //Instantiate(enemyPrefab, new Vector3(1, 0, 0), Quaternion.identity);
        }
        myText.text = timer.ToString();

    }


    private IEnumerator enemySpawn(float waitTime)
    {
        enemySpawned = false;
        Debug.Log("timer fired off");
        Instantiate(enemyPrefab, new Vector3(1, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(waitTime);
        enemySpawned = true;
    }
}
