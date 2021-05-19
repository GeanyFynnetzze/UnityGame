using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpHeight;

    [SerializeField] private Animator playerAnim;

    private CharacterController controller;
 

    private void Start()
    {
        controller = GetComponent<CharacterController>();
      
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move() 
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //Moves the character on the axes
            moveDirection = new Vector3(horizontal, 0, vertical);
           moveDirection = transform.TransformDirection(moveDirection);





        if (isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                Walk();
            }
            else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                Run();
            }
            else if (moveDirection == Vector3.zero)
            {
                Idle();
            }

            moveDirection *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        



    }

    private void Idle()
    {
        playerAnim.SetFloat("Velocity X", 0f, 0.1f, Time.deltaTime);
        playerAnim.SetFloat("Velocity Z", 0f, 0.1f, Time.deltaTime);
    }

    private void Walk()
    {   //When walking in different directions, with a 2D Blend Tree
        //Backwards-left
        if (Input.GetKey("s") && Input.GetKey("a"))
         { 
            playerAnim.SetFloat("Velocity X",-0.5f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", -0.25f, 0.1f, Time.deltaTime);
            moveSpeed = walkSpeed;
        }
        //Backwards-right
        if (Input.GetKey("s") && Input.GetKey("d"))
        {
            playerAnim.SetFloat("Velocity X", 0.5f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", -0.25f, 0.1f, Time.deltaTime);
            moveSpeed = walkSpeed;
        }
        //Backwards
        if (Input.GetKey("s") )
        {
            playerAnim.SetFloat("Velocity X", 0f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", -0.5f, 0.1f, Time.deltaTime);
            moveSpeed = walkSpeed;
        }
        //Forward
        if (Input.GetKey("w"))
        {
            playerAnim.SetFloat("Velocity X", 0f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", 0.5f, 0.1f, Time.deltaTime);
            moveSpeed = walkSpeed;
        }
        //Forward-left
        if (Input.GetKey("w") && Input.GetKey("a"))
        {
            playerAnim.SetFloat("Velocity X", -0.5f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", 0.5f, 0.1f, Time.deltaTime);
            moveSpeed = walkSpeed;
        }
        //Left
        if (Input.GetKey("a"))
        {
            playerAnim.SetFloat("Velocity X", -0.5f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", 0f, 0.1f, Time.deltaTime);
            moveSpeed = walkSpeed;
        }
        //Right
        if (Input.GetKey("d"))
        {
            playerAnim.SetFloat("Velocity X", 0.5f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", 0f, 0.1f, Time.deltaTime);
            moveSpeed = walkSpeed;
        }
        //Forward-right
        if (Input.GetKey("w") && Input.GetKey("d"))
        {
            playerAnim.SetFloat("Velocity X", 0.5f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", 0.5f, 0.1f, Time.deltaTime);
            moveSpeed = walkSpeed;
        }
       
    }

    private void Run()
    {
       //Still walking backwards
        if (Input.GetKey("s"))
        {
            playerAnim.SetFloat("Velocity X", 0f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", -0.5f, 0.1f, Time.deltaTime);
            moveSpeed = walkSpeed;
        }
        //Running forward
        if (Input.GetKey("w"))
        {
            playerAnim.SetFloat("Velocity X", 0f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", 2f, 0.1f, Time.deltaTime);
            moveSpeed = runSpeed;
        }
        //Running forward-left
        if (Input.GetKey("w") && Input.GetKey("a"))
        {
            playerAnim.SetFloat("Velocity X", -0.5f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", 2f, 0.1f, Time.deltaTime);
            moveSpeed = runSpeed;
        }
        //Running forward-right
        if (Input.GetKey("w") && Input.GetKey("d"))
        {
            playerAnim.SetFloat("Velocity X", 0.5f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", 2f, 0.1f, Time.deltaTime);
            moveSpeed = runSpeed;
        }
        //Running Left
        if (Input.GetKey("a"))
        {
            playerAnim.SetFloat("Velocity X", -1f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", 0f, 0.1f, Time.deltaTime);
            moveSpeed = runSpeed;
        }
        //Running Right
        if (Input.GetKey("d"))
        {
            playerAnim.SetFloat("Velocity X", 1f, 0.1f, Time.deltaTime);
            playerAnim.SetFloat("Velocity Z", 0f, 0.1f, Time.deltaTime);
            moveSpeed = runSpeed;
        }
   
    }
    
    private void Jump()
    {
        //A simple jump, could need improvement
            playerAnim.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        
    }
}
