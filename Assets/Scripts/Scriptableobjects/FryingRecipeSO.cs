using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
//油炸配方数据
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float fryingTimerMax;
}
