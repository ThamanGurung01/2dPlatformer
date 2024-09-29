using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject GameOver;
    public GameObject PauseMenu;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null&&!(PauseMenu.activeSelf))
            {
            GameOver.SetActive(true);
            }
        else
            {
            GameOver.SetActive(false);
            }
        if (Input.GetKeyDown(KeyCode.Escape)&&!(GameOver.activeSelf))
            {
            TogglePause();
            }
        }
    public void ReloadScene()
        {
        ResumeGame();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        }
    public void newGame()
        {
        SceneManager.LoadScene("Level 1");
        }

    public void TogglePause()
        {
        if (isPaused)
            {
            ResumeGame();
            }
        else
            {
            PauseGame();
            }
        }

    public void PauseGame()
        {
        Time.timeScale = 0;
        isPaused = true;
        PauseMenu.SetActive(true);
        }

    public void ResumeGame()
        {
        Time.timeScale = 1;
        isPaused = false;
        PauseMenu.SetActive(false);
        }
    public void ExitGame()
        {
        Application.Quit();
        }
    }
