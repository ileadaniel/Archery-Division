using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject pauseMenu;

    public bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quit!");
            Application.Quit();
        }
    }

    //public void PlayGame()
   // {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     //   pauseMenu.SetActive(false);
     //   Time.timeScale = 1f;
     //   isPaused = false;
    //}

   // public void PauseGame()
   // {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     //   pauseMenu.SetActive(true);
    //    Time.timeScale = 0f;
    //    isPaused = true;
    //}

  //  public void QuitGame()
  //  {
   //     Debug.Log("Quit!");
  //      Application.Quit();
   // }
    
}
