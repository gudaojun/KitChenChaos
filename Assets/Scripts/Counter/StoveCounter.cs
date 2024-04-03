using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

/// <summary>
/// 炉子台
/// </summary>
public class StoveCounter : BaseCounter, IHasProgress
{
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    
    public EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }

    public enum State
    {
        Idle, //默认
        Frying, //油炸中
        Fried, //油炸完一段时间
        Burned //炸过了
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSoArray;
    [SerializeField] private BurnedRecipeSO[] BurnedRecipeSoArray;

    private float fryingTimer;
    private float burnedTimer;
    private FryingRecipeSO fryingRecipeSo;
    private BurnedRecipeSO burnedRecipeSo;
    private State _state;
    private IHasProgress _hasProgressImplementation;

    private void Start()
    {
        _state = State.Idle;
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (_state)
            {
                case State.Idle:
                    break;
                case State.Frying:
                    //计时后生成新的物品
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = fryingTimer / fryingRecipeSo.fryingTimerMax
                    });

                    if (fryingTimer > fryingRecipeSo.fryingTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSo.output, this);

                        burnedTimer = 0;
                        burnedRecipeSo = GetBurnedRecipeSOWihtInput(GetKitchenObject().GetKitchenObjectSo());
                        _state = State.Fried;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            state = _state
                        });
                    }

                    break;
                case State.Fried:
                    burnedTimer += Time.deltaTime;
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized =burnedTimer/burnedRecipeSo.burnedTimerMax
                    });
                    if (burnedTimer > burnedRecipeSo.burnedTimerMax)
                    {
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burnedRecipeSo.output, this);
                        _state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            state = _state
                        });
                        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                        {
                            ProgressNormalized =0
                        });
                    }

                    break;
                case State.Burned:
                    break;
            }
        }
    }

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
                    fryingRecipeSo = GetFryingRecipeSOWihtInput(GetKitchenObject().GetKitchenObjectSo());
                    _state = State.Frying;

                    fryingTimer = 0f;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                    {
                        state = _state
                    });
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        ProgressNormalized = fryingTimer / fryingRecipeSo.fryingTimerMax
                    });
                }
            }
        }
        else
        {
            //有厨房用品
            if (player.HasKitchenObject())
            {
                //玩家有厨房用品 比较两个厨房用品是否相同
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSo()))
                    {
                        GetKitchenObject().DestroySelf();
                        _state= State.Idle;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                        {
                            state = _state
                        });
                        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                        {
                            ProgressNormalized =0
                        });
                    }
                }
            }
            else
            {
                //
                GetKitchenObject().SetKitchenObjectParent(player);
                _state = State.Idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs()
                {
                    state = _state
                });
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                {
                    ProgressNormalized =0
                });
            }
        }
    }

    private KitchenObjectSO GetOutPutForInput(KitchenObjectSO inputKitchenObjectSo)
    {
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWihtInput(inputKitchenObjectSo);
        if (fryingRecipeSo != null)
        {
            return fryingRecipeSo.output;
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
        FryingRecipeSO fryingRecipeSo = GetFryingRecipeSOWihtInput(inputKitchenObjectSo);
        return fryingRecipeSo != null;
    }

    /// <summary>
    /// 获取配方数据
    /// </summary>
    /// <param name="inputKitchenObjectSo"></param>
    /// <returns></returns>
    private FryingRecipeSO GetFryingRecipeSOWihtInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (FryingRecipeSO fryingRecipSO in fryingRecipeSoArray)
        {
            if (fryingRecipSO.input == inputKitchenObjectSo)
            {
                return fryingRecipSO;
            }
        }

        return null;
    }

    /// <summary>
    /// 获取烧过了的配方数据
    /// </summary>
    /// <param name="inputKitchenObjectSo"></param>
    /// <returns></returns>
    private BurnedRecipeSO GetBurnedRecipeSOWihtInput(KitchenObjectSO inputKitchenObjectSo)
    {
        foreach (BurnedRecipeSO burnedRecipe in BurnedRecipeSoArray)
        {
            if (burnedRecipe.input == inputKitchenObjectSo)
            {
                return burnedRecipe;
            }
        }

        return null;
    }

    public bool IsFried()
    {
        return _state == State.Fried;
    }
}