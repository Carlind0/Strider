using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    public GameObject gameOverUI;
    public AudioMixer audioMixer;
    public AudioSource audioPlay;
    public AudioSource audioHover;
    public AudioSource audioQuit;
    public GameObject BattleTheme;
    public GameObject DeathScreenTheme;
    public TextMeshProUGUI ScoreBoard;


    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverUI.activeInHierarchy)
        {
            Color color = new Color(1, 1, 1, 1);
            ScoreBoard.color = color;
            BattleTheme.SetActive(false);
            DeathScreenTheme.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        
    }
    public void gameOver()
    {

        gameOverUI.SetActive(true);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void mainMenu()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 2);
    }
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        
    }
    public void playButton()
    {
        audioPlay.Play();
    }

    public void hoverButton()
    { audioHover.Play(); }

    public void quitButton()
    {
        audioQuit.Play();
    }
}
