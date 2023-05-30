using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{
    public bool CanMovePlayer { get; private set; } = true;
    private bool IsSprinting => canSprint && runPressed && characterController.isGrounded;
    private bool ShouldJump => jumpPressed && characterController.isGrounded;
    private bool ShouldCrouch => !duringCrouchAnim && characterController.isGrounded;
    //private bool ShouldCrouch => crouchPressed && !duringCrouchAnim && characterController.isGrounded;
    //Component
    InputManager inputManager;
    CharacterController characterController;

    [Header("Functional Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    [SerializeField] private bool canCrouch = true;
    [SerializeField] private bool canUseHeadbob = true;
    [SerializeField] private bool willSlideOnSlopes = true;
    [SerializeField] private bool canInteract = true;
    [SerializeField] private bool useFootsSteps = true;

    [Header("Movement parameters")]
    [SerializeField] private Vector3 verticalDirection;
    [SerializeField] private float crouchSpeed = 2f;
    [SerializeField] private float walkingSpeed = 4f;
    [SerializeField] private float runningSpeed = 8f;
    [SerializeField] private float sloopSpeed = 8f;

    [Header("Jump parameters")]
    [SerializeField] private float gravity = Physics.gravity.y * 3f;
    [SerializeField] private float jumpHeight = 2f;

    [Header("crouch parameters")]
    [SerializeField] private float crouchHeight = 0.9f;
    [SerializeField] private float standingHeight = 1.8f;
    [SerializeField] private float timeToCrouch = 0.2f;
    [SerializeField] private Vector3 crouchCenter = new Vector3(0, 0.4f, 0);
    [SerializeField] private Vector3 standCenter = new Vector3(0, -.1f, 0);
    private float lenghtStuckDetection => crouchCenter.y + standingHeight / 2 + 0.1f;
    private bool isCrouching;
    private bool duringCrouchAnim;
    private bool crouchStuck;
    private Coroutine crouchingPlayer;

    [Header("Bob parameters")]
    [SerializeField] private float walkBobSpeed = 10f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 14f;
    [SerializeField] private float sprintBobAmount = 0.1f;
    [SerializeField] private float crouchBobSpeed = 7f;
    [SerializeField] private float crouchBobAmount = 0.02f;
    private float defaultYPos = 0;
    private float timer;

    [Header("Footstep parameters")]
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultiplier = 0.05f;
    [SerializeField] private float sprintStepMultiplier = 14f;
    [SerializeField] private AudioSource footStepsAudioSource = default;
    [SerializeField] private AudioClip[] footStepsClips = default;
    private float footSteepTimer = 0f;
    private float GetCurrentStepOffset => isCrouching ? baseStepSpeed * crouchStepMultiplier : IsSprinting ? baseStepSpeed * sprintStepMultiplier : baseStepSpeed;
    //Sliding parameters
    private Vector3 hitPointNormal;
    private bool IsSliding
    {
        get
        {
            if (characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 1f))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
            }
            else
                return false;
        }
    }

    [Header("Interaction")]
    [SerializeField] private Vector3 interactionRayPoint = default;
    [SerializeField] private float interactionDistance = default;
    [SerializeField] private LayerMask interactionLayer = default;
    private Interactable currentInteractable;
    private Interactable objectOnHand;
    private bool hasObjectInHand = false;

    //Button inputs
    private Vector2 moveDirectionInput { get { return inputManager.moveDirectionVector; } }
    private bool jumpPressed { get { return inputManager.jumping; } }
    private bool runPressed { get { return inputManager.running; } }
    private bool crouchPressed { get { return inputManager.crouching; } }

    //
    private Camera cameraPlayer;
    private Vector3 movementDirection;
    public Transform handPosition;
    private void OnEnable()
    {
        EventManager.OnInteractPressed += HandleInteractionInput;
    }
    private void OnDisable()
    {
        EventManager.OnInteractPressed -= HandleInteractionInput;
    }

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();
        cameraPlayer = GetComponentInChildren<Camera>();
        defaultYPos = cameraPlayer.transform.localPosition.y;
        cameraPlayer.transform.rotation = Quaternion.identity;
    }
    void Update()
    {

        if (CanMovePlayer)
        {
            HandleGravity();
            HandleMove();

            if (canJump)
                HandleJump();
            if (canCrouch)
                HandleCrouch();
            if (canUseHeadbob)
                HandleHeadbob();
            if (canInteract)
            {
                HandleInteractionCheck();
            }
            if (useFootsSteps)
                HandleFootsteps();
            ApplyFinalMovement();
        }
    }
    void HandleMove()
    {
        movementDirection = (transform.right * moveDirectionInput.x * (isCrouching ? crouchSpeed : IsSprinting ? runningSpeed : walkingSpeed))
                          + (transform.forward * moveDirectionInput.y * (isCrouching ? crouchSpeed : IsSprinting ? runningSpeed : walkingSpeed));
        movementDirection.y = verticalDirection.y;
    }
    void HandleJump()
    {
        if (ShouldJump)
        {
            verticalDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    void HandleGravity()
    {
        if (!ShouldJump)
            verticalDirection.y += gravity * Time.deltaTime;
        if (characterController.isGrounded)
            verticalDirection.y = 0f;

    }
    void HandleCrouch()
    {
        //if (ShouldCrouch)
        //   StartCoroutine(CrouchStand());

        if (crouchPressed)
        {
            if (!isCrouching && !duringCrouchAnim)
            {
                if (crouchingPlayer != null)
                {
                    StopCoroutine(crouchingPlayer);
                }
                crouchingPlayer = StartCoroutine(CrouchHold());
                //Debug.Log("HOLDING");
            }
        }
        else
        {
            if (isCrouching && !duringCrouchAnim)
            {
                if (crouchingPlayer != null)
                {
                    StopCoroutine(crouchingPlayer);
                }
                crouchingPlayer = StartCoroutine(CrouchRelease());
                //Debug.Log("RELEASE");
            }
            else if (crouchStuck)
            {
                crouchingPlayer = StartCoroutine(CrouchRelease());
            }
        }


    }
    void HandleHeadbob()
    {
        if (!characterController.isGrounded) return;

        if (Mathf.Abs(movementDirection.x) > 0.1f || Mathf.Abs(movementDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);

            cameraPlayer.transform.localPosition = new Vector3(
                cameraPlayer.transform.localPosition.x,
                defaultYPos + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                cameraPlayer.transform.localPosition.z);
        }
    }
    void HandleInteractionCheck()
    {
        if (Physics.Raycast(cameraPlayer.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            //int layerToInteract = LayerMask.NameToLayer("Interaction");
            if (hit.collider.gameObject.layer == 8 && (currentInteractable == null || hit.collider.gameObject.GetInstanceID() != currentInteractable.GetInstanceID()))
            {
                hit.collider.TryGetComponent(out currentInteractable);

                if (currentInteractable)
                    currentInteractable.OnFocus();
            }
        }
        else if (currentInteractable)
        {
            currentInteractable.OnLoseFocus();
            currentInteractable = null;
        }
    }
    void HandleInteractionInput()
    {
        if (currentInteractable != null && Physics.Raycast(cameraPlayer.ViewportPointToRay(interactionRayPoint), out RaycastHit hit, interactionDistance, interactionLayer))
        {
            //Check type of the currentInteractableObject
            if (currentInteractable is ShowObjectDescription)
            {
                currentInteractable.OnInteract();
            }
            else if (currentInteractable is PickUpObject)
            {
                if (objectOnHand==null)
                {
                    hasObjectInHand = true;
                    objectOnHand = currentInteractable;
                    objectOnHand.transform.parent = handPosition;
                    objectOnHand.transform.localPosition = Vector3.zero;
                    objectOnHand.transform.localRotation = Quaternion.Euler(-90f, 180, 0);
                    objectOnHand.GetComponent<Collider>().enabled = false;
                }
            }
            else if (currentInteractable is PlacePickedObject)
            {
                if (objectOnHand != null)
                {
                    if (currentInteractable.GetComponent<PlacePickedObject>().OnPlaceObject(objectOnHand))
                    {
                        //objectOnHand.GetComponent<Collider>().enabled = true;
                        objectOnHand = null;
                        hasObjectInHand = false;
                    }
                    currentInteractable.OnInteract();
                }else
                    currentInteractable.OnInteract();

            }
        }

        //activate OnInteract of the object in hand(no hace nada de momento)
        if (objectOnHand)
        {
            objectOnHand.OnInteract();
        }


    }
    void HandleFootsteps()
    {
        if (!characterController.isGrounded) return;
        if (moveDirectionInput == Vector2.zero) return;

        footSteepTimer -= Time.deltaTime;

        if(footSteepTimer <= 0)
        {
            footStepsAudioSource.PlayOneShot(footStepsClips[Random.Range(0, footStepsClips.Length - 1)]);
            footSteepTimer = GetCurrentStepOffset;
        }
    }
    void ApplyFinalMovement()
    {
        //if (!characterController.isGrounded)
        //    movementDirection.y -= gravity * Time.deltaTime;

        if (willSlideOnSlopes && IsSliding)
            movementDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * sloopSpeed;
        characterController.Move(movementDirection * Time.deltaTime);
    }

    #region Corroutines

    IEnumerator CrouchHold()
    {
        duringCrouchAnim = true;

        float timeElapsed = 0f;
        float targetHeight = crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = crouchCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        duringCrouchAnim = false;
        isCrouching = true;
    }
    IEnumerator CrouchRelease()
    {

        crouchStuck = false;
        Debug.DrawRay(transform.position, Vector3.up, Color.red);
        if (Physics.Raycast(transform.position, Vector3.up, lenghtStuckDetection, this.gameObject.layer))
        {
            Debug.Log("Stuck");
            crouchStuck = true;
            yield break;
        }


        duringCrouchAnim = true;


        float timeElapsed = 0f;
        float targetHeight = standingHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = standCenter;
        Vector3 currentCenter = characterController.center;

        while (timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        duringCrouchAnim = false;
        isCrouching = false;
    }
    #endregion
}
