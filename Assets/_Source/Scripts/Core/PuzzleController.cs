using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoContainer
{
    [SerializeField] private PuzzleElement[] _puzzleElements;
    [SerializeField] private RectTransform[] _targetElements;

    [SerializeField] private GridLayoutGroup _grid;

    [Space(10)]
    [SerializeField] private Transform _contentScroll;
    [SerializeField] private Transform _contentMiss;
    [SerializeField] private Transform _contentActive;
    [SerializeField] private Transform _contentComplated;
    [SerializeField] private Image _imageReference;
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private TextMeshProUGUI _textCount;

    [Space(10)]
    [SerializeField] private RectTransform _shadow;
    [SerializeField] private ScrollRect _scrollRect;

    private int _count;
    private bool _isShowing;

    private readonly List<GameObject> PuzzleList = new();
    private readonly Vector2 Pivot = new(0.5f, 0.5f);
    private ButtonStage _stage;
    private Sequence _sequence;

    public int Complated { get; private set; }
    public RectTransform Shadow => _shadow;
    public Transform ContentMiss => _contentMiss;
    public Transform ContentActive => _contentActive;
    public Transform ContentComplated => _contentComplated;

    private void Start() 
    {
        for (int i = 0; i < _puzzleElements.Length; i++)
        {
            PuzzleList.Add(_puzzleElements[i].gameObject);
            _puzzleElements[i].Target = _targetElements[i];
        }

        Game.Action.OnWin += Action_OnWin;
    }

    private void Action_OnWin()
    {
        if (Game.Data.Saves.IsWin[_stage.Id]) return;

        _stage.IsWin = true;

        Game.Data.Saves.Gallery.Add(_stage.Id);
        Game.Data.Saves.IsWin[_stage.Id] = true;
        Game.Data.SaveProgress();
    }

    public void SetSetting(ButtonStage stage)
    {
        _stage = stage;

        int hw = 1024 / stage.Count;
        int a = 0;

        _imageReference.sprite = stage.Sprite;

        Vector2 size = new(hw, hw);

        _count = stage.Count * stage.Count;
        _grid.cellSize = size;
        _shadow.sizeDelta = size * 1.05f;

        for (int i = stage.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < stage.Count; j++)
            {
                Rect rect = new(j * hw, i * hw, hw, hw);
                _puzzleElements[a].Sprite = Sprite.Create(stage.Texture, rect, Pivot);
                _puzzleElements[a].gameObject.SetActive(true);
                _puzzleElements[a].transform.SetParent(_contentScroll);
                _puzzleElements[a].IsComplated = false;
                a++;
            }
        }

        for (; a < _puzzleElements.Length; a++)
        {
            _puzzleElements[a].gameObject.SetActive(false);
            _puzzleElements[a].IsComplated = true;
        }    
            
        Shuffle();
    }

    public void Restart()
    {
        for(int i = 0; i < _count; i++)
        {
            _puzzleElements[i].IsComplated = false;
            _puzzleElements[i].transform.SetParent(_contentScroll);
        }

        Shuffle();
    }

    private void Shuffle()
    {
        for (int i = 0; i < PuzzleList.Count; i++)
        {
            int randomIndex = Random.Range(i, PuzzleList.Count);
            GameObject temp = PuzzleList[i];
            PuzzleList[i] = PuzzleList[randomIndex];
            PuzzleList[randomIndex] = temp;
        }

        Complated = 0;
        Game.Instance.Single<PuzzleTimer>().StartTimer(_stage.Time);
        RepositionObjects();
    }

    public void Show()
    {
        if (_isShowing) return;

        _isShowing = true;

        _sequence?.Kill();

        _sequence = DOTween.Sequence();

        _sequence.
            Append(_canvas.DOFade(1, 0)).
            AppendInterval(5f).
            Append(_canvas.DOFade(0, 1)).OnComplete(() => 
            { 
                _isShowing = false;
            });
    }

    private void RepositionObjects()
    {
        for (int i = 0; i < PuzzleList.Count; i++)
        {
            PuzzleList[i].transform.SetSiblingIndex(i);
        }

        LayoutRebuilder.MarkLayoutForRebuild(_scrollRect.content.GetComponent<RectTransform>());
    }

    public bool CheckComplated()
    {
        Complated++;

        foreach(PuzzleElement puzzle in _puzzleElements)        
            if (!puzzle.IsComplated) return false;
        
        return true;
    }
}