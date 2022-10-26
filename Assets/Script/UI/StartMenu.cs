using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void OnStartInput()
    {
        SceneManager.LoadScene("Scene/Cemetery");
    }

    public void OnQuitInput()
    {
        Application.Quit();
    }
}
