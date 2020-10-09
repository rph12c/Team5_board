using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void sceneChange(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void exit()
    {
        Application.Quit();
    }
}
