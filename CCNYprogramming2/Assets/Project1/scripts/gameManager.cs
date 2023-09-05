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
    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 5f)
        {
            timer = 0f;
            Debug.Log("timer fired off");
            Instantiate(enemyPrefab, new Vector3(1, 0, 0), Quaternion.identity);
        }
        myText.text = timer.ToString();

    }
}
