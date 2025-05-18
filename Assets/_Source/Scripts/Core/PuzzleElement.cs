using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public RectTransform Target { get; set; }

    private Image _image;
    private RectTransform _main;

    private const float _offset = 50f;

    public bool IsComplated { get; set; }

    public Sprite Sprite 
    { 
        set 
        {
            _image.sprite = value;
            _image.SetNativeSize();
        }
    }

    private void Awake()
    {
        _image = GetComponent<Image>();
        _main = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsComplated) return;

        transform.position = Input.mousePosition;
        Game.Instance.Single<PuzzleController>().Shadow.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsComplated) return;

        Game.Instance.Single<PuzzleController>().Shadow.transform.position = transform.position;
        Game.Instance.Single<PuzzleController>().Shadow.gameObject.SetActive(true);

        transform.localScale = Vector3.one * 1.05f;

        transform.SetParent(Game.Instance.Single<PuzzleController>().ContentActive);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsComplated) return;
        Game.Audio.PlayClip(0);
        Game.Instance.Single<PuzzleController>().Shadow.gameObject.SetActive(false);
        transform.localScale = Vector3.one;

        if (Mathf.Abs(_main.transform.position.x - Target.transform.position.x) <= _offset &&
            Mathf.Abs(_main.transform.position.y - Target.transform.position.y) <= _offset)
        {
            _main.anchoredPosition = Target.anchoredPosition;
            transform.SetParent(Game.Instance.Single<PuzzleController>().ContentComplated);
            IsComplated = true;

            if (Game.Instance.Single<PuzzleController>().CheckComplated())        
                Game.Action.SendWin();           
        }
        else transform.SetParent(Game.Instance.Single<PuzzleController>().ContentMiss);
    }
}