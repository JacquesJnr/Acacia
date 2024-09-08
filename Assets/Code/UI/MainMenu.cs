using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    public Button playButton;
    public TMP_Text playButton_tag;
    public Button quitButton;

    private void Start()
    {
        if (FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            playButton_tag.text = "CONTINUE GAME";
        }
        else
        {
            playButton_tag.text = "PLAY GAME";
        }

    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
