using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GV;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] _audioClips;

    private AudioSource _audioSource;

    private void Awake() {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayCardSelect() {
        _audioSource.clip = _audioClips[0];
        _audioSource.Play();
    }

    public void PlayCardSwap() {
        _audioSource.clip = _audioClips[1];
        _audioSource.Play();
    }

    public void PlayTileBreak() {
        _audioSource.clip = _audioClips[2];
        _audioSource.Play();
    }
}
