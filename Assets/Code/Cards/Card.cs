using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Card : MonoBehaviour
{
   public int ID;

   [Header("Card Front")]
   public Image frontImage;

   [Header("Card Back")]
   public Image backImage;

   public UI_CardBehavior ButtonBehavior;

   private void Start()
   {
      ButtonBehavior = GetComponentInChildren<UI_CardBehavior>();
      ButtonBehavior.name.text = ID.ToString();
   }
}
