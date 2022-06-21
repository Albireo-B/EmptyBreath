using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public GameOverScreen ws;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Transform play = other.GetComponent<Collider>().gameObject.transform;
        if (play.gameObject.tag ==  "Player"){
            play.GetComponent<PlayerAudioManager>().SafeZone(true);
            ws.Setup();
            
        }
    }
}
