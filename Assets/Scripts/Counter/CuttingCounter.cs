using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter,IHasProgress
{
    /// <summary>
    /// 切声音的事件
    /// </summary>
    public static event EventHandler OnAnyCut;

    public new static void ResetStaticData()
    {
        OnAnyCut = null;
    }
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    public EventHandler OnCut;
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;
    private int cuttingProgress;
    private IHasProgress _hasProgressImplementation;

    public override void Interact(Player player)
    {
        if (!HasKitchenObject())
        {
            //没有厨房用品
            if (player.HasKitchenObject())
            {
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSo()))
                {
                    //玩家有厨房用品 把用品放到台上
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;

                    CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWihtInput(GetKitchenObject().GetKitchenObjectSo());
                    //通知切菜进度UI
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs()
                    {
                        ProgressNormalized  =(float)cuttingProgress / cuttingRecipeSo.cuttingProgressMax
                    });
                    
                }
           
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

            }
            else
            {
                //
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player)
    {           //这是里是厨房用品并且可以被切
        if (HasKitchenObject()&&HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSo()))
        {
 
            cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSo=GetCuttingRecipeSOWihtInput(GetKitchenObject().GetKitchenObjectSo());
            //通知切菜进度UI
            OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs()
            {
                ProgressNormalized  =(float)cuttingProgress / cuttingRecipeSo.cuttingProgressMax
            });
            OnCut?.Invoke(this,EventArgs.Empty);
            OnAnyCut?.Invoke(this,EventArgs.Empty);
            if (cuttingProgress>=cuttingRecipeSo.cuttingProgressMax)
            {
                KitchenObjectSO outputKitchenObjectSo = GetOutPutForInput(GetKitchenObject().GetKitchenObjectSo());
                //持有厨房物品，要切开
                GetKitchenObject().DestroySelf();
                //创建新的切片
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSo,this);
            }
        }
    }

    private KitchenObjectSO GetOutPutForInput(KitchenObjectSO inputKitchenObjectSo)
    {
        CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWihtInput(inputKitchenObjectSo);
        if (cuttingRecipeSo!=null)
        {
            return cuttingRecipeSo.output;
        }
        else
        {
            return null;
        }

    }

    /// <summary>
    /// 是否有配方数据
    /// </summary>
    /// <param name="inputKitchenObjectSo"></param>
    /// <returns></returns>
    private bool HasRecipeWithInput(KitchenObjectSO inputKitchenObjectSo)
    {
        CuttingRecipeSO cuttingRecipeSo = GetCuttingRecipeSOWihtInput(inputKitchenObjectSo);
        return cuttingRecipeSo != null;
    }

    /// <summary>
    /// 获取配方数据
    /// </summary>
    /// <param name="inputKitchenObjectSo"></param>
    /// <returns></returns>
    private CuttingRecipeSO GetCuttingRecipeSOWihtInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (CuttingRecipeSO cuttingRecipeSo in cuttingRecipeSOArray)
        {
            
            if (cuttingRecipeSo.input == inputKitchenObjectSo)
            {
                return cuttingRecipeSo;
            }
        }

        return null;
    }
    
}
