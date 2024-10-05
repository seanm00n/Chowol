using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GV;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;

    private static SoundManager _instance;

    private void Awake() {
        DontDestroyOnLoad(this);
        if( _instance == null) {
            _instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        _audioSource = GetComponent<AudioSource>();
        if(!_audioSource.isPlaying) {
            _audioSource.Play();
        }
    }
}
