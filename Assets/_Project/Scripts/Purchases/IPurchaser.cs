using UnityEngine.Purchasing;

namespace _Project.Scripts.Purchases
{
    public interface IPurchaser
    {
        public bool IsAdsRemoved { get; }
        public void OnPurchasePending(PendingOrder order);
        public void OnPurchasesFetched(Orders existingOrders);
    }
}
