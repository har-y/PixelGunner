using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio - Source")]
    [SerializeField] private AudioSource _audioSource;
    private Camera _camera;

    [Header("Audio")]
    [SerializeField] private AudioClip[] _musicClip;
    [SerializeField] private AudioClip[] _soundClip;
    [Range(0f, 1f)][SerializeField] private float _audioVolume;
    [Range(0f, 1f)][SerializeField] private float _afxVolume;
    [SerializeField] private bool _audio;
    [SerializeField] private bool _afx;

    [Header("Audio - Clip")]
    [SerializeField] private int _playMusicClip;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;

        PlayMusic(_playMusicClip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(int clip)
    {
        if (!_audioSource || !_audio)
        {
            return;
        }

        _audioSource.Stop();
        _audioSource.clip = _musicClip[clip];
        _audioSource.volume = _audioVolume;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    public void PlaySoundClip(int clip)
    {
        if (_afx)
        {
            AudioSource.PlayClipAtPoint(_soundClip[clip], _camera.transform.position, Mathf.Clamp(_afxVolume, 0.5f, 1f));
        }
    }

    public void ToggleAudio()
    {
        _audio = !_audio;

        if (_audio)
        {
            _audioSource.UnPause();
        }
        else
        {
            _audioSource.Pause();
        }
    }

    public void ToggleAFX()
    {
        _afx = !_afx;
    }
}
