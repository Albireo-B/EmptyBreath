using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IInteractableObjectPattoun : MonoBehaviour, IInteractableObject
{
    // Start is called before the first frame update
    public ScoreManager ScoreMana;
    void Start()
    {
        
    }

    // Update is called once per frame

    public void OnPlayerInteraction()
    {
        ScoreMana.MorePoint();
        Destroy(this.gameObject);
    }
        
}
