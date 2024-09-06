using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameData : MonoBehaviour
{
    [SerializeField] [Range(1,6)]private int columns;
    [SerializeField][Range(1,6)] private int rows;

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
    
    static int maxCards;
    public static int GetMaxCards()
    {
        return maxCards;
    }

    private void OnEnable()
    {
        GameManager.OnStateChanged += Setup;
    }

    private void Start()
    {
        maxCards = rows * columns;
        gameGrid.constraintCount = columns;
    }
    
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
                
                if (i % 2 == 0)
                {
                    id++;
                }
            
                // Assign IDs
                var cardDetails = obj.GetComponent<Card>();
                cardDetails.ID = id;
            
                GameManager.Instance.GetCardCollection.Add(cardDetails);
            }
        }
    }
}
