using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask isWall;
    public LayerMask isGround;
    public float wallRunForce;
    public float maxWallRunTime;
    private float WallRunTimer;

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Detections")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool leftWall;
    private bool rightWall;

    [Header("Reference")]
    public Transform orientation;
    private PlayerMovement pm;
    private CharacterController rb;


    void Start()
    {
        rb = GetComponent<CharacterController>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate() {
        if(pm.IsWallRunning){
            Debug.Log("1");
            WallRunningMovement();
            }
    }

    private void CheckForWall(){
        rightWall = Physics.Raycast(transform.position,orientation.right, out rightWallhit, wallCheckDistance, isWall);
        leftWall = Physics.Raycast(transform.position,-orientation.right, out rightWallhit, wallCheckDistance, isWall);
    }

    private void StateMachine(){
        //Gettings inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // State 1 - Wallrunning
        if ((leftWall || rightWall) && verticalInput > 0 && !pm.IsGround())
        {
            if(!pm.IsWallRunning)
                StartWallRun();
        }
        else {
            if(pm.IsWallRunning)
                StopWallRun();
        }

    }
    private void StartWallRun(){
        pm.IsWallRunning = true;
    }
    private void WallRunningMovement(){
        pm.isGravity = false;
        Vector3 wallNormal= rightWall ? rightWallhit.normal : leftWallhit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal,transform.up);

        rb.Move(wallForward * wallRunForce * Time.deltaTime);
        // rb.Move(wallForward * wallRunForce, ForceMode.Force);
    }
    private void StopWallRun()
    {
        pm.IsWallRunning = false;
        pm.isGravity = true;
    }
}
