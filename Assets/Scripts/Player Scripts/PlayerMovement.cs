using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float groundDrag;

    [Header("Ground Check")]
    [SerializeField] private float playerHeight;
    [SerializeField] private LayerMask ground;
    private bool isGrounded;

    [Header("Jumping")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    [SerializeField] private float airMultiplier;
    private bool canJump;

    [Header("Keybinds")]
    [SerializeField] private KeyCode jumpKey;

    [SerializeField] private Transform playerOrientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody playerRB;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerRB.freezeRotation = true;
        canJump = true;
    }

    private void Update()
    {
        PlayerInput();
        ApplyDrag();
        SpeedControl();
        CheckGround();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && canJump && isGrounded)
        {
            Debug.Log("Smile");
            Jump();
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = playerOrientation.forward * verticalInput + playerOrientation.right * horizontalInput;

        if (isGrounded) { playerRB.AddForce(moveDirection.normalized * moveSpeed * speedMultiplier, ForceMode.Force); }
        else if (!isGrounded) { playerRB.AddForce(moveDirection.normalized * moveSpeed * speedMultiplier * airMultiplier, ForceMode.Force); }
    }

    private void CheckGround()
    {
        Vector3 groundCheckHalfExtents = new Vector3(1f, 0.5f, 1f);

        // check ground using box cast to allow for slight coyote time on jumps
        isGrounded = Physics.BoxCast(transform.position, groundCheckHalfExtents, Vector3.down, Quaternion.identity, playerHeight * 0.5f + 0.2f, ground);

        Debug.Log(isGrounded);
    }

    private void ApplyDrag()
    { 
        // handle drag
        if (isGrounded) { playerRB.drag = groundDrag; }
        else { playerRB.drag = 0; }
    }

    private void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);

        // limit velocity
        if (flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            playerRB.velocity = new Vector3(limitedVelocity.x, 0f, limitedVelocity.z);
        }
    }

    private void Jump()
    {
        canJump = false;

        // reset y velocity to 0
        playerRB.velocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);

        playerRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        // set jump to available
        Invoke(nameof(ResetJump), jumpCooldown);
    }

    private void ResetJump()
    {
        canJump = true;
    }
}
