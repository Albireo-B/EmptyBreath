using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KillPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    private Vector3 positionLastSecond;
    private Vector3 position;

    [SerializeField]
    private float timeBeforePursuitSounds = 1f;

    private float timeLeftBeforePursuit;


    
    // Start is called before the first frame update
    void Start()
    {
        positionLastSecond = player.transform.position;
        timeLeftBeforePursuit = timeBeforePursuitSounds;
    }

    // Update is called once per frame
    void Update()
    {
        
        //Debug.Log(Vector3.Distance(player.transform.position, positionLastSecond));
        if (Vector3.Distance(player.transform.position, positionLastSecond) < 0.15f) 
        {
            if (timeLeftBeforePursuit > 0.0f){
                timeLeftBeforePursuit -= Time.deltaTime;
            } else {
                player.GetComponent<PlayerAudioManager>().SetImmobile(true);
                timeLeftBeforePursuit = timeBeforePursuitSounds;
            }
            
        } else {
            //reset pursuit time
            timeLeftBeforePursuit = timeBeforePursuitSounds;
            player.GetComponent<PlayerAudioManager>().SetImmobile(false);
        }
        
        positionLastSecond = player.transform.position;
        
    }
}
