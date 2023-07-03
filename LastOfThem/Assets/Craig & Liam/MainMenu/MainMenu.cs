using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
       
        GetComponent<Animator>().SetTrigger("Change");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application closing");
    }
}
