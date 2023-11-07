using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChanger : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void newScene(string sceneName)
    {
        //built in Unity function to load a new scene
        SceneManager.LoadScene(sceneName);
        //physicsGameManager.myInstance.SceneChanger(sceneName);
    }
}
