using DG.Tweening;
using TMPro;
using UnityEngine;
using Utilities.Container;

public class PanelSelectable : PanelBase, IContainer
{
    [SerializeField] private ButtonBase _buttonBuy, _buttonClose;
    [SerializeField] private TextMeshProUGUI _priceText;

    private ButtonStage _stage;
    public ButtonStage Stage 
    {
        get => _stage;
        set
        {
            _stage = value;
            _priceText.text = $"{value.Price}<sprite=1>";
        }
    }

    protected override void Hide()
    {
        _sequence.
            Append(_canvas.DOFade(0, _delay)).
            Join(_components[0].DOScale(0, _delay).SetEase(Ease.InBack));
    }

    protected override void Show()
    {
        OnShowComplated();

        _sequence.
            Append(_canvas.DOFade(1, _delay)).
            Join(_components[0].DOScale(1, _delay).From(0).SetEase(Ease.OutBack));
    }

    protected override void Start()
    {
        base.Start();

        Game.Instance.Add(this);

        _buttonBuy.OnClick.AddListener(Buy);
        _buttonClose.OnClick.AddListener(Exit);
    }

    private void Buy()
    {
        if (Game.Wallet.Spend(_stage.Price))
        {
            Exit();
            _stage.IsPurchased = true;
        }

        else Game.Audio.PlayClip(2);
    }
}