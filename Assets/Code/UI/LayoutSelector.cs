using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LayoutSelector : MonoBehaviour
{
    public void OpenLayoutSelector()
    {
        GetComponent<CanvasGroup>().alpha = 1;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    private void Start()
    {
        CloseLayoutSelector();
    }

    public void SetGameRows(int rowCount)
    {
        GameManager.Instance.GetSaveData.SetRows(rowCount);
    } 
    
    public void SetGameColumns(int columnCount)
    {
        GameManager.Instance.GetSaveData.SetColumns(columnCount);
    }

    public void SelectRandomLayout()
    {
        var range = Random.Range(2, 6);
        int randRow = range, randColumn = range;

        if (randRow * randColumn % 2 == 0)
        {
            GameManager.Instance.GetSaveData.SetRows(randRow);
            GameManager.Instance.GetSaveData.SetColumns(randColumn);
        }
        else
        {
            GameManager.Instance.GetSaveData.SetRows(3);
            GameManager.Instance.GetSaveData.SetColumns(4);
        }
        
        LoadNewLayout();
    }

    public void LoadNewLayout()
    {
        if (GameManager.Instance.GetSaveData.loadData == null)
        {
            GameManager.Instance.SetGameState(GameState.Reset);
        }
        else
        {
            GameManager.Instance.GetSaveData.DeleteSaveData();
            GameManager.Instance.SetGameState(GameState.Reset);
        }
        
        CloseLayoutSelector();
    }
    
    public void CloseLayoutSelector()
    {
        GetComponent<CanvasGroup>().alpha = 0;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void QuitGame()
    {
    // #if UNITY_EDITOR
    //     UnityEditor.EditorApplication.isPlaying = false;
    // #endif
        
        GameManager.Instance.GetSaveData.SaveGame();
        SceneManager.LoadScene(0);
    }
}
