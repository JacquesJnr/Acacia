using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    None,
    Game,
    Reset,
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnStateChanged; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        _gameCards = GetComponent<GameCards>();
        _gameData = GetComponent<GameData>();
        _cardMatcher = GetComponent<CardMatch>();

        SetGameState(GameState.Game);
    }

    private GameCards _gameCards;
    private GameData _gameData;
    private CardMatch _cardMatcher;

    public GameCards GetCardCollection => _gameCards;
    public GameData GetSaveData => _gameData;
    public CardMatch GetCardMatcher => _cardMatcher;

    public void SetGameState(GameState newState)
    {
        state = newState;
        OnStateChanged?.Invoke(newState);

        if (newState == GameState.Reset)
        {
            ResetTable();
            RestartGame(1);
        }

        if (newState == GameState.None)
        {
            
        }
    }

    public void RestartGame(float delay)
    {
        StartCoroutine(SetStateOnDelay(GameState.Game, delay));
        ResetTable();
    }

    IEnumerator SetStateOnDelay(GameState state, float delay)
    {
        yield return new WaitForSeconds(delay);
        SetGameState(state);
    }

    public void ResetTable()
    {
        GetCardCollection.Clear();
        GetSaveData.matches = 0;
    }
}