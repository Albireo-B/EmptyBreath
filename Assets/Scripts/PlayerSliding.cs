using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSliding : MonoBehaviour
{

    [Header ("Reference")]
    private PlayerMovement playerMovement;


    [Header ("Sliding")]
    [SerializeField] private float maxSlideTime;
    [SerializeField] private float slideForce;
    private float slideTimer;
    [SerializeField] private float slideYScale;
    private float startYScale;
    [SerializeField] private float maxSlopeAngle = 80f;  


    [Header ("Input")]
    private float horizontalInput;
    private float verticalInput;
    private bool isSliding = false;


    private RaycastHit slopeHit;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        startYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.C) && playerMovement.GetPlayerRunning())
            StartSlide();
        

        if (Input.GetKeyUp(KeyCode.C) && isSliding)
            StopSlide();
    }

    void FixedUpdate() {
        if (isSliding)
            SlidingMovement();
    }

    private void SlidingMovement()
    {
        Vector3 inputDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        //Sliding normal
        if (!OnSlope()) { //|| rb.velocity.y > -0.1f
            playerMovement.GetCharacterController().Move(inputDirection * slideForce * Time.deltaTime);

            slideTimer -= Time.deltaTime;
        } 
        //Sliding down a slope
        else {
            playerMovement.GetCharacterController().Move(GetSlopeMoveDirection(inputDirection) * slideForce * Time.deltaTime);
        }


        if (slideTimer <= 0)
            StopSlide();
    }

    private void StopSlide()
    {
        isSliding = false;

        transform.localScale = new Vector3(-transform.localScale.x,startYScale,transform.localScale.z);


    }

    private void StartSlide(){
        isSliding = true;

        transform.localScale = new Vector3(transform.localScale.x, slideYScale, transform.localScale.z);

        slideTimer = maxSlideTime;
    }

    private bool OnSlope(){
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, Mathf.Infinity)){
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection(Vector3 direction){
        return Vector3.ProjectOnPlane(direction,slopeHit.normal).normalized;
    }
}
