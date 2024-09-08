using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CardBehavior : MonoBehaviour
{
   private RectTransform rect;
   public Button button;
   //public TMP_Text ID_tag;
   public bool flipped;
   
   public bool isFaceUp => button.interactable && !flipped;
   public bool isFaceDown => button.interactable && flipped;

   private void OnEnable()
   {
      rect = GetComponent<RectTransform>();
      DelayedFlip(initialFlipDelay);
   }

   [Header("Animation Control")]
   [SerializeField] private float initialFlipDelay;
   [SerializeField] private float animationSpeed;
   [SerializeField] private float flipSpeed;

   #region Card Flip Logic

   public void Flip()
   {
      // Lerp - Flip the card
      if (!flipped)
      {
         CustomLerp.Instance.UI_Rotate(rect, new Vector3(0,180,0), flipSpeed, false);
         flipped = true;
      }
      else
      {
         CustomLerp.Instance.UI_Rotate(rect, new Vector3(0,0,0), flipSpeed, false);
         flipped = false;
      }
   }
   
   public void DelayedFlip(float delay)
   {
      if (!button.enabled)
      {
         return;
      }

      StartCoroutine(FlipOnDelay(delay));
   }
   
   IEnumerator FlipOnDelay(float delay)
   {
      yield return new WaitForSeconds(delay);
      
      Flip();
      if (button != null)
      {
         button.interactable = true;
      }
   }
   #endregion

   #region Mouse Events

   public void MouseClick()
   {
      Flip();
   }
   
   public void MouseEnter()
   {
      if(!button.interactable || button == null){return;}
      
      // Lerp - Scale up when hovered.
      CustomLerp.Instance.UI_Scale(rect, new Vector3(1.1f, 1.1f, 1f), animationSpeed, false);
   }

   public void MouseExit()
   {
      if(!button.interactable || button == null){return;}
      
      // Lerp - Scale to normal size when unhovered.
      CustomLerp.Instance.UI_Scale(rect, Vector3.one, animationSpeed, false);
   }

   #endregion
   
   public void OnMatched(float delay)
   {
      button.interactable = false;
      StartCoroutine(ShrinkOnDelay(delay));
   }

   IEnumerator ShrinkOnDelay(float delay)
   {
      yield return new WaitForSeconds(delay);
      CustomLerp.Instance.UI_Scale(rect, Vector3.zero, animationSpeed, false);
   }
}
