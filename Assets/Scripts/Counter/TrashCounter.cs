using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTransh;
     public new static void ResetStaticData()
    {
        OnTransh = null;
    }
    
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnTransh?.Invoke(this,EventArgs.Empty);
        }
    }
}
