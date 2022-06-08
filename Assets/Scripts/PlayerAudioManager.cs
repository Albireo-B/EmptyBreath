using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerAudioManager : MonoBehaviour
{


    enum VolumeMode{Increase,Decrease};

    [SerializeField] private float volumeStrengthModifier = 0.1f; // from 0 to 1
    
    [SerializeField] private float volumeSpeedModifier = 1f; // time between volume changes


    private Coroutine volumeCoroutine;
    private AudioSource audioSource;
    private bool playerImmobile;
    private bool audioReset;
    private bool audioIncreasing;
    private bool audioDecreasing;

    // Start is called before the first frame update
    void Start()
    {
        audioReset = false;
        audioIncreasing = false;
        audioDecreasing = false;
        audioSource = transform.GetComponentInChildren<AudioSource>();
        audioSource.volume = 0.01f;

    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying && (audioDecreasing || audioIncreasing))
            audioSource.Play();
        else if (playerImmobile && !audioIncreasing && !audioDecreasing){
            if (volumeCoroutine!=null)
                StopCoroutine(volumeCoroutine);
            PlayAliensIncoming();
        } else if (!playerImmobile && audioIncreasing){
            audioReset = false;
            audioIncreasing = false;
            audioDecreasing = true;
            StopCoroutine(volumeCoroutine);
            volumeCoroutine = StartCoroutine(VolumeHandler(VolumeMode.Decrease));
        } else if (playerImmobile && audioDecreasing){
            audioIncreasing = true;
            audioDecreasing = false;
            audioReset = true;
            StopCoroutine(volumeCoroutine);
            volumeCoroutine = StartCoroutine(VolumeHandler(VolumeMode.Increase));
        }
    }

    void PlayAliensIncoming(){
        audioIncreasing = true;
        //We play randomly the left or right speaker to panic the player and improve immersion
        if(Random.value<0.5f)
            audioSource.panStereo = -0.75f;
        else
            audioSource.panStereo = 0.75f;
        audioSource.volume = 0.01f;
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
        audioDecreasing = false;
        audioIncreasing = false;
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
