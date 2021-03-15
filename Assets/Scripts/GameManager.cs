using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static float ChangeSceneTimer = 1.5f;
    public  static bool MainMenu;
    public static bool MainScene;
    
    // Start is called before the first frame update
    void Start()
    {
        MainScene = true;
        
    }
    public void changeToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    // Update is called once per frame
    void Update()
    {
        if (wizard.health <= 0 && !MainScene)
        {
            ChangeSceneTimer -= Time.deltaTime;
            
            if (ChangeSceneTimer <= 0)
            {
                ChangeSceneTimer = 3.5f;
                MainMenu = true;
                MainScene = false;
                changeToScene(0);
            }
        } 
        if(MainMenu)
        {
            ChangeSceneTimer -= Time.deltaTime;
            if (ChangeSceneTimer <= 0)
            {
                MainMenu = false;
                ChangeSceneTimer = 1.5f;
                changeToScene(2);
            }
        }
        
        
        
    }
}
