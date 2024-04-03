using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IkitchenObjectPanrent
{
    /// <summary>
    /// 玩家放下东西播放声音事件
    /// </summary>
    public static event EventHandler OnAnyobjectPlacedHere;
    
    public static void ResetStaticData()
    {
        OnAnyobjectPlacedHere = null;
    }
    
    /// <summary>
    /// 生成坐标点
    /// </summary>
    [SerializeField] private Transform counterTopPoint;
    
    
    /// <summary>
    /// 当前持有的厨房物品
    /// </summary>
    private KitchenObject kitchenObject;
    public virtual void Interact(Player player)
    {
        
    }

    public virtual void InteractAlternate(Player player)
    {
        
    }
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }

    public void SetkitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;

        if (kitchenObject!=null)
        {
            OnAnyobjectPlacedHere?.Invoke(this,EventArgs.Empty);
        }
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}