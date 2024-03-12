using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class physicsGameManager : MonoBehaviour
{
    public GameObject[] enemyTeam;
    public NPC[] enemyScripts;
    // Start is called before the first frame update
    void Start()
    {
        enemyScripts = new NPC[enemyTeam.Length];
        for(int i = 0; i < enemyTeam.Length; i++) 
        {
            enemyScripts[i] = enemyTeam[i].GetComponent<NPC>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
