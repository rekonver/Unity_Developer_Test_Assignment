using UnityEngine;

public class PlayerCollectibleCounter : MonoBehaviour
{
    public int CoinCount { get; private set; }

    public static event System.Action<int> OnCoinCountChanged;

    void OnEnable()
    {
        Coin.OnCollected += HandleCoinCollected;
    }

    void OnDisable()
    {
        Coin.OnCollected -= HandleCoinCollected;
    }

    private void HandleCoinCollected(Coin coin)
    {
        CoinCount++;
        Debug.Log($"Монет зібрано: {CoinCount}");
        OnCoinCountChanged?.Invoke(CoinCount);
    }
}
