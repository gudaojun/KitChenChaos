using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
   /// <summary>
   /// 显示配方交付數量文本
   /// </summary>
   [SerializeField] private TextMeshProUGUI recipesDeliveredText;
   
   private void Start()
   {
      KitChenGameManager.Instance.OnStateChanged += KitChenGameManager_OnStateChanged;
      Hide();
   }
   
   private void KitChenGameManager_OnStateChanged(object sender, EventArgs e)
   {
      if (KitChenGameManager.Instance.ISGameOver())
      {
         Show();
         recipesDeliveredText.text = DeliveryManager.Instance.GetSuccessfulRecipesAmuount().ToString();
      }
      else
      {
         Hide();
      }
   }

   private void Show()
   {
      gameObject.SetActive(true);
   }

   private void Hide()
   {
      gameObject.SetActive(false);
   }
}
