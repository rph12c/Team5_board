using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetMasterLvl(float masterLvl)
    {
        audioMixer.SetFloat("MasterVolume", masterLvl);
    }

    public void SetMusicLvl(float musicLvl)
    {
        audioMixer.SetFloat("MainMusicVolume", musicLvl);
    }

    public void SetSfxLvl(float sfxLvl)
    {
        audioMixer.SetFloat("SFX", sfxLvl);
    }
}
