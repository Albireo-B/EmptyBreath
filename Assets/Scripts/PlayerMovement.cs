using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded;


    [SerializeField]
    private float speed = 12f;
    // Start is called before the first frame update
    public float gravity = -9.81f;

    [SerializeField]
    Vector3 velocity;

    [SerializeField]
    public float jumpHeight = 3f;

    [SerializeField]
    private GameObject hitboxClimb;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check on ground & reset gravity velocity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y <0f) 
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
    
        Vector3 move;
        //Empeche de bouger sur x en saut
        if(!isGrounded) move = transform.right * (x *0.1f) + transform.forward * z;

        //mouvement normal au sol
        else move = transform.right * x + transform.forward * z; 

        
        //gravity
        velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity * Time.deltaTime);

        //deplacement

        controller.Move(move * speed * Time.deltaTime);

        //Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }   

    }

    void OnCollisionEnter(Collision collision) {
        Debug.Log(collision.collider.gameObject.name);
        Debug.Log("in");
    }
    private void OnTriggerEnter(Collider collision) {
        Debug.Log(collision.GetComponent<Collider>().gameObject.name);
        Debug.Log("in");
    }

}
