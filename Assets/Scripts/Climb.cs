using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{

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

    [Header("Reference")]
    public Transform orientation;
    private PlayerMovement pm;
    private CharacterController rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<CharacterController>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // CheckForWall();
        // StateMachine();
    }
}
