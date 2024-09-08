using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is borrowed from: https://github.com/UnityTechnologies/UniteNow20-Persistent-Data/blob/main/SaveData.cs
///
/// I've extended this class to store basic game data e.g. gamelayout, card ID's and card matches.
/// </summary>

[System.Serializable]
public class SaveData
{
    [System.Serializable]
    public struct CardData
    {
        public int id;
        public bool isMatched;
        public int siblingIndex;
    }
    
    public int matches;
    public int rows;
    public int columns;
    public List<CardData> cardData = new List<CardData>();

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string JSON)
    {
        JsonUtility.FromJsonOverwrite(JSON, this);
    }

    public void SortCardData()
    { 
        cardData.Sort((x, y) => x.siblingIndex.CompareTo(y.siblingIndex));
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveData saveData);
    void LoadFromSaveData(SaveData saveData);
}
