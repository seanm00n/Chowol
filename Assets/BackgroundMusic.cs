using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource _audioSource;

    private static BackgroundMusic _instance;

    public static BackgroundMusic Instance {
        get {
            if(_instance == null) {
                _instance = new BackgroundMusic();
            }
            return _instance;
        }
    }

    private void Awake() {
        DontDestroyOnLoad(this);
        if(_instance == null) {
            _instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }
}
