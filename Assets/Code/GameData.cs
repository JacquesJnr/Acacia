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

    public void SetRows(int r) => rows = r;
    public int SetColumns(int c) => columns = c;

    public GridLayoutGroup gameGrid;
    public GameObject cardPrefab;
    
    #region Save

    public void SaveGame()
    {
        SaveJsonData(this);
    }
    
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
        saveData.score = Score.GetScore;
        
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
    
    public SaveData loadData;

    public void LoadFromSaveData(SaveData saveData)
    {
        matches = saveData.matches;
        rows = saveData.rows;
        columns = saveData.columns;
        Score.SetScore(saveData.score);
        
        loadData = saveData;
    }
    #endregion

    #region Delete

    public void DeleteSaveData()
    {
        if(FileManager.LoadFromFile("SaveData.dat", out var json))
        {
            FileManager.DeleteSaveFile("SaveData.dat");
        }
    }

    #endregion

    private void OnEnable()
    {
        // Subscribe to Events
        GameManager.OnStateChanged += OnGameStateChanged;
        CardMatch.OnMatch += OnCardMatched;
    }
    
    // The collection of card data stored in the save file;
    private List<Card> savedCardData;
    
    
    public void OnGameStateChanged(GameState state)
    {
        // Instance the game cards
        if (state == GameState.Game)
        {
            // Destroy Any Existing Cards
            GameManager.Instance.GetCardCollection.Clear();
            foreach (Transform t in gameGrid.transform)
            {
                Destroy(t.gameObject);
            }
            
            
            var id = 0;
            
            // Get Layout Data
            maxCards = rows * columns;
            gameGrid.constraintCount = columns;
            
            for (int i = 0; i < maxCards; i++)
            {
                // Instance Cards
                var obj = Instantiate(cardPrefab, gameGrid.transform);
                var cardDetails = obj.GetComponent<Card>();
                
                // Check for no saves
                if (!FileManager.LoadFromFile("SaveData.dat", out var json))
                {
                    // Increment ID
                    if (i % 2 == 0)
                    {
                        id++;
                    }
                        
                    // Assign Card IDs
                    cardDetails.ID = id;
                        
                    // Add Cards to Card Collection
                    GameManager.Instance.GetCardCollection.Add(cardDetails);
                }
                else
                {
                    // Load card data and add the cards to game card collection
                    LoadJsonData(this);
                    maxCards = rows * columns;
                    gameGrid.constraintCount = columns;
                    
                    GameManager.Instance.GetCardCollection.AddFromLoad(cardDetails, rows * columns);
                }
            }
        }

        if (state == GameState.Reset)
        {
            loadData = null;
        }
    }
    
    public int matches;
    
    private void OnCardMatched(int cardID)
    {
        ++matches;
        //score = GameManager.Instance.GetScore.CalulateNewScore();
        
        if (matches >= maxCards / 2)
        {
            GameManager.Instance.SetGameState(GameState.Reset);
            DeleteSaveData();
            matches = 0;
        }
    }

    private void OnApplicationQuit()
    {
        SaveJsonData(this);
    }

    private void OnDisable()
    {
        GameManager.OnStateChanged -= OnGameStateChanged;
        CardMatch.OnMatch -= OnCardMatched;
    }
}
