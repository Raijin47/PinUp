using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities.Container;
using Utilities.Singleton;
using UnityEngine.SceneManagement;

public class Game : SingleBehavior<Game, DontDestroyModifyPattern>
{
    [SerializeField] private bool _installer;

    [Space(10)]
    [SerializeField] private GameAudio _audio;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private AudioSpriteSwap _audioSettings;
    [SerializeField] private SceneLoader _loadScreen;

    private readonly GameAction GameAction = new();
    private readonly SaveService SaveService = new();
    private readonly Dictionary<Type, List<IContainer>> MapContainers = new();

    public static GameAudio Audio;
    public static Wallet Wallet;
    public static GameAction Action;
    public static SaveService Data;

    public AudioSpriteSwap AudioSettings => _audioSettings;

    protected override void Awake()
    {
        base.Awake();

        Audio = _audio;
        Wallet = _wallet;
        Action = GameAction;
        Data = SaveService;
    }

    private void Start()
    {
        _audioSettings.Init();
        _audio.Init();
        SaveService.LoadingData();
        _wallet.Init();

        if (_installer)
        {
            _loadScreen.Init();
            StartCoroutine(_loadScreen.Init());
        }
    }

    public void LoadScene(int index, LoadSceneMode mode) => StartCoroutine(_loadScreen.LoadScene(index, mode));

    public Game Add(IContainer container, bool dontDestroyParent = false)
    {
        if (MapContainers.ContainsKey(container.GetType()))
        {
            MapContainers[container.GetType()].Add(container);
        }
        else
        {
            MapContainers[container.GetType()] = new List<IContainer>() { container };
        }

        if (dontDestroyParent)
            if (container is MonoBehaviour monoBehaviour)
            {
                monoBehaviour.transform.SetParent(transform);
            }

        if (container is IHavingInitContainer havingInitContainer)
        {
            havingInitContainer.Init();
        }

        return this;
    }

    public void Remove(IContainer container)
    {
        if (MapContainers.ContainsKey(container.GetType()))
        {
            MapContainers[container.GetType()].Remove(container);
        }
    }

    public T Single<T>() where T : IContainer
    {
        var temp = Entities<T>();
        return temp.FirstOrDefault();
    }

    public IEnumerable<T> Entities<T>() where T : IContainer
    {
        return !MapContainers.ContainsKey(typeof(T)) ? default : MapContainers[typeof(T)].Select(item => (T)item);
    }
}