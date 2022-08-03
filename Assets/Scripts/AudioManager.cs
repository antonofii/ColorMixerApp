using UnityEngine;

public enum SoundType
{
    Blender,
    Choose,
    GoodResult,
    BadResult,
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip blenderSound;
    [SerializeField] private AudioClip chooseSound;
    [SerializeField] private AudioClip goodResultSound;
    [SerializeField] private AudioClip badResultSound;
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private float soundVolume;
    [SerializeField] private float musicVolume;
    private AudioSource soundPlayer;
    private AudioSource musicPlayer;

    private void Start()
    {
        soundPlayer = gameObject.AddComponent<AudioSource>();
        musicPlayer = gameObject.AddComponent<AudioSource>();
        PlayBackgroundMusic();
    }

    public void PlaySound(SoundType type) 
    {
        switch (type)
        {
            case SoundType.Blender:
                PlayBlenderSound();
                break;
            case SoundType.BadResult:
                PlayBadResultSound();
                break;
            case SoundType.GoodResult:
                PlayGoodResultSound();
                break;
            case SoundType.Choose:
                PlayChooseSound();
                break;
        }
    }

    private void PlayBlenderSound()
    {
        soundPlayer.clip = blenderSound;
        soundPlayer.Play();
    }

    private void PlayChooseSound()
    {
        soundPlayer.clip = chooseSound;
        soundPlayer.Play();
    }

    private void PlayGoodResultSound()
    {
        soundPlayer.clip = goodResultSound;
        soundPlayer.Play();
    }

    private void PlayBadResultSound()
    {
        soundPlayer.clip = badResultSound;
        soundPlayer.Play();
    }

    private void PlayBackgroundMusic()
    {
        musicPlayer.clip = backgroundMusic;
        musicPlayer.volume = musicVolume;
        musicPlayer.Play();
        musicPlayer.loop = true;
    }
}
