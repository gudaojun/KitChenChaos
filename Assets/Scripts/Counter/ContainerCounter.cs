using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 容器货柜
/// </summary>
public class ContainerCounter : BaseCounter
{
    
    public event EventHandler OnPlayerGrabbedObject;
    
    /// <summary>
    /// 厨房物品数据结构
    /// </summary>
    [SerializeField] private KitchenObjectSO kitchenObjectSo;

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawnKitchenObject(kitchenObjectSo, player);
            OnPlayerGrabbedObject?.Invoke(this,EventArgs.Empty);
        }
    }

}
