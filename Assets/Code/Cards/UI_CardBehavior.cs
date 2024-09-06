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
   public TMP_Text name;
   public bool flipped;

   private void OnEnable()
   {
      rect = GetComponent<RectTransform>();
      StartCoroutine(FlipOnDelay());
   }

   [Header("Animation Control")]
   [SerializeField] private float initialFlipDelay;
   [SerializeField] private float animationSpeed;
   [SerializeField] private float flipSpeed;

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

   public void MouseClick()
   {
      Flip();
   }

   public void MouseEnter()
   {
      if(!button.interactable){return;}
      
      // Lerp - Scale up when hovered.
      CustomLerp.Instance.UI_Scale(rect, new Vector3(1.1f, 1.1f, 1f), animationSpeed, false);
   }

   public void MouseExit()
   {
      if(!button.interactable){return;}
      
      // Lerp - Scale to normal size when unhovered.
      CustomLerp.Instance.UI_Scale(rect, Vector3.one, animationSpeed, false);
   }

   public void SetInteractable(bool state)
   {
      button.interactable = state;
   }

   IEnumerator FlipOnDelay()
   {
      yield return new WaitForSeconds(initialFlipDelay);
      
      Flip();
      button.interactable = true;
   }
}
