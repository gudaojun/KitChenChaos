using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    /// <summary>
    /// 通知盘子有新的食材
    /// </summary>
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    private List<KitchenObjectSO> KitchenObjectSoList;

    private void Awake()
    {
        KitchenObjectSoList = new List<KitchenObjectSO>();
    }

    /// <summary>
    /// 添加物品到盘子
    /// </summary>
    /// <param name="kitchenObjectSo"></param>
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSo)
    {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSo))
        {
            return false;
        }
        //是否盘子里有相同的东西
        if (KitchenObjectSoList.Contains(kitchenObjectSo))
        {
            return false;
        }
        else
        {
            KitchenObjectSoList.Add(kitchenObjectSo);
            OnIngredientAdded?.Invoke(this,new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSo
            });
            return true;
        }
        
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return KitchenObjectSoList;
    }
}
