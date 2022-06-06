using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerAudioManager : MonoBehaviour
{


    enum VolumeMode{Increase,Decrease};

    [SerializeField] private float volumeStrengthModifier = 0.1f; // from 0 to 1
    
    [SerializeField] private float volumeSpeedModifier = 1f; // time between volume changes
    private bool audioPlaying;

    private Coroutine volumeCoroutine;
    private AudioSource audioSource;
    private bool playerImmobile;

    // Start is called before the first frame update
    void Start()
    {
        audioPlaying = false;
        audioSource = transform.GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (playerImmobile && audioPlaying){
            audioPlaying = true;
            StopCoroutine(volumeCoroutine);
            volumeCoroutine = StartCoroutine(VolumeHandler(VolumeMode.Increase));
        }*/
         if (playerImmobile && !audioPlaying){
            if (volumeCoroutine!=null)
                StopCoroutine(volumeCoroutine);
            PlayAliensIncoming();
        } else if (!playerImmobile && audioPlaying){
            audioPlaying = false;
            StopCoroutine(volumeCoroutine);
            volumeCoroutine = StartCoroutine(VolumeHandler(VolumeMode.Decrease));
        } 
        //Tant que joueur immobile : play et monter son?
        // sinon  : play et baisser son -> quand volume 0 ou 100 stop.
    }

    void PlayAliensIncoming(){
        audioPlaying = true;
        //We play randomly the left or right speaker to panic the player and improve immersion
        if(Random.value<0.5f)
            audioSource.panStereo = -0.75f;
        else
            audioSource.panStereo = 0.75f;
        audioSource.volume = 0.1f;
        audioSource.Play();
        volumeCoroutine = StartCoroutine(VolumeHandler(VolumeMode.Increase));
    }

    IEnumerator VolumeHandler(VolumeMode volumeMode){
        while (audioSource.volume != 1 && audioSource.volume != 0){
            
            if (volumeMode == VolumeMode.Increase){
                audioSource.volume +=  volumeStrengthModifier;
            }else if (volumeMode == VolumeMode.Decrease){
                audioSource.volume -=  volumeStrengthModifier;
            }
            yield return new WaitForSeconds(volumeSpeedModifier);
        }
        if (audioSource.volume == 1){
            StopAudio();
            PlayerDeath();
        } else if (audioSource.volume == 0) {
            StopAudio();
        }
        yield return null;
    }

    void StopAudio(){
        audioPlaying = false;
        audioSource.Stop();
        StopAllCoroutines();
    }

    void PlayerDeath(){
        Debug.Log("The THINGS got you...");
        SceneManager.LoadScene("DemoScene");
    }

    public void SetImmobile(bool immobile){
        playerImmobile = immobile;
    }
}
