using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 交付管理
/// </summary>
/// 负责交付管理验证
public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    
    //配方生成
    public event EventHandler OnRecipeSpawned;
    //交付配方
    public event EventHandler OnRecipeCompleted;
    /// <summary>
    /// 播放交付配方成功声音
    /// </summary>
    public event EventHandler OnRecipeSuccess;
    
    /// <summary>
    /// 播放交付配方失败声音
    /// </summary>
    public event EventHandler OnRecipeFailed;
    
    
    [SerializeField] private RecipeListSO recipeListSo;

    private List<RecipeSO> waitingRecipeSOList;

    //生成配方计时器
    private float SpawnRecipeTimer;
    ////生成配方计时器等待时间
    private float SpawnRecipeTimerMax=4f;
    private int waitingRecipesMax = 4;
    /// <summary>
    /// 成功交付的配方数量
    /// </summary>
    private int successfulRecipesAmuount;
    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update()
    {
        //计时器生成等待配方
        SpawnRecipeTimer -= Time.deltaTime;
        if (SpawnRecipeTimer<=0f)
        {
            SpawnRecipeTimer = SpawnRecipeTimerMax;
            if (KitChenGameManager.Instance.IsGamePlaying()&&waitingRecipeSOList.Count<waitingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSo.recipeSOList[Random.Range(0, recipeListSo.recipeSOList.Count)];
                Debug.Log(waitingRecipeSO.recipeName);
                waitingRecipeSOList.Add(waitingRecipeSO);    
                
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }

    /// <summary>
    /// 交付
    /// 验证是否符合
    /// </summary>
    /// <param name="plateKitchenObject"></param>
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count==plateKitchenObject.GetKitchenObjectSOList().Count)
            {  //等待交付的配方列表与盘子含有的物品长度是否相同

                bool plateContentMatchesRecipe = true; 
                foreach (KitchenObjectSO recipeKitchenObjectSo in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    //对比等待配方中的物品与盘子中含有的物品是否相同
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if (plateKitchenObjectSO==recipeKitchenObjectSo)
                        {
                            //成分匹配
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound)
                    {
                        plateContentMatchesRecipe = false;
                    }
                }

                if (plateContentMatchesRecipe)
                {
                    Debug.Log("交付正确");

                    successfulRecipesAmuount++;
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeCompleted(this,EventArgs.Empty);  
                    OnRecipeSuccess(this,EventArgs.Empty);
                    return;
                }
                
            }
        }
        Debug.Log("交付错误");
        OnRecipeFailed(this, EventArgs.Empty);
        //没有匹配的配方物品
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }

    public int GetSuccessfulRecipesAmuount()
    {
        return successfulRecipesAmuount;
    }
}
