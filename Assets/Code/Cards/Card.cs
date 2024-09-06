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

   public static event Action<int> OnCardShown;

   public UI_CardBehavior ButtonBehavior;
   
   private void Start()
   {
      ButtonBehavior = GetComponentInChildren<UI_CardBehavior>();
      ButtonBehavior.ID_tag.text = ID.ToString();
   }
   
   [Header("Auto-flip")] 
   public bool autoUnflip = true;
   public float autoFlipAfterSeconds;
   private float timeElapsed = 0f;
   
   private void Update()
   {
      // Count time since flipped
      if (autoUnflip && ButtonBehavior.isFaceUp)
      {
         timeElapsed += Time.deltaTime;
      }
      else
      {
         timeElapsed = 0;
      }
      
      // Unflip the card if too much time has passed
      if (timeElapsed >= autoFlipAfterSeconds)
      {
         ButtonBehavior.Flip();
         timeElapsed = 0;
      }
   }

   public void OnCardClicked()
   {
      if(ButtonBehavior.isFaceDown) {return;}
      OnCardShown?.Invoke(ID);
   }
}
