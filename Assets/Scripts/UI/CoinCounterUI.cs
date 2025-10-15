using UnityEngine;
using TMPro;

public class CoinCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    private void Awake()
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        PlayerCollectibleCounter.OnCoinCountChanged += UpdateText;
    }

    private void OnDisable()
    {
        PlayerCollectibleCounter.OnCoinCountChanged -= UpdateText;
    }

    private void UpdateText(int count)
    {
        text.text = $"Coins: {count}";
    }
}
