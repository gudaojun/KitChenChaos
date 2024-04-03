using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 干净的货台
/// </summary>
public class ClearCounter : BaseCounter
{
    /// <summary>
    /// 厨房物品数据结构
    /// </summary>
    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    
    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //没有厨房用品
            if (player.HasKitchenObject())
            {
                //玩家有厨房用品 把用品放到台上
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
        }
        else
        {
            //有厨房用品
            if (player.HasKitchenObject())
            {
                //玩家有厨房用品 是否是盘子
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSo()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                }
                else  //玩家不持有盘子而且其他物体
                {
                    if (GetKitchenObject().TryGetPlate(out  plateKitchenObject))
                    {
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSo()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }
            else
            {
                //
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }


}
