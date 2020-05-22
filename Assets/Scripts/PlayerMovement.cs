using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpForce = 1000f;

    [Tooltip("Fast Fall uses the shift so when held your gravity acceleration increases")]
    public bool useFastFall = true;
    private float additionalGravity = 0;

    public Transform GroundCheck_LeftRay;
    public Transform GroundCheck_RightRay;

    private float walkSpeed;
    private Rigidbody RB_player;
    private Transform T_player;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        //Application.targetFrameRate = 165;

        // Setup components
        RB_player = GetComponent<Rigidbody>();
        T_player = transform;
        characterController = GetComponent<CharacterController>();

        walkSpeed = speed * .5f;
    }

    Vector3 vel = Vector3.zero;

    private void Update()
    {
        HandleMoveInput();
        HandleMovement();

    }

    void HandleMoveInput()
    {
        vel = Vector3.zero;

        // Movement
        if (Input.GetKey(KeyCode.W))
            vel += transform.forward;

        if (Input.GetKey(KeyCode.S))
            vel += -transform.forward;

        if (Input.GetKey(KeyCode.D))
            vel += transform.right;

        if (Input.GetKey(KeyCode.A))
            vel += -transform.right;
    }
    void HandleMovement()
    {
        characterController.Move(vel * speed * Time.deltaTime);

    }
    void HandleJump()
    {
        if(Input.GetKey(KeyCode.Space) || Mathf.Abs(Input.mouseScrollDelta.y) != 0)
        {
            if (!Create_Raycast_Jump(GroundCheck_LeftRay.position, Vector3.down, .2f))
            {
                Create_Raycast_Jump(GroundCheck_RightRay.position, Vector3.down, .2f);
            }
        }
    }



    // Used for shooting
    public void Create_Raycast(Vector3 startPos, Vector3 direction)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(startPos, direction, out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(startPos, direction * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
    }

    // Raycast to see how far away we are from the ground.
    public bool Create_Raycast_Jump(Vector3 startPos, Vector3 direction, float rayDist)
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(startPos, direction, out hit, rayDist, layerMask))
        {
            Debug.DrawRay(startPos, direction * hit.distance, Color.yellow);
            Debug.Log("Hit Name: " + hit.transform.gameObject.name);

            // Make the player jump
            RB_player.AddForce(Vector3.up * jumpForce);

            return true; // Did jump
        }

        return false; // Cant jump
    }
}
