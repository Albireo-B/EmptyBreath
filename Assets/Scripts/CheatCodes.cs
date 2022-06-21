using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour
{

    private bool lightsDisabled;
    private bool soundDisabled;

    // Start is called before the first frame update
    void Start()
    {
        lightsDisabled  = false;
        soundDisabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)){
            soundDisabled  = !soundDisabled;
            GetComponent<KillPlayer>().enabled = !soundDisabled;
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAudioManager>().StopAudio();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAudioManager>().SetImmobile(false);
        } else if (Input.GetKeyDown(KeyCode.N)){
            lightsDisabled = !lightsDisabled;
            RenderSettings.fog =  !lightsDisabled;
            RenderSettings.ambientIntensity = lightsDisabled ? 8 : 1;
        }
    }
}
