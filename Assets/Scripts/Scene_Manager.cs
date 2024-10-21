using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Audio;


public class Scene_Manager : MonoBehaviour
{
    public static Scene_Manager instance;
    public Animator animator;
    public GameObject transitionScreen;
    public AudioSource ThemeSong;
  

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToScene()
    {
        
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        while (ThemeSong.volume > 0)
        {
            ThemeSong.volume -= 0.003f;
            //yield return null;
        }
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        animator.SetTrigger("Start");
    }
    
}

