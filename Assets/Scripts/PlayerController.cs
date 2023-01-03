using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Connection;
using FishNet.Object;
using Unity.VisualScripting;
public class PlayerController : NetworkBehaviour
{
    private bool isRunning = false, isWalking = false, isJumping = false;
    public GameObject musicmanager, player1, player2;
    [SerializeField] private AudioSource[] running;
    [SerializeField] private AudioSource[] jump;
    [SerializeField] private AudioSource[] walking;

    [Header("Base setup")]
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    [HideInInspector]
    public bool canMove = true;

    [SerializeField]
    private float cameraYOffset = 1.665f, cameraZOffset = 0.198f;
    private Camera playerCamera;
    private Animator mAnimator;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            playerCamera = Camera.main;
            playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y + cameraYOffset, transform.position.z + cameraZOffset);
            playerCamera.transform.SetParent(transform);
        }
        else
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }

    void Start()
    {
        musicmanager = GameObject.Find("MusicManagement");
        characterController = GetComponent<CharacterController>();
        mAnimator = GetComponentInChildren<Animator>();
        Cursor.visible = false;
    }

    void Update()
    {
        running[0].volume = musicmanager.gameObject.GetComponent<AudioSource>().volume;
        running[1].volume = musicmanager.gameObject.GetComponent<AudioSource>().volume;
        jump[0].volume = musicmanager.gameObject.GetComponent<AudioSource>().volume;
        jump[1].volume = musicmanager.gameObject.GetComponent<AudioSource>().volume;
        walking[0].volume = musicmanager.gameObject.GetComponent<AudioSource>().volume;
        walking[1].volume = musicmanager.gameObject.GetComponent<AudioSource>().volume;

         // Press Left Shift to run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        if (isRunning && characterController.isGrounded && (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0))
        {
            if (base.IsHost)
            {
                running[0].clip = Resources.Load<AudioClip>("Sounds/running1");
                if (!running[0].isPlaying)
                {
                    running[0].Play(); // if player is moving and audiosource is not playing play it
                }
            }
            else
            {
                running[1].clip = Resources.Load<AudioClip>("Sounds/running2");
                if (!running[1].isPlaying)
                {
                    running[1].Play(); // if player is moving and audiosource is not playing play it
                }
            }
        }
        else
        {
            isRunning = false;
            running[0].Stop();
            running[1].Stop();
        }

        // We are grounded, so recalculate move direction based on axis
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            isJumping = true;
            StartCoroutine(Countdown());
            moveDirection.y = jumpSpeed;
            if (base.IsHost)
            {
                jump[0].clip = Resources.Load<AudioClip>("Sounds/jump1");
                if (!jump[0].isPlaying)
                {
                    jump[0].Play(); // if player is moving and audiosource is not playing play it
                }
            }
            else
            {
                jump[1].clip = Resources.Load<AudioClip>("Sounds/jump2");
                if (!jump[1].isPlaying)
                {
                    jump[1].Play(); // if player is moving and audiosource is not playing play it
                }
            }
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        IEnumerator Countdown()
        {
            yield return new WaitForSeconds(0.867f);
            isJumping = false;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }


            if (!isRunning && characterController.isGrounded && (Input.GetAxis("Vertical")  != 0|| Input.GetAxis("Horizontal") != 0))
        {
            isWalking = true;
            if (base.IsHost)
            {
                walking[0].clip = Resources.Load<AudioClip>("Sounds/walking1");
                if (!walking[0].isPlaying)
                {
                    walking[0].Play(); // if player is moving and audiosource is not playing play it
                }
            }
            else
            {
                walking[1].clip = Resources.Load<AudioClip>("Sounds/walking2");
                if (!walking[1].isPlaying)
                {
                    walking[1].Play(); // if player is moving and audiosource is not playing play it
                }
            }
        }
        else
        {
            isWalking = false;
            walking[0].Stop();
            walking[1].Stop();
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove && playerCamera != null)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (isWalking)
        {
            Walk();
        }
        else if (isRunning)
        {
            Run();
        }
        else if (isJumping)
        {
            Jump();
        }
        else
        {
            Idle();
        }
    }

    public void Idle()
    {
        //Debug.Log("Idle içinde");
        if (mAnimator != null)
        {
            mAnimator.SetFloat("Player1", 0);
        }
    }
    public void Walk()
    {
        //Debug.Log("Walk içinde");
        if (mAnimator != null)
        {
            mAnimator.SetFloat("Player1", 0.25f);
        }
    }
    public void Run()
    {
        //Debug.Log("Run içinde");

        if (mAnimator != null)
        {
            mAnimator.SetFloat("Player1", 0.50f);
        }
    }
    public void Jump()
    {
        //Debug.Log("Jump içinde");
        if (mAnimator != null)
        {
            mAnimator.SetFloat("Player1", 0.75f);
        }
    }

}