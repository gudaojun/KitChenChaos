using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 配方数据
/// </summary>
[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
   public List<KitchenObjectSO> kitchenObjectSOList;
   public string recipeName;

}
