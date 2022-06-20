using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public WinScreen ws;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player"){
            GameObject player = other.gameObject;
            player.GetComponent<PlayerAudioManager>().SafeZone(true);
            player.GetComponent<PlayerMovement>().DisplayCollectibles(true);
            if (this.gameObject.name == ("SafeZone Victo"))
                ws.Setup(1);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player"){
            GameObject player = other.gameObject;
            player.GetComponent<PlayerAudioManager>().SafeZone(false);
            player.GetComponent<PlayerMovement>().DisplayCollectibles(false);
        }
    }
}
