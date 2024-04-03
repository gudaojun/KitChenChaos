using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using UnityEngine;

/// <summary>
/// 倒计时UI
/// </summary>
public class GameStartCountdownUI : MonoBehaviour
{

   private const string NUMBER_POPUP = "NumberPopup";
   
   [SerializeField] private TextMeshProUGUI countdownText;

   private Animator animator;
   private int preiousCountdownNumber;

   private void Awake()
   {
      animator = GetComponent<Animator>();
   }

   private void Start()
   {
      KitChenGameManager.Instance.OnStateChanged += KitChenGameManager_OnStateChanged;
      Hide();
   }

   private void Update()
   {
      int countdownNumber=Mathf.CeilToInt( KitChenGameManager.Instance.GetCountdownTimer());
      countdownText.text = countdownNumber.ToString();
      if (preiousCountdownNumber!=countdownNumber)
      {
         preiousCountdownNumber = countdownNumber;
         animator.SetTrigger(NUMBER_POPUP);
         SoundManager.Instance.PlayCountdownSound();
      }
   }

   private void KitChenGameManager_OnStateChanged(object sender, EventArgs e)
   {
      if (KitChenGameManager.Instance.IsCountdownStartActive())
      {
         Show();
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
