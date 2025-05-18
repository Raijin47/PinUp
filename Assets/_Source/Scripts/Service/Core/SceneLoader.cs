using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class SceneLoader
{
    [SerializeField] private CanvasGroup _loadingScreen;
    [SerializeField] private GameObject _content;
    [SerializeField] private TextMeshProUGUI _progressText;
    [SerializeField] private Slider _progressBar;

    private readonly float Duration = 0.5f;

    private int _current;

    public IEnumerator Init()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(1);

        yield return new WaitUntil(() => load.isDone);

        yield return FadeLocation(0);

        _content.SetActive(false);
    }

    public IEnumerator LoadScene(int index, LoadSceneMode mode)
    {
        yield return FadeLocation(1);

        _content.SetActive(true);
        _progressBar.value = 0;
        _progressText.text = "0%";

        _current = index;
        AsyncOperation load = SceneManager.LoadSceneAsync(index, mode);

        while (!load.isDone)
        {
            _progressBar.value = load.progress;
            _progressText.text = $"{Mathf.RoundToInt(load.progress * 100)}%";
            yield return null;
        }

        yield return new WaitUntil(() => load.isDone);

        _content.SetActive(false);

        yield return FadeLocation(0);
    }

    private YieldInstruction FadeLocation(float value)
    {
        return DOTween.
            To(() => _loadingScreen.alpha, x => _loadingScreen.alpha = x, value, Duration).
            SetEase(Ease.Linear).
            WaitForCompletion();
    }
}