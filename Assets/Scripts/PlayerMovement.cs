using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    
    const float WALKING_SPEED = 6f;

    [SerializeField]
    private ScoreManager scoreManager;

    [SerializeField]
    private CharacterController controller;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isRunning = false;
    
    [SerializeField]
    public bool isGravity = true;
    private bool isGrounded;

    public bool IsWallRunning = false;
    public bool IsWallClimbing = false;
    public bool IsClimbing = false;
    public bool IsHanging = false;

    private Transform obstacle;

    [SerializeField]
    private float currentSpeed = 0f;

    [SerializeField] 
    private float runningSpeedMultiplier;

    // Start is called before the first frame update
    public float gravity = -19.81f;

    [SerializeField]
    Vector3 velocity;

    [SerializeField]
    public float jumpHeight = 3f;

    [SerializeField]
    private GameObject hitboxClimb;

    public enum LastWallRun{ leftW, rightW, none}
    public LastWallRun lastWallSide, currentWallSide;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Check on ground & reset gravity velocity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded)
        if (isGrounded && velocity.y <0f) 
        {
            lastWallSide = LastWallRun.none;
            // currentWallSide = LastWallRun.none;
            ResetVelocity();
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded){
            isRunning = true;
        } else if (Input.GetKeyUp(KeyCode.LeftShift)){
            isRunning = false;
        }

        currentSpeed = isRunning ? WALKING_SPEED * runningSpeedMultiplier : WALKING_SPEED;
        


        //gravity
        if(isGravity)
            velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity * Time.deltaTime);

        //deplacement
        if(IsWallRunning){

        }
        else if(IsWallClimbing || IsHanging)
        {

        }
        else if(IsClimbing)
        {
            if (IsGround())
            {
                IsClimbing = false;
            }
        }
            
        else { //Todo debugging
            controller.Move(move * currentSpeed * Time.deltaTime);

        }
        //Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }   

        

        
    }
 

    public bool GetPlayerRunning() {return isRunning;}

    public CharacterController GetCharacterController() {return controller;}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Collectible"){
            scoreManager.AddCollectible(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }

    public bool IsGround()
    {
        return isGrounded;
    }


    public void DisplayCollectibles(bool display){
        scoreManager.ShowCollectibles(display);
    }

    public void ResetYVelocity()
    {
        velocity.y = 0f;
    }
        public void SetYVelocity(float yVelocity)
    {
        velocity.y = yVelocity;
    }       
     public void SetXVelocity(float xVelocity)
    {
        velocity.x = xVelocity;
    }        
    public void SetZVelocity(float zVelocity)
    {
        velocity.z = zVelocity;
    }
    public void SetVelocity(Vector3 velocit){
        velocity = velocit;
    }
    public void ResetVelocity()
    {
        velocity = Vector3.zero;
    }
}
