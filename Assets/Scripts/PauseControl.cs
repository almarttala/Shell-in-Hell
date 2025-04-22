using UnityEngine;
using TMPro;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused;

    public GameObject pauseMenu;

    public GameObject objectiveList;

    public ObjectiveList objectiveListScript;  


    void Start()
    {
        objectiveListScript = objectiveList.GetComponent<ObjectiveList>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            pauseMenu.SetActive(gameIsPaused);
            PauseGame();
        }
        if (gameIsPaused)
        {
            PauseMenuSetup();
        }
        else
        {

        }
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    void PauseMenuSetup()
    {

    }
}