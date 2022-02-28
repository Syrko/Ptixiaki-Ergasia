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

    public void PlaySFX(SFXToPlay sfx)
    {
        if (SFXAudioSource.isPlaying)
        {
            SFXAudioSource.Stop();
        }
        switch (sfx)
        {
            case SFXToPlay.Battle:
                SFXAudioSource.PlayOneShot(battle);
                break;
            case SFXToPlay.Whoosh:
                SFXAudioSource.PlayOneShot(whoosh);
                break;
            case SFXToPlay.March:
                SFXAudioSource.PlayOneShot(march);
                break;
            case SFXToPlay.Victory:
                SFXAudioSource.PlayOneShot(victory);
                break;
            case SFXToPlay.Defeat:
                SFXAudioSource.PlayOneShot(defeat);
                break;
            case SFXToPlay.Draw:
                SFXAudioSource.PlayOneShot(draw);
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
