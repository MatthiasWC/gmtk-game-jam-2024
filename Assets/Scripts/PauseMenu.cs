using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject panelUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panelUI.SetActive(!panelUI.activeSelf);
            TimeControl();

            //CheckGameObjects();
            //ChangeControls();
        }
    }

    public void GoToMainMenu()
    {
        //Debug.Log("went to main menu");
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        //Debug.Log("restarted game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResumeGame()
    {
        //Debug.Log("resumed game");
        panelUI.SetActive(!panelUI.activeSelf);
        //ChangeControls();
        TimeControl();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Ended!");
    }

    void TimeControl()
    {
        //ChangeControls();
        if (panelUI.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
