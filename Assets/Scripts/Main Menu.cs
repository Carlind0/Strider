using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsScreen;
    //public GameObject pause;
    public AudioMixer audioMixer;
    public AudioSource audioPlay;
    public AudioSource audioHover;
    public AudioSource audioQuit;
   
    
    


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            optionsScreen.SetActive(false);
            mainMenu.SetActive(true);
            audioQuit.Play();

        }

      

    }
   /*public void GoToScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
    }
   */

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Player has quit the game");
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
        mainMenu.SetActive(false);
    }
    public void CloseOpitions()
    {
        optionsScreen.SetActive(false);
        mainMenu.SetActive(true);

    }
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);

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
