using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GamePingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerIamge;

    private void Update()
    {
        timerIamge.fillAmount = KitChenGameManager.Instance.GetGamePlayingTimerNormalized();
    }
}
