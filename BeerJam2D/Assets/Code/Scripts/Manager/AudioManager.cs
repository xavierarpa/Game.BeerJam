using UnityEngine;
public sealed class AudioManager : MonoBehaviour
{
    private const float MAX_dB = 55f;
    public static AudioManager _;
    public AudioSource src_music;
    public AudioSource src_sound;
    private void Awake()
    {
        _ = this;
    }
    public void PlayMusic(AudioClip clip)
    {
        // if(_.src_music.clip == clip) return;
        _.src_music.clip = clip;
        _.src_music.Play();
    }
    public void PlaySound(AudioClip clip, float pitch = 1)
    {
        _.src_sound.pitch = pitch;
        _.src_sound.PlayOneShot(clip);
    }
}