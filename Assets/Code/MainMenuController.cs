using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void RetryGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryGame3()
    {
        SceneManager.LoadScene(3);
    }

    public void RetryGame2()
    {
        SceneManager.LoadScene(2);
    }

    public void RetryGame4()
    {
        SceneManager.LoadScene(4);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
