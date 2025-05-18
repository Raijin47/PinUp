using System.Collections;
using TMPro;
using UnityEngine;

public class PuzzleTimer : MonoContainer
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
        Game.Action.OnWin += Release;
    }

    public void StartTimer(float value)
    {
        CurrentTime = value + 1;

        Release();
        _coroutine = StartCoroutine(UpdateProcess());
    }

    private IEnumerator UpdateProcess()
    {
        while (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;

            if(Mathf.RoundToInt(CurrentTime) % 30 == 0)            
                Game.Instance.Single<PuzzleController>().Show();
            
            yield return null;
        }

        CurrentTime = 0;
        Game.Action.SendLose();
    }

    public void Release()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}