using TMPro;
using UnityEngine;

public class PanelBuyStage : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private GameObject _panelBuy;

    [SerializeField] private AudioClip _clip;

    private ButtonStage _stage;
    public ButtonStage Stage 
    {
        get => _stage;
        set
        {
            _stage = value;
            _panelBuy.SetActive(true);
            _priceText.text = _stage.Price.ToString();
        }
    }

    public void Purchased()
    {
        if(Game.Wallet.Spend(Stage.Price))
        {
            Stage.IsPurchased = true;
            _panelBuy.SetActive(false);
            Game.Audio.PlayClip(_clip);
        }
    }
}