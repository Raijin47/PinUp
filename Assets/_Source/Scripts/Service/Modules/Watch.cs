using System.Collections;
using UnityEngine;
using TMPro;

public class Watch : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private Coroutine _coroutine;

    private float _currentTime;

    public float CurrentTime 
    {
        get => _currentTime;
        set
        {
            _currentTime = value;
            _text.text = TextUtility.FormatMinute(_currentTime);
        }
    }

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        Game.Action.InGame += Action_InGame;
    }

    private void Action_InGame(bool inGame)
    {
        if (inGame)
        {
            CurrentTime = 0;

            _coroutine = StartCoroutine(UpdateProcess());
        }
        else Release();
    }

    private IEnumerator UpdateProcess()
    {
        while (true)
        {
            CurrentTime += Time.deltaTime;
            yield return null;
        }        
    }

    private void Release()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}