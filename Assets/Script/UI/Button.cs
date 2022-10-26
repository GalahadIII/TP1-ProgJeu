using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Button : MonoBehaviour
{
    [SerializeField] private Menu _menuScript;
    
    public void ExitEvent()
    {
        Application.Quit();
    }

    public void ResumeEvent()
    {
        _menuScript.SpawnMenu();
    }

    public void MusicVolumeEvent(bool up)
    {
        _menuScript.MusicVolume(!up);
    }
    
    public void RestartInput()
    {
        SceneManager.LoadScene("Scene/Cemetery");
    }
    
}
