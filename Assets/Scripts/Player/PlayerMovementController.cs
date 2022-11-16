using Bonus;
using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [field: Header("Movement")]
        [field: SerializeField] public float moveSpeed { get; set; }
        [field:SerializeField]public float walkSpeed{ get; set; }
        
        [SerializeField]private float sprintSpeed;
        [SerializeField]private float airMultiplier;
        [SerializeField]private float groundDrag;
        [SerializeField]private float maxSlopeAngle;
        private float startYScale { get; set; }
    
        [Header("Keybinds")]
        [SerializeField]private KeyCode sprintKey = KeyCode.LeftShift;

        [Header("Ground Check")]
        [SerializeField]private float playerHeight;
        [SerializeField]private LayerMask WhatIsGround;
        [SerializeField]private bool grounded;
        [SerializeField]private Transform orientation;
        private bool exitingSlope { get; set; }
        private RaycastHit slopeHit { get; set; }

        private float horizontalInput;
        private float verticalInput;

        private Vector3 moveDirection;

        private Rigidbody2D rb;

        private MovementState state;

        private enum MovementState
        {
            walking,
            sprinting
        }

        public Trap trap;
        public AutoWeaponController autoWeaponController;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            rb.freezeRotation = true;

            startYScale = transform.localScale.y;
        }
        
        private void Update()
        {
            // ground check
            grounded = Physics2D.Raycast(transform.position, Vector3.down, playerHeight, WhatIsGround);

            MyInput();
            SpeedControl();
            StateHandler();

            //handle drag
            if(grounded) rb.drag = groundDrag;
            else rb.drag = 0;
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }

        private void MyInput()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }

        private void StateHandler()
        {
            //Mode - Sprinting
            if(grounded && Input.GetKey(sprintKey))
            {
                state = MovementState.sprinting;
                moveSpeed = sprintSpeed;
            }

            //Mode - walking
            else if(grounded)
            {
                state = MovementState.walking;
                moveSpeed = walkSpeed;
            }
        }

        private void MovePlayer()
        {
            // calculate movement direction
            moveDirection = orientation.up * verticalInput + orientation.right * horizontalInput;
        
            //on slope
            if(OnSlope() && !exitingSlope)
            {
                rb.AddForce(GetSlopeMoveDirection() * moveSpeed, (ForceMode2D) ForceMode.Force);

                if(rb.velocity.y > 0)
                    rb.AddForce(Vector3.down , (ForceMode2D) ForceMode.Force);
            }

            //on ground
            if(grounded)
            {
                rb.AddForce(moveDirection.normalized * (moveSpeed *20f), (ForceMode2D) ForceMode.Force);
            }

            //in air
            else if(!grounded)
            {
                rb.AddForce(moveDirection.normalized * (moveSpeed * 10f * airMultiplier), (ForceMode2D) ForceMode.Force);
            }

            //turn gravity off while on slope
            rb.useFullKinematicContacts = !OnSlope();
        }

    

        private void SpeedControl()
        {

            //limit speed on slope
            if(OnSlope() && !exitingSlope)
            {
                if(rb.velocity.magnitude > moveSpeed)
                    rb.velocity = rb.velocity.normalized * moveSpeed;
            }

            //limiting speed on ground or in air
            else
            {
                Vector3 flatVel = new Vector3(rb.velocity.x,  rb.velocity.y);

                //limit velocity if needed
                if(flatVel.magnitude > moveSpeed)
                {
                    Vector3 limitedVel = flatVel.normalized * moveSpeed;
                    rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
                }
            }     
        }

        private bool OnSlope()
        {
            if(Physics.Raycast(transform.position, Vector3.down, out var raycastHit, playerHeight * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, raycastHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }

            return false;
        }

        private Vector3 GetSlopeMoveDirection()
        {
            return Vector3.ProjectOnPlane(moveDirection, slopeHit.normal).normalized;
        }
    }
}
