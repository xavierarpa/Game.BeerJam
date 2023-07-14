using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

#pragma warning disable 0649
public sealed class AudioSystem : MonoBehaviour
{
    public const float DEFAULT_SOUND_PERCENT = .7f;
    public const float DEFAULT_MUSIC_PERCENT = .7f;
    [Tooltip("Key usado en el mixer de Music")]
    private const string MUSIC_KEY = "MusicVolume";
    [Tooltip("Key usado en el mixer de Sound")]
    private const string SOUND_KEY = "SoundVolume";
    private const float TIMER_FADE = 5;
    private const float MAX_dB = 55f;//dato curioso: Según la OMS, el nivel de ruido que el oído humano puede tolerar sin alterar su salud es de 55 decibeles. Y dependiendo del tiempo de exposición, ruidos mayores a los 60 decibeles pueden provocarnos malestares físicos.
    private static AudioSystem _;
    private Vector2 dBValues;

    [Header("AudioSystem")]
    public AudioMixer mixer;
    [SerializeField] private AudioSource src_sound;
    [SerializeField] private AudioSource src_generalSound;

    private void Awake()
    {
        this.Singleton(ref _, false);
    }
    private float Normalize(float value) => (value.PercentOf(MAX_dB) / 100) + 1;
    private void SetAdjustedB(ref float dB, float percent, string key)
    {
        mixer.GetFloat(key, out dB);
        dB = (-1 + percent).QtyOf(MAX_dB) * 100;
        mixer.SetFloat(key, dB);
    }
    private IEnumerator FadePlay(float timer, bool fadeIn = true, AudioClip clip = default)
    {
        int volumeToReach = fadeIn.ToInt();
        float lastVolume = _.src_sound.volume;
        float val = Time.deltaTime / timer;
        float magnitude = lastVolume.UnitInTime(volumeToReach);

        if (!fadeIn) magnitude--;

        while (!_.src_sound.volume.Equals(volumeToReach))
        {
            _.src_sound.volume = (_.src_sound.volume + magnitude).Min(0).Max(1);
            yield return new WaitForSeconds(val);
        }
        if (clip)
        {
            src_sound.clip = clip;
            src_sound.Play();
            StartCoroutine(FadePlay(timer));
        }
    }
    public static void SetMusic(float percent) => _.SetAdjustedB(ref _.dBValues.x, percent, MUSIC_KEY);
    public static void SetSound(float percent) => _.SetAdjustedB(ref _.dBValues.y, percent, SOUND_KEY);
    public static void SavedBValues()
    {
        DataSystem.data.saved.musicPercent = _.Normalize(_.dBValues.x);
        DataSystem.data.saved.soundPercent = _.Normalize(_.dBValues.y);
        DataSystem.Save();
    }
    public static void PlayMusic(AudioClip clip)
    {
        if (_.src_sound.clip && clip.name.Equals(_.src_sound.clip.name))
        {
#if UNITY_EDITOR
            "Se esta intentando una cancion que ya esta puesta".Print("yellow");
#endif
            return;//🛡
        }

        _.StartCoroutine(_.FadePlay(TIMER_FADE, false, clip));
    }
    public static void PlaySound(GeneralSounds g, float pitch = 1, float pitchModify = .2f) => PlaySound(g.ToInt(), pitch, pitchModify);
    public static void PlaySound(int index, float pitch = 1, float pitchModify = .2f) => PlaySound(Service.Local.Get.audio.sounds[index], pitch, pitchModify);
    public static void PlaySound(AudioClip clip, float pitch = 1, float pitchModify = .2f)
    {
        _.src_generalSound.pitch = pitch;
        _.src_generalSound.PlayOneShot(clip);
    }
    public static AudioClip GetSound(GeneralSounds g) => Service.Local.Get.audio.sounds[g.ToInt()];
}
