using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StateDebug : MonoBehaviour
{
   public void StartGame()
   {
      Debug.Log("Starting Game");
      GameManager.Instance.SetGameState(GameState.Game);
   }
   
   public void ResetGame()
   {
      Debug.Log("Resetting Game");
      GameManager.Instance.SetGameState(GameState.Reset);
   }
}
