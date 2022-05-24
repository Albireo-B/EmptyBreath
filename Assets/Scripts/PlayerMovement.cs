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
    
    [SerializeField]
    private bool isGravity = true;
    private bool isGrounded;

    //Climb
    [SerializeField]
    private bool canClimbing = false;
    [SerializeField]
    private bool isClimbing = false;
    [SerializeField]
    private float cdnClimb = 0.0f;
    private Transform obstacle;

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
        if(isGravity)
            velocity.y += gravity * Time.deltaTime; 
        controller.Move(velocity * Time.deltaTime);

        //deplacement
        if(!isClimbing)
            controller.Move(move * speed * Time.deltaTime);
        else { //Todo debugging
            if(Input.GetMouseButtonDown(1))
            {
            Vector3 pos = new Vector3(obstacle.transform.position.x + (obstacle.transform.position.x - transform.position.x), obstacle.transform.position.y + 3 , obstacle.transform.position.z + (obstacle.transform.position.z- transform.position.z));
            Debug.Log(pos);
            this.transform.position = pos;
            this.isClimbing = false;
            this.isGravity = true;
            velocity.y = -2f;
            }

        }
        //Jump
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }   

        //Climb 
        if(Input.GetMouseButtonDown(0) && canClimbing)
        {
            isGravity = false;
            velocity.y = 0f;
            isClimbing =true;
            Debug.Log(obstacle.transform.position);
            
        }

        if(cdnClimb > 0f)
        {
            cdnClimb -= Time.deltaTime;
        }
        if(cdnClimb <0f)
        {
            canClimbing = false;

        }

        
    }

    // void OnCollisionEnter(Collision collision) {
    //     Debug.Log(collision.collider.gameObject.name);
    //     Debug.Log("in");
    // }
    private void OnTriggerStay(Collider collision) {
        obstacle = collision.GetComponent<Collider>().gameObject.transform;
        CanClimb(true);

    }

    private void CanClimb(bool can)
    {
        this.canClimbing = true;
        cdnClimb = 0.5f;
    }

}
