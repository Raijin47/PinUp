using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : ButtonBase
{
    [SerializeField] private int _sceneBuildIndex;

    public int SceneBuildIndex
    {
        get => _sceneBuildIndex;
        set => _sceneBuildIndex = value;
    }

    protected override void Click()
    {
        base.Click();

        Game.Instance.LoadScene(_sceneBuildIndex, LoadSceneMode.Single);
    }
}