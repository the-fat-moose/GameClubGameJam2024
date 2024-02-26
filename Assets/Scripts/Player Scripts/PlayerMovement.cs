using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerCam;
    private Shooting shootScript; 
    
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float groundDrag;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask ground;
    private bool isGrounded;

    [Header("Slope Handling")]
    [SerializeField] float maxSlopeAngle;
    [SerializeField] private RaycastHit slopeHit;
    private bool exitingSlope = true;

    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool canJump;

    [Header("Crouching")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float crouchYScale;
    [SerializeField] private float startYScale;

    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode crouchKey = KeyCode.C;
    [SerializeField] KeyCode shootKey = KeyCode.Mouse0;

    [SerializeField] private Transform playerOrientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;
    private Rigidbody playerRB;
    public MovementState state;

    private bool canShoot = true;

    public enum MovementState
    {
        walking,
        sprinting,
        crouching,
        air
    }

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        shootScript = GetComponent<Shooting>();
        playerRB.freezeRotation = true;
        canJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        PlayerInput();
        CheckGround();
        StateHandler();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        ApplyDrag();
        SpeedControl();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        // Jump
        if (Input.GetKey(jumpKey) && canJump && isGrounded) { Jump(); }

        // Crouch
        if (Input.GetKeyDown(crouchKey)) { Crouch(); }

        // UnCrouch
        if (Input.GetKeyUp(crouchKey)) { UnCrouch(); }

        // Shoot
        if (Input.GetKeyDown(shootKey))
        {
            if (canShoot)
            {
                shootScript.Shoot(playerCam);
                StartCoroutine(CanPlayerShoot());
            }
        }
    }

    // time between the player being able to shoot -- Firerate
    private IEnumerator CanPlayerShoot()
    {
        canShoot = false;

        yield return new WaitForSeconds(0.2f);

        canShoot = true;
    }

    private void StateHandler()
    {
        // Crouching
        if (Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
            moveSpeed = crouchSpeed;
        }
        // Sprinting
        else if (isGrounded && Input.GetKey(sprintKey))
        {
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }
        // Walking
        else if (isGrounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }
        // Air
        else { state = MovementState.air; }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;

        if (OnSlope() && !exitingSlope) 
        { 
            playerRB.AddForce(GetSlopeMoveDir() * moveSpeed * speedMultiplier, ForceMode.Force);
            if (playerRB.velocity.y > 0) { playerRB.AddForce(Vector3.down * 80f, ForceMode.Force); }
        }
        else if (isGrounded) { playerRB.AddForce(moveDirection.normalized * moveSpeed * speedMultiplier, ForceMode.Force); }
        else if (!isGrounded) 
        { 
            playerRB.AddForce(new Vector3(moveDirection.x, -2.5f, moveDirection.z).normalized * moveSpeed * speedMultiplier * airMultiplier, ForceMode.Force);
        }

        playerRB.useGravity = !OnSlope();
    }

    private void CheckGround()
    {
        Vector3 groundCheckHalfExtents = new Vector3(1f, 0.5f, 1f);

        // check ground using box cast to allow for slight coyote time on jumps
        //isGrounded = Physics.BoxCast(transform.position, groundCheckHalfExtents, Vector3.down, Quaternion.identity, playerHeight * 0.5f + 0.2f, ground);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, ground);

        Debug.Log(isGrounded);
    }

    private void ApplyDrag()
    {
        // handle drag
        if (isGrounded) { playerRB.drag = groundDrag; }
        else { playerRB.drag = groundDrag; }
    }

    private void SpeedControl()
    {
        // limit speed on slopes
        if (OnSlope() && !exitingSlope) 
        { 
            if (playerRB.velocity.magnitude > moveSpeed) { playerRB.velocity = playerRB.velocity.normalized * moveSpeed; }
        }
        // limit speed on ground or in air
        else
        {
            Vector3 flatVelocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);

            // limit velocity
            if (flatVelocity.magnitude > moveSpeed)
            {
                Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
                playerRB.velocity = new Vector3(limitedVelocity.x, 0f, limitedVelocity.z);
            }
        }
    }

    private void Jump()
    {
        exitingSlope = true;
        canJump = false;

        // reset y velocity to 0
        playerRB.velocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);

        playerRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        // set jump to available
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void Crouch()
    {
        transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        playerRB.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    private void UnCrouch()
    {
        transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
    }

    private void ResetJump()
    {
        canJump = true;
        exitingSlope = false;
    }

    private bool OnSlope()
    {
        exitingSlope = false;
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, ((playerHeight * 0.5f) + 0.3f)))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDir()
    {
        return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
    }
}
