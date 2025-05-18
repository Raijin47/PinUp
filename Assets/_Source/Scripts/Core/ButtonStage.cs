using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonStage : ButtonBase
{
    public int Count => _size;
    public Texture2D Texture => _texture;
    public Sprite Sprite => _sprite.sprite;
    public int Id => _id;
    public int Price => _price;
    public bool IsPurchased
    {
        get => Game.Data.Saves.IsPurchased[_id];
        set
        {
            Game.Data.Saves.IsPurchased[_id] = value;
            Game.Data.SaveProgress();

            UpdateUI();
        }
    }
    public bool IsWin
    {
        set
        {
            _object.SetActive(value);
        }
    }

    public float Time => _time;

    [SerializeField] private int _id;
    [SerializeField] private int _size = 8;
    [SerializeField] private int _price;
    [SerializeField] private float _time;
    [SerializeField] private GameObject _object;

    private Image _sprite;
    private Texture2D _texture;
    private TextMeshProUGUI _priceText;

    protected override void Awake()
    {
        base.Awake();

        _sprite = GetComponent<Image>();
        _texture = _sprite.sprite.texture;
        _priceText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{_size * _size}<sprite=0>";
        UpdateUI();

        IsWin = Game.Data.Saves.IsWin[_id];
    }


    protected override void Click()
    {
        if(IsPurchased)
        {
            Game.Instance.Single<PageGame>().Enter();
            Game.Instance.Single<PageLevel>().Exit();
            Game.Instance.Single<PuzzleController>().SetSetting(this);
        }
        else
        {
            var panel = Game.Instance.Single<PanelSelectable>();

            panel.Enter();
            panel.Stage = this;
        }
    }

    private void UpdateUI()
    {
        _priceText.text = IsPurchased ? "" : $"{_price}<sprite=1>";
        _sprite.color = IsPurchased ? Color.white : Color.gray;
    }
}