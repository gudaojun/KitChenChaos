using System.Collections;
using UnityEngine;
    public interface IkitchenObjectPanrent
    {
        public Transform GetKitchenObjectFollowTransform();


        public void SetkitchenObject(KitchenObject kitchenObject);

        public KitchenObject GetKitchenObject();


        public void ClearKitchenObject();

        public bool HasKitchenObject();

    }
