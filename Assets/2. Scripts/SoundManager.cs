using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GV;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    AudioClip[] _audioClips;

    private AudioSource _audioSource;
    private AudioSource _effectAudioSource;

    private static SoundManager _instance;

    public static SoundManager Instance {
        get {
            if(_instance == null) {
                _instance = new SoundManager();
            }
            return _instance;
        }
    }

    private void Awake() {
        DontDestroyOnLoad(this);
        if( _instance == null) {
            _instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        _audioSource = GetComponent<AudioSource>();
        _effectAudioSource = GetComponent<AudioSource>();
        if(!_audioSource.isPlaying) {
            _audioSource.clip = _audioClips[0];
            _audioSource.Play();
        }
    }

    public void PlayCardSelect() {
        _effectAudioSource.clip = _audioClips[1];
        _effectAudioSource.Play();
    }

    public void PlayCardSwap() {
        _effectAudioSource.clip = _audioClips[2];
        _effectAudioSource.Play();
    }

    public void PlayTileBreak() {
        _effectAudioSource.clip = _audioClips[3];
        _effectAudioSource.Play();
    }
}
