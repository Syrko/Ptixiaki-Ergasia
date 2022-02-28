using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    [SerializeField]
    AudioClip battle;

    [SerializeField]
    AudioClip whoosh;

    [SerializeField]
    AudioClip march;

    [SerializeField]
    AudioClip victory;

    [SerializeField]
    AudioClip defeat;

    [SerializeField]
    AudioClip draw;

    [SerializeField]
    AudioSource SFXAudioSource;

    [SerializeField]
    float SFXScale = 0.4f;

    public void PlaySFX(SFXToPlay sfx)
    {
        if (SFXAudioSource.isPlaying)
        {
            SFXAudioSource.Stop();
        }
        switch (sfx)
        {
            case SFXToPlay.Battle:
                SFXAudioSource.PlayOneShot(battle, SFXScale);
                break;
            case SFXToPlay.Whoosh:
                SFXAudioSource.PlayOneShot(whoosh, SFXScale);
                break;
            case SFXToPlay.March:
                SFXAudioSource.PlayOneShot(march, SFXScale);
                break;
            case SFXToPlay.Victory:
                SFXAudioSource.PlayOneShot(victory, SFXScale);
                break;
            case SFXToPlay.Defeat:
                SFXAudioSource.PlayOneShot(defeat, SFXScale);
                break;
            case SFXToPlay.Draw:
                SFXAudioSource.PlayOneShot(draw, SFXScale);
                break;
        }
    }
}

public enum SFXToPlay
{
    Battle,
    Whoosh,
    March,
    Victory,
    Defeat,
    Draw
}
