using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audio1;
    // Start is called before the first frame update
  public void SetupVolume(float vol)
    {
        audio1.SetFloat("Volume", vol);
    }
}
