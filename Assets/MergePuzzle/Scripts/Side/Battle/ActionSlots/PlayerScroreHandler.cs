using UnityEngine;
using UnityEngine.UI;

public class PlayerScroreHandler : MonoBehaviour
{
    [SerializeField] private CoinSlot coinSlot;
    [SerializeField] private Text playerScore;

    private int currentPlayerScore;

    private void Awake()
    {
        coinSlot.CoinAdded += CoinSlot_CoinAdded;
        currentPlayerScore = PlayerPrefs.GetInt(Constants.SCORE_INT);
        UpdateScoreUI();
    }

    private void CoinSlot_CoinAdded(object sender, System.EventArgs e)
    {
        SupportEventArgs args = (SupportEventArgs)e;
        if (args.GetItemType() == ItemType.Coin)
        {
            var level = args.GetLevel() + 1;
            currentPlayerScore += level * Constants.COIN_MULTIPLYER;
            UpdateScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        playerScore.text = currentPlayerScore.ToString();
    }

    private void OnDisable()
    {
        // save score
        PlayerPrefs.SetInt(Constants.SCORE_INT, currentPlayerScore);
    }
}

