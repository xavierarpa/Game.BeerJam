using UnityEngine;
public sealed class AudioManager : MonoBehaviour
{
    private const float MAX_dB = 55f;
    private static AudioManager _;
    [SerializeField] private AudioSource src_music;
    [SerializeField] private AudioSource src_sound;
    private void Awake()
    {
        _ = this;
    }
    public static void PlayMusic(AudioClip clip)
    {
        _.src_music.clip = clip;
        _.src_music.Play();
    }
    public static void PlaySound(AudioClip clip, float pitch = 1)
    {
        _.src_sound.pitch = pitch;
        _.src_sound.PlayOneShot(clip);
    }
}