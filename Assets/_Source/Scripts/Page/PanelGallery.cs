using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities.Container;

public class PanelGallery : PanelBase, IContainer
{
    [SerializeField] private ButtonBase _previewButton, _nextButton, _closeButton, _openButton;
    [SerializeField] private Image _image;

    [SerializeField] private Sprite _emptySprite;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Sprite[] _sprites;
    private int _current;

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

        _openButton.OnClick.AddListener(Open);
        _closeButton.OnClick.AddListener(Exit);
        _nextButton.OnClick.AddListener(Next);
        _previewButton.OnClick.AddListener(Preview);
    }

    private void Open()
    {
        bool isClosed = Game.Data.Saves.Gallery.Count == 0;

        _image.sprite = isClosed ? _emptySprite : _sprites[Game.Data.Saves.Gallery[_current]];
        _text.text = isClosed ? "Pass the levels to unlock the gallery!" : "";

        Enter();
    }

    private void Next()
    {
        if (Game.Data.Saves.Gallery.Count == 0)
        {
            Game.Audio.PlayClip(2);
            return;
        }

        _current++;

        if(_current >= Game.Data.Saves.Gallery.Count) _current = 0;

        _image.sprite = _sprites[Game.Data.Saves.Gallery[_current]];
    }

    private void Preview()
    {
        if (Game.Data.Saves.Gallery.Count == 0)
        {
            Game.Audio.PlayClip(2);
            return;
        }

        _current--;

        if (_current < 0) _current = Game.Data.Saves.Gallery.Count - 1;

        _image.sprite = _sprites[Game.Data.Saves.Gallery[_current]];
    }
}