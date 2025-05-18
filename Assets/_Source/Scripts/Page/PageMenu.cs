using DG.Tweening;

public class PageMenu : PanelBase
{
    protected override void Hide()
    {
        _sequence.
            Append(_canvas.DOFade(0, _delay));
    }

    protected override void Show()
    {
        _sequence.
            AppendInterval(_delay).
            Append(_canvas.DOFade(1, _delay)).
            OnComplete(OnShowComplated);
    }

    protected override void Start()
    {
        IsActive = true;
        _canvas.alpha = 1;
    }
}