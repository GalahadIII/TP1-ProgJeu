using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{


    [SerializeField] private Image volumeBar;
    [SerializeField] private Image sfxBar;
    
    private bool menuInput;
    private bool isUp;
    
    private Canvas canvas;

    public int ppsize;
    
    private int maxVolume;
    private int musicVolumeValue;
    private uint musicVolumeSfx;

    // Start is called before the first frame update
    private void Start()
    {
        canvas = GetComponent<Canvas>();
        isUp = false;

        musicVolumeValue = 10;
        volumeBar.fillAmount = 1;
        sfxBar.fillAmount = 1;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            SpawnMenu();
        }
    }
    
    // Spawn or remove menu
    public void SpawnMenu()
    {
        canvas.enabled = !isUp;
        isUp = !isUp;
        Time.timeScale = isUp ? 0 : 1;
    }

    public void MusicVolume(bool up)
    {
        musicVolumeValue += (up ? 1 : -1);
        
        volumeBar.fillAmount = musicVolumeValue * 0.1f;
    }
}
