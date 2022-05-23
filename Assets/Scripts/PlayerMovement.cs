using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    
    const float WALKING_SPEED = 12f;

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isGrounded = true;
    private bool isRunning = false;


    [SerializeField]
    private float currentSpeed = 0f;

    [SerializeField] 
    private float runningSpeedMultiplier;

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

        //Set the current speed depending on character running or not        
        if (Input.GetKeyDown(KeyCode.LeftShift)){
            isRunning = true;
        } else if (Input.GetKeyUp(KeyCode.LeftShift)){
            isRunning = false;
        }

        currentSpeed = isRunning ? WALKING_SPEED * runningSpeedMultiplier : WALKING_SPEED;
        


        //gravity
        velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity * Time.deltaTime);

        //deplacement

        controller.Move(move * currentSpeed * Time.deltaTime);

        //Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        //Debug.Log(isGrounded);
    }
    public void OnCollisionEnter(Collision collision)
    {     
        IInteractableObject io = collision.gameObject.GetComponent<IInteractableObject>();
        if (io == null)
            return;
        io.OnPlayerInteraction();


    }

    public bool GetPlayerRunning() {return isRunning;}

    public CharacterController GetCharacterController() {return controller;}

}
