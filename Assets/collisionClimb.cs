using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionClimb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
        private void OnTriggerEnter(Collider collision) {
        Debug.Log(collision.GetComponent<Collider>().gameObject.name);
        Debug.Log("in");
    }
}
