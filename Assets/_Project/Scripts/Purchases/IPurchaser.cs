using _Project.Scripts.Saves;
using UnityEngine.Purchasing;

namespace _Project.Scripts.Purchases
{
    public interface IPurchaser
    {
        public bool IsAdsRemoved { get; }
        public void OnPurchaseButtonClicked(string productId);
    }
}
