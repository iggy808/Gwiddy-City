using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
	public PlayerEngaged PlayerEngaged;
	public ScenarioController ScenarioController;
    public float moveSpeed;
    public float playerHeight;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float dashForce;
    public float dashCooldown;
    public float airMultiplier;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.LeftShift;
    public LayerMask groundLayer;
    public Transform orientation;
    public bool dbJump;
    public bool dsh;
    public bool engaged;
    private bool grounded;
    private bool jumpReady = true;
    private bool dashReady = true;
    private bool dashCD = true;
    private int jumps;
    private float horizontalInput;
    private float verticalInput;
    private Vector3 moveDirection;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
		engaged = PlayerEngaged.battleEngaged;
    }

    private void Update()
    {
		engaged = PlayerEngaged.battleEngaged;
        if (!engaged && !ScenarioController.IsScenarioActive)
        {
            grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.8f, groundLayer);
            PlayerInput();
            SpeedControl();
            if (grounded)
            {
                rb.drag = groundDrag;
                jumps = 0;
                if (dashCD)
                    dashReady = true;
            }
            else
                rb.drag = 0;
        }
    }

    
    private void FixedUpdate()
    {
        if (!engaged)
        MovePlayer();
    }
    

    private void PlayerInput()
    {

            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");


        if (Input.GetKey(jumpKey) && jumpReady)
        {
            if (jumps != 1)
            {
                jumpReady = false;
                jumps += 1;
                Jump();
                Invoke("ResetJump", jumpCooldown);
            }
        }
        if(Input.GetKey(dashKey) && dashReady && dsh)
        {
            dashReady = false;
            dashCD = false;
            Dash();
            Invoke("ResetDash", dashCooldown);
        }
    }

    
    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else if (!grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }
    

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        if (grounded || dbJump)
            jumpReady = true;
        else
            Invoke("ResetJump", jumpCooldown);
    }

    private void Dash()
    {
        rb.velocity = new Vector3(0, 0, 0);
        rb.AddRelativeForce(orientation.forward * dashForce, ForceMode.Impulse);
    }

    private void ResetDash()
    {
        dashCD = true;
    }
}
