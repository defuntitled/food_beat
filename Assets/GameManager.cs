using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum Difficult
    {
        Easy = 5,
        Medium = 4,
        Hard = 3
    }

    [SerializeField] private GameObject[] prefabs;
    [SerializeField] private float delay;
    [SerializeField] private TextMeshProUGUI scoreLabel;
    [SerializeField] private TextMeshProUGUI livesLabel;
    [SerializeField] private GameObject PauseLabel;
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject startMenu;
    [SerializeField] private int failedTargets = 3;

    private float score;
    private Difficult difficult;
    
    public bool gameOver = false;
    public bool gameRunning = false;

    private void SetLivesText() 
    {
        livesLabel.text = string.Format("Lives: {0}", failedTargets);
    }

    private void PauseOn() 
    {
        pauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Resume";
        gameRunning = false;
        Time.timeScale = 0;
        PauseLabel.SetActive(true);
    }

    private void PauseOff()
    {
        pauseButton.GetComponentInChildren<TextMeshProUGUI>().text = "Pause";
        gameRunning = true;
        Time.timeScale = 1;
        PauseLabel.SetActive(false);
    }

    public void SwitchPause() 
    {
        if (gameRunning) PauseOn();
        else PauseOff();
    }

    public void Extit() 
    {
        Application.Quit();
    }

    private void StartGame() 
    {
        pauseButton.gameObject.SetActive(true);
        gameRunning = true;
        Time.timeScale = 1;
        failedTargets = (int)difficult;
        score = 0;
        SetScoreText();
        SetLivesText();
        startMenu.SetActive(false);
        switch (difficult)
        {
            case Difficult.Easy:
                InvokeRepeating("SpawnTarget", delay, 1.5f);
                break;
            case Difficult.Medium:
                InvokeRepeating("SpawnTarget", delay, 1f);
                break;
            case Difficult.Hard:
                InvokeRepeating("SpawnTarget", delay, 0.5f);
                break;

        }
        
        gameOver = false;
    }

    public void SetEasyDifficult()
    {
        difficult = Difficult.Easy;
        StartGame();
    }

    public void SetMediumDifficult()
    {
        difficult = Difficult.Medium;
        StartGame();
    }

    public void ShowMainMenu()
    {
        gameOverMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void SetHardDifficult()
    {
        difficult = Difficult.Hard;
        StartGame();
    }

    public void RestartGame() 
    {
        gameOverMenu.SetActive(false);
        StartGame();
    }

    private void SetScoreText() 
    {
        scoreLabel.text = string.Format("Score: {0}", score);
    }

    public void ImplicateScore(int added) 
    {
        score+=added;
        SetScoreText();
    }

    private void SpawnTarget() 
    {
            int targetIndex = Random.Range(0, prefabs.Length);
            Instantiate(prefabs[targetIndex]);
    }

    public void Fali()
    {
        
        if(failedTargets>0)failedTargets--;
        if (failedTargets == 0 && !gameOver) 
        {
            GameOver();
        }
        SetLivesText();
    }

    private void GameOver() 
    {
        Time.timeScale = 0.5f;
        pauseButton.gameObject.SetActive(false);
        gameRunning = false;
        CancelInvoke("SpawnTarget");
        gameOver = true;
        gameOverMenu.SetActive(true);
        Debug.Log(failedTargets);
    }

   
}
