using UnityEngine;
using UnityEngine.UI;

public class AudioService : Singleton<AudioService>
{
    [SerializeField]
    private AudioSource _musicSource;

    [SerializeField]
    private AudioSource _sfxSource;

    [SerializeField]
    private AudioSource _sfxDeathSource;
    private bool _isOn = true;

    [SerializeField]
    private AudioClip[] _clips;

    [SerializeField]
    private AudioClip[] _clipsDeath;
    private float _musicVolume;
    private float _sfxVolume;
    private float _sfxDeathVolume;

    [SerializeField]
    Sprite _on;

    [SerializeField]
    Sprite _off;

    [SerializeField]
    Button _musicButton;

    [SerializeField]
    Button _clickButton;

    private void Start()
    {
        _musicVolume = _musicSource.volume;
        _sfxVolume = _sfxSource.volume;
        _sfxDeathVolume = _sfxDeathSource.volume;
        _musicButton.onClick.AddListener(SwitchVolume);
        _clickButton.onClick.AddListener(PlaySfx);
    }

    private AudioClip RandomClip(AudioClip[] clips)
    {
        var clip = clips[Random.Range(0, clips.Length)];
        return clip;
    }

    private void PlaySfx()
    {
        var clip = RandomClip(_clips);
        _sfxSource.PlayOneShot(clip);
    }

    public void PlaySfxDeath()
    {
        var clip = RandomClip(_clipsDeath);
        _sfxDeathSource.PlayOneShot(clip);
    }

    private void SwitchVolume()
    {
        if (_isOn)
        {
            _musicSource.volume = 0f;
            _sfxSource.volume = 0f;
            _sfxDeathSource.volume = 0f;
            _musicButton.image.sprite = _off;
        }
        else
        {
            _musicSource.volume = _musicVolume;
            _sfxSource.volume = _sfxVolume;
            _sfxDeathSource.volume = _sfxDeathVolume;

            _musicButton.image.sprite = _on;
        }

        _isOn = !_isOn;
    }
}
