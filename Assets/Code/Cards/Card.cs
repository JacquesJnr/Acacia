using System;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Card : MonoBehaviour, ISaveable
{
   public int ID;
   public bool Matched;

   [Header("Card Front")]
   public Image frontImage;

   [Header("Card Back")]
   public Image backImage;

   public static event Action<int> OnCardShown;

   [Header("Componenets")]
   public UI_CardBehavior ButtonBehavior;
   public AssignRandomIndex indexer;
   
   private void Start()
   {
      ButtonBehavior = GetComponentInChildren<UI_CardBehavior>();
      indexer = GetComponent<AssignRandomIndex>();
      
      ButtonBehavior.ID_tag.text = ID.ToString();
   }

   private void OnEnable()
   {
      
   }

   public void OnCardClicked()
   {
      if(ButtonBehavior.isFaceDown) {return;}
      OnCardShown?.Invoke(ID);
   }

   public void PopulateSaveData(SaveData saveData)
   {
      SaveData.CardData myData = new SaveData.CardData();

      myData.id = ID;
      myData.isMatched = Matched;
      myData.siblingIndex = transform.GetSiblingIndex();
      saveData.cardData.Add(myData);
      saveData.SortCardData();
   }

   public void LoadFromSaveData(SaveData saveData)
   {
      indexer.enabled = false;
      var cd = saveData.cardData;
      int i = transform.GetSiblingIndex();

      ID = cd[i].id;
      Matched = cd[i].isMatched;

      if (Matched)
      {
         ButtonBehavior.transform.localScale = Vector3.zero;
         ButtonBehavior.button.interactable = false;
         Destroy(ButtonBehavior.button);
      }
   }
}
