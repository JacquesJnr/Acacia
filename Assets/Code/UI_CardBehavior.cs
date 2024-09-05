using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CardBehavior : MonoBehaviour
{
   private RectTransform rect;
   public bool flipped;

   private void Start()
   {
      rect = GetComponent<RectTransform>();
   }

   [Header("Animation Control")]
   [SerializeField] private float animationSpeed;

   public void MouseClick()
   {
      // Lerp - Flip the card
      if (!flipped)
      {
         CustomLerp.Instance.UI_Rotate(rect, new Vector3(0,180,0), animationSpeed, false);
         flipped = true;
      }
      else
      {
         CustomLerp.Instance.UI_Rotate(rect, new Vector3(0,0,0), animationSpeed, false);
         flipped = false;
      }
   }

   public void MouseEnter()
   {
      // Lerp - Scale up when hovered.
      CustomLerp.Instance.UI_Scale(rect, new Vector3(1.1f, 1.1f, 1f), animationSpeed, false);
   }

   public void MouseExit()
   {
      // Lerp - Scale to normal size when unhovered.
      CustomLerp.Instance.UI_Scale(rect, Vector3.one, animationSpeed, false);
   }
}
