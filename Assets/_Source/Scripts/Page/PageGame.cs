using DG.Tweening;
using Utilities.Container;

public class PageGame : PanelBase, IContainer
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
        base.Start();

        Game.Instance.Add(this);
    }
}