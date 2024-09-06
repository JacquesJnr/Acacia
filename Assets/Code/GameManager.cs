using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

    private GameCards _gameCards;
    private GameData _gameData;

    public GameCards GetCardCollection => _gameCards;
    public GameData GetSaveData => _gameData;

    public void SetGameState(GameState newState)
    {
        state = newState;
        OnStateChanged?.Invoke(newState);

        if (newState == GameState.Reset)
        {
            StartCoroutine(SetStateOnDelay(1,GameState.None));
        }
    }

    IEnumerator SetStateOnDelay(int delay, GameState state)
    {
        yield return new WaitForSeconds(delay);
        SetGameState(GameState.None);
    }
}