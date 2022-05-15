using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.1f;
    public LayerMask groundMask;
    private bool isGrounded;

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private float speed = 12f;
    // Start is called before the first frame update
    public float gravity = -9.81f;

    [SerializeField]
    Vector3 velocity;

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
            velocity.y = -4f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
    
        Vector3 move = transform.right * x + transform.forward * z; 
    
        //gravity
        velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity * Time.deltaTime);

        //deplacement
        controller.Move(move * speed * Time.deltaTime);
    }

}
