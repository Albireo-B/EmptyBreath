using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{

    public LayerMask isWall;
    public LayerMask isGround;
    public float wallClimbForce;
    private float wallClimbForceSave;
    public float maxWallClimbTime;
    private float WallClimbTimer;
    private Vector3 hangPosition;

    public float hangHight;
    public float climbHight;

    public int itWillClimbSameWall = 1;

    private bool hang = false;

    [Header("Input")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Detections")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit frontWallhit;
    private bool frontWall;


    [Header("Reference")]

    public GameObject player;
    public Transform orientation;
    private PlayerMovement pm;
    private CharacterController rb;
    private Transform obstacle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<CharacterController>();
        pm = GetComponent<PlayerMovement>();
        wallClimbForceSave = wallClimbForce;

    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        StateMachine();
        if (pm.IsGround())
            itWillClimbSameWall = 1;
    }

    private void FixedUpdate() {
        if(pm.IsWallClimbing && pm.lastWallSide != pm.currentWallSide){
            WallClimbingMovement();
        }

    }

    private void CheckForWall(){
        frontWall = Physics.Raycast(transform.position,orientation.forward, out frontWallhit, wallCheckDistance);

    }

    private void StateMachine(){
        //Gettings inputs
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // State 1 - WallClimbing
        if (hang)
        {
            
            if (Input.GetMouseButtonDown(0))
                StartClimb();
            else if(player.transform.position.y > hangPosition.y)
            {
                pm.isGravity = true;

                if (player.transform.position.y > obstacle.transform.position.y + 0.4f)
                {
                    if(verticalInput>0)
                    {
                        wallClimbForce = 0f;
                        pm.isGravity = false;
                    }
                    else
                    {
                        pm.isGravity = true;
                    }
                }
                else
                    wallClimbForce = wallClimbForceSave;
               
           
            }
            else{
                pm.isGravity = false;
                pm.ResetVelocity();
            }
        }
        if ((frontWall) && verticalInput > 0 && !pm.IsClimbing ) //&& !pm.IsGround()
        {

            if(!pm.IsWallClimbing)
                StartWallClimb();
        }
        else {
            if(pm.IsWallClimbing)
                StopWallClimb();
        }

    }
    private void StartWallClimb(){
        pm.IsWallClimbing = true;
    }
    private void WallClimbingMovement(){
        pm.isGravity = false;
        // Vector3 wallNormal= frontWall.normal;
        Vector3 wallForward =frontWallhit.normal;

        // if ((orientation.forward - wallForward).magnitude > (orientation.forward - -wallForward).magnitude)
        //     wallForward = -wallForward;
        pm.ResetYVelocity();
        rb.Move(transform.up * wallClimbForce * Time.deltaTime);
        // rb.AddForce(wallForward * wallRunForce, ForceMode.Force);

        // push to wall force
        // if (!(leftWall && horizontalInput > 0) && !(rightWall && horizontalInput < 0)){
        //     rb.Move(-wallNormal * 10);
        // }


    }
    private void StopWallClimb()
    {
        pm.IsWallClimbing = false;
        pm.isGravity = true;
        // pm.lastWallSide = pm.currentWallSide;
    }

    private void OnTriggerEnter(Collider collision) {
        obstacle = collision.GetComponent<Collider>().gameObject.transform;
        if(itWillClimbSameWall > 0){
            IsHang(true);
            itWillClimbSameWall = 0;    
        }
        else
        {

        }


    }

    private void IsHang(bool can)
    {
        hang = can;
        pm.IsHanging = can;
        SetHangPosition();
    }

    private void StartClimb()
    {
        wallClimbForce = wallClimbForceSave;
        pm.IsHanging = false;
        pm.isGravity = true;
        pm.IsClimbing= true;
        hang = false;
        ClimbMovement();
    }
    private void ClimbMovement()
    {
        Vector3 wallForward =frontWallhit.normal;
        pm.SetXVelocity(-wallForward.x * climbHight);
        pm.SetZVelocity(-wallForward.z * climbHight);
        pm.SetYVelocity(Mathf.Sqrt(pm.jumpHeight * -2 * pm.gravity));
    }
    private void StopClimb()
    {

    }

    private void SetHangPosition()
    {
        this.hangPosition = new Vector3(player.transform.position.x ,obstacle.transform.position.y - hangHight , rb.transform.position.z);
    }



}