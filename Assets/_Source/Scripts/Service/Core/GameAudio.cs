using System;
using UnityEngine;

[Serializable]
public class GameAudio
{
    [SerializeField] private AudioSource _audioSource;

    [Space(10)]
    [SerializeField] private AudioClip _onClick;
    [SerializeField] private AudioClip _onWin;
    [SerializeField] private AudioClip _onLose;
    [SerializeField] private AudioClip _onSpendMoney;

    [Space(10)]
    [SerializeField] private AudioClip[] _clips;

    public void Init()
    {
        Game.Action.OnLose += () => _audioSource.PlayOneShot(_onLose);
        Game.Action.OnWin += () => _audioSource.PlayOneShot(_onWin);
        Game.Wallet.OnSpendMoney += () => { _audioSource.PlayOneShot(_onSpendMoney); };
    }

    public void OnClick() => _audioSource.PlayOneShot(_onClick);
    public void PlayClip(AudioClip clip, float volume = 1) => _audioSource.PlayOneShot(clip, volume);
    public void PlayClip(int index, float volume = 1) => _audioSource.PlayOneShot(_clips[index], volume);
}