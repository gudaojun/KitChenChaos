using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 厨房物品
/// </summary>
public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO _kitchenObjectSo;

    private IkitchenObjectPanrent KitchenObjectParent;
    public KitchenObjectSO GetKitchenObjectSo()
    {
        return _kitchenObjectSo;
    }

    public void SetKitchenObjectParent(IkitchenObjectPanrent KitchenObjectParent)
    {
        if (this.KitchenObjectParent!=null)
        {
            this.KitchenObjectParent.ClearKitchenObject();
        }
        this.KitchenObjectParent = KitchenObjectParent;
        if (KitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("已经存在厨房物品了");
            
        }
        KitchenObjectParent.SetkitchenObject(this);    
        
        transform.parent = KitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition=Vector3.zero;
    }

    public IkitchenObjectPanrent GetKitchenObjectParent()
    {
        return KitchenObjectParent;
    }

    public void DestroySelf()
    {
        KitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject)
    {
        if (this is PlateKitchenObject)
        {
            plateKitchenObject=this as PlateKitchenObject;
            return true;
        }
        else
        {
            plateKitchenObject = null;
            return false;
        }
    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO, IkitchenObjectPanrent kitchenObjectParent)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
        KitchenObject kitchenObject=kitchenObjectTransform.GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
            return kitchenObject;
    }
} 
