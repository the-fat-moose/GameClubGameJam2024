using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public bool CanMove { get; set; } = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey);
    private bool ShouldJump => (Input.GetKeyDown(jumpKey) && characterController.isGrounded) || (Input.GetKeyDown(jumpKey) && canDoubleJump && remainingJumps > 0); // replace isGrounded with a different check for double jumping if double jumping is enabled
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && characterController.isGrounded;
    private bool ShouldShoot => canShoot && Input.GetKeyDown(shootKey);
    private bool ShouldDash => Input.GetKeyDown(dashKey) && canDash && isDashAbility;

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.C;
    [SerializeField] private KeyCode shootKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode dashKey = KeyCode.E;
    [SerializeField] private KeyCode creatureCageHealKey = KeyCode.F;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 6.0f;
    [SerializeField] private float sprintSpeed = 12.0f;
    [SerializeField] private float crouchSpeed = 3.0f;
    [SerializeField] private float dashSpeed = 2000.0f;
    public bool canDash { get; set; } = false;
    public bool isDashAbility { get; set; } = false;
    public float maxDashCooldown { get; private set; } = 12.5f;
    public float dashCooldown { get; private set; } = 12.5f;


    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] private float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] private float lowerLookLimit = 80.0f;

    [Header("Jumping Parameters")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 20.0f;
    public bool canDoubleJump { get; set; } = false;
    public int remainingJumps { get; private set; } = 1;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 0.5f;
    [SerializeField] private float standingHeight = 2.0f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenterPoint = new Vector3(0, 0.5f, 0);
    [SerializeField] private Vector3 standingCenterPoint = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;

    [Header("Shooting Parameters")]
    private bool canShoot = true;

    [Header("Audio Parameters")]
    [SerializeField] private AudioClip walkingSoundSnow;
    [SerializeField] private AudioClip walkingSoundDirt;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip dashSound;
    [SerializeField] private AudioSource playerAudioSourceWalk;
    [SerializeField] private AudioSource playerAudioSourceOneShots;
    
    public GameObject creatureCage { get; private set; } = null;
    public int cageMaterialPickups { get; set; } = 0;
    private float healAmount = 10f;

    private Camera playerCamera;
    private Camera gunCamera;
    public CharacterController characterController { get; private set; }
    private Shooting shootScript;
    private PlayerUIManager playerUIManager;
    public OptionsManager optionsManager { get; private set; }

    private Vector3 moveDirection;
    private Vector2 currentInput;

    Scene currentScene;

    private float rotationX = 0.0f; // used for look view clamping

    private void Awake()
    {
        playerCamera = gameObject.transform.GetChild(1).GetComponent<Camera>();
        gunCamera = gameObject.transform.GetChild(2).GetComponent<Camera>();
        characterController = GetComponent<CharacterController>();
        shootScript = GetComponent<Shooting>();
        playerUIManager = GetComponentInChildren<PlayerUIManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        optionsManager = GameObject.FindFirstObjectByType<OptionsManager>();
        if (playerAudioSourceOneShots != null) { playerAudioSourceOneShots.loop = false; }
        if (playerAudioSourceWalk != null) { playerAudioSourceWalk.loop = true; }

        currentScene = SceneManager.GetActiveScene();

        if (playerAudioSourceWalk != null)
        {
            if (currentScene.name == "Forest-1" || currentScene.name == "Sandy-1") { playerAudioSourceWalk.clip = walkingSoundDirt; }
            else if (currentScene.name == "Rocky-1") { playerAudioSourceWalk.clip = walkingSoundSnow; }
        }
    }

    public void SetCreatureCage(GameObject _creatureCage)
    {
        creatureCage = _creatureCage;
    }

    private void Update()
    {
        if (CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if (canJump) { HandleJump(); }
            if (canCrouch) { HandleCrouch(); }
            if (canShoot) { HandleShoot(); }
            if (canDash) { HandleDash(); }
            if (Input.GetKeyUp(creatureCageHealKey) && creatureCage != null && creatureCage.GetComponent<CageTarget>().canHeal && cageMaterialPickups >= 1) { HandleHeal(); }

            ApplyFinalMovements();
        }
    }

    private void HandleMovementInput()
    {
        if (characterController.isGrounded && canDoubleJump) { remainingJumps = 1; } // resets the players ability to doublejump upon hitting the ground

        currentInput = new Vector2((isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Vertical"), (isCrouching ? crouchSpeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxis("Horizontal"));
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            if (playerAudioSourceWalk != null && optionsManager != null)
            {
                if (IsSprinting) // increase pitch of sound file to make it appear faster when the player is running
                {
                    if (currentScene.name == "Forest-1" || currentScene.name == "Sandy-1") { playerAudioSourceWalk.pitch = 1.25f; }
                    else if (currentScene.name == "Rocky-1") { playerAudioSourceWalk.pitch = 1.5f; }
                }
                else
                {
                    if (currentScene.name == "Forest-1" || currentScene.name == "Sandy-1") { playerAudioSourceWalk.pitch = 0.75f; }
                    else if (currentScene.name == "Rocky-1") { playerAudioSourceWalk.pitch = 1f; }
                }
                playerAudioSourceWalk.volume = 1f * (optionsManager.sfxVolume * optionsManager.masterVolume);
                playerAudioSourceWalk.enabled = true;
            }
        }
        else
        {
            if (playerAudioSourceWalk != null) { playerAudioSourceWalk.enabled = false; }
        }


        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        float sensX = 1f;
        float sensY = 1f;

        if (optionsManager != null)
        {
            sensX = optionsManager.mouseSensX;
            sensY = optionsManager.mouseSensY;
        }

        rotationX -= Input.GetAxis("Mouse Y") * (lookSpeedY * sensY);
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        gunCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * (lookSpeedX * sensX), 0);
    }

    private void HandleJump()
    {
        if (ShouldJump) 
        {
            moveDirection.y = jumpForce;
            if (canDoubleJump && !characterController.isGrounded) { remainingJumps = 0; }
        }
    }

    private void HandleCrouch()
    {
        if (ShouldCrouch) { StartCoroutine(CrouchStand()); }
    }

    private void HandleShoot()
    {
        if (ShouldShoot)
        {
            shootScript.Shoot(playerCamera.transform);
            if (playerAudioSourceOneShots != null && shootSound != null && optionsManager != null) { playerAudioSourceOneShots.PlayOneShot(shootSound, 1f * (optionsManager.sfxVolume * optionsManager.masterVolume)); }
            StartCoroutine(CanPlayerShoot());
        }
    }

    private void HandleDash()
    {
        if (ShouldDash) 
        {
            currentInput = new Vector2((ShouldDash ? dashSpeed : walkSpeed) * Input.GetAxis("Vertical"), (ShouldDash ? dashSpeed : walkSpeed) * Input.GetAxis("Horizontal"));

            float moveDirectionY = moveDirection.y;
            moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
            moveDirection.y = moveDirectionY;
            if (playerAudioSourceOneShots != null && dashSound != null && optionsManager != null) { playerAudioSourceOneShots.PlayOneShot(dashSound, 1f * (optionsManager.sfxVolume * optionsManager.masterVolume)); }

            StartCoroutine(DashCooldown());
        }
    }

    private void HandleHeal()
    {
        if (creatureCage.GetComponent<CageTarget>().currentHealth < creatureCage.GetComponent<CageTarget>().maxHealth) 
        {
            creatureCage.GetComponent<CageTarget>().IncreaseHealth(healAmount);
            HandleMaterialCollection(-1);
        }
    }

    public void HandleMaterialCollection(int numToAdd)
    {
        cageMaterialPickups += numToAdd;
        playerUIManager.GetScrapCount(cageMaterialPickups);
    }

    private void ApplyFinalMovements()
    {
        if (!characterController.isGrounded) { moveDirection.y -= gravity * Time.deltaTime; }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 2f)) { yield break; }

        duringCrouchAnimation = true;

        float timeElasped = 0f;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenterPoint : crouchingCenterPoint;
        Vector3 currentCenter = characterController.center;

        while (timeElasped < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElasped / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElasped / timeToCrouch);
            timeElasped += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }

    // time between the player being able to shoot -- Firerate
    private IEnumerator CanPlayerShoot()
    {
        canShoot = false;

        yield return new WaitForSeconds(0.2f);

        canShoot = true;
    }

    // time between the player being able to dash -- Dash Cooldown
    private IEnumerator DashCooldown()
    {
        dashCooldown = maxDashCooldown;

        canDash = false;

        while(dashCooldown > 0)
        {
            dashCooldown -= Time.deltaTime;
            yield return null;
        }

        canDash = true;
    }
}