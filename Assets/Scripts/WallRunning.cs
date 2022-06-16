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

    private int itWillRunSameWall = 1;


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
        if (pm.IsGround())
            itWillRunSameWall = 1;
    }

    private void FixedUpdate() {
        if(pm.IsWallRunning && pm.lastWallSide != pm.currentWallSide){
            WallRunningMovement();
        }

    }

    private void CheckForWall(){
        rightWall = Physics.Raycast(transform.position,orientation.right, out rightWallhit, wallCheckDistance, isWall);
        leftWall = Physics.Raycast(transform.position,-orientation.right, out leftWallhit, wallCheckDistance, isWall);
        if(leftWall)
            pm.currentWallSide = PlayerMovement.LastWallRun.leftW;
        else if(rightWall)
            pm.currentWallSide = PlayerMovement.LastWallRun.rightW;


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

            else if (Input.GetButtonDown("Jump")){
                itWillRunSameWall = 0;
                WallJump();
                StopWallRun();
            }
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


        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;
        pm.ResetYVelocity();
        rb.Move(wallForward * wallRunForce * Time.deltaTime);
        // rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        // push to wall force
        // if (!(leftWall && horizontalInput > 0) && !(rightWall && horizontalInput < 0)){
        //     rb.Move(-wallNormal * 10);
        // }


    }
    private void StopWallRun()
    {
        pm.IsWallRunning = false;
        pm.isGravity = true;
        pm.lastWallSide = pm.currentWallSide;
    }

    private void WallJump()
    {
        Vector3 wallNormal= rightWall ? rightWallhit.normal : leftWallhit.normal;
        Vector3 wallForward = Vector3.Cross(wallNormal,transform.up);
        

        if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
            wallForward = -wallForward;

        Vector3 wallJumpDir = wallNormal + wallForward;
        pm.SetXVelocity(wallJumpDir.x * 10);
        pm.SetZVelocity(wallJumpDir.z * 10);
        pm.SetYVelocity(Mathf.Sqrt(pm.jumpHeight * -2 * pm.gravity));
    }
}
