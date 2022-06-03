using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class KillPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    private Vector3 positionLastSecond;
    private Vector3 position;
    [SerializeField]
    private float timeBeforeDeath = 1f;
    // Start is called before the first frame update
    void Start()
    {
        positionLastSecond = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBeforeDeath > 0.0f){
            timeBeforeDeath -= Time.deltaTime;

        }
        else{
            timeBeforeDeath = 1f;
            if (Vector3.Distance(player.position, positionLastSecond) < 0.008f)
            {
               
                Debug.Log("Death");
                SceneManager.LoadScene("FPSMap");
            }
            }
        positionLastSecond = player.position;
    }
}
