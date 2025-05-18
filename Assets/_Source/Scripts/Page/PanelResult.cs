using DG.Tweening;
using TMPro;
using UnityEngine;

public class PanelResult : PanelBase
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _textReward;
    [SerializeField] private TextMeshProUGUI _textTimeReward;
    [SerializeField] private GameObject _titleTimeReward;

    private int _reward;

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
            Join(_components[0].DOScale(1, _delay).From(0).SetEase(Ease.OutBack)).
            OnComplete(AddMoney);
    }

    private void AddMoney()
    {
        Game.Wallet.Add(_reward);
        Game.Audio.PlayClip(1);
    }

    protected override void Start()
    {
        base.Start();

        Game.Action.OnWin += Action_OnWin;
        Game.Action.OnLose += Action_OnLose;
    }

    private void Action_OnLose() => UpdateResult(false);
    private void Action_OnWin() => UpdateResult(true);

    private void UpdateResult(bool isWin)
    {
        int countPuzzle = IncreaseValue.Int(isWin ? 10 : 5, Game.Instance.Single<PuzzleController>().Complated, 1.05f);

        _textReward.text = $"+{countPuzzle}<sprite=1>";

        if (isWin)
        {
            int timeReward = Mathf.RoundToInt(Game.Instance.Single<PuzzleTimer>().CurrentTime);
            _textTimeReward.text = $"+{timeReward}<sprite=1>";

            _reward = countPuzzle + timeReward;
        }
        else _reward = countPuzzle;


        _text.text = isWin ? "Complated!" : "Time is up!";
        _textTimeReward.gameObject.SetActive(isWin);
        _titleTimeReward.SetActive(isWin);

        Enter();
    }
}