using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    private int _collectedCoins = 0;
    private int _totalCoinsInScene = 0; 

    [SerializeField] 
    private TextMeshProUGUI coinText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
     
        _totalCoinsInScene = GameObject.FindGameObjectsWithTag("Coin").Length;
        
        UpdateCoinText();
        
        Debug.Log("Total de Abóboras na cena: " + _totalCoinsInScene);
    }

    public void IncreaseCoins(int amount = 1)
    {
        _collectedCoins += amount;
        UpdateCoinText();
        
        
        Debug.Log("Abóboras Coletadas: " + _collectedCoins + " | Abóboras Restantes: " + (_totalCoinsInScene - _collectedCoins));
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            int remainingCoins = _totalCoinsInScene - _collectedCoins;
            
            coinText.text = "Abóboras restantes: " + remainingCoins.ToString();
            
            
            if (remainingCoins <= 0)
            {
                coinText.text = "Todas as Abóboras coletadas!";
            }
        }
    }
}