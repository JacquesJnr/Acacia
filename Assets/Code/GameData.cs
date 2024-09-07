using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour, ISaveable
{
    [SerializeField] [Range(1,6)]private int columns;
    [SerializeField][Range(1,6)] private int rows;
    int maxCards;
    [SerializeField] private int matches;

    public SaveData loadData;

    public int GetRows()
    {
        return rows;
    }

    public int GetColumns()
    {
        return columns;
    }
    
    public GridLayoutGroup gameGrid;
    public GameObject cardPrefab;

    private void OnEnable()
    {
        // Subscribe to Events
        GameManager.OnStateChanged += Setup;
        CardMatch.OnMatch += OnCardMatched;
        
        maxCards = rows * columns;
        gameGrid.constraintCount = columns;
    }
    
    // The collection of card data stored in the save file;
    private List<Card> savedCardData;
    
    public void Setup(GameState state)
    {
        if(state == GameState.None){return;}
        
        if (state == GameState.Game)
        {
            
            var id = 0;
            // Instance Cards
            for (int i = 0; i < maxCards; i++)
            {
                var obj = Instantiate(cardPrefab, gameGrid.transform);
                var cardDetails = obj.GetComponent<Card>();
                // Check for saves
                if (!FileManager.LoadFromFile("SaveData.dat", out var json))
                {
                    // Increment ID
                    if (i % 2 == 0)
                    {
                        id++;
                    }
                        
                    // Assign IDs
                    cardDetails.ID = id;
                        
                    // Add Cards to Card Collection
                    GameManager.Instance.GetCardCollection.Add(cardDetails);
                }
                else
                {
                    LoadJsonData(this);
                    GameManager.Instance.GetCardCollection.AddFromLoad(cardDetails);
                }
                
                
            }
        }

        if (state == GameState.Reset)
        {
            matches = 0;
        }
    }
    
    private void OnCardMatched(int cardID)
    {
        ++matches;
        
        if (matches >= maxCards / 2)
        {
            GameManager.Instance.SetGameState(GameState.Reset);
        }
    }

    #region Save

    public static void SaveJsonData(GameData data)
    {
        SaveData sd = new SaveData();
        data.PopulateSaveData(sd);

        if (FileManager.WriteToFile("SaveData.dat", sd.ToJson()))
        {
            Debug.Log("Save Sucessful");
        }
    }

    public void PopulateSaveData(SaveData saveData)
    {
        saveData.matches = matches;
        saveData.rows = rows;
        saveData.columns = columns;
        
        foreach (Card card in GameManager.Instance.GetCardCollection.cards)
        {
            card.PopulateSaveData(saveData);
        }
    }
    
    #endregion

    #region Load

    public static void LoadJsonData(GameData data)
    {
        if (FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            SaveData sd = new SaveData();
            sd.LoadFromJson(json);

            data.LoadFromSaveData(sd);
            Debug.Log("Load complete");
        }
    }

    public void LoadFromSaveData(SaveData saveData)
    {
        matches = saveData.matches;
        rows = saveData.rows;
        columns = saveData.columns;
        
        loadData = saveData;
    }
    #endregion

    private void OnApplicationQuit()
    {
        SaveJsonData(this);
    }
}
