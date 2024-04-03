using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
//烧坏了配方数据
public class BurnedRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float burnedTimerMax;
}
