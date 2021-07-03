using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // speed 
    // gravity 
    // direction 
    [SerializeField]
    private float speed = 15;
    [SerializeField]
    private float gravity = 25;
    private Vector3 direction;
    private CharacterController controller;
    [SerializeField]
    private float jumpHeight = 10;
    [SerializeField]
    private Animator anim;
    private bool jumping;
    private bool onLedge;
    private Ledge activeLedge; 

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>(); 
        if(controller == null)
        {
            return; 
        }
    }

    private void Update()
    {
        CalculateMovement(); 

        if(onLedge == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                anim.SetTrigger("IsClimbUp"); 
            }
        }
    }

    private void CalculateMovement()
    {
        // if grounded = true 
        // movement direction on user input 
        if (controller.isGrounded == true && controller != null)
        {
            // if jumping was true previously 
            if (jumping == true)
            {
                jumping = false;
                anim.SetBool("IsJump", jumping);
            }

            float x = Input.GetAxisRaw("Horizontal"); //Raw instant to 0 or -1 
            anim.SetFloat("Speed", Mathf.Abs(x)); // assign value h and set speed anim // get absolute value 
            direction = new Vector3(0, 0, x) * speed;

            // flip character face direction 
            // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 180f, 0); // immutable declaration 
            // if direction x is geater than 0 
            if (x != 0)
            {
                Vector3 facingDirection = transform.localEulerAngles;
                facingDirection.y = direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facingDirection;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction.y += jumpHeight;
                jumping = true;
                anim.SetBool("IsJump", jumping); // set value based off of bool jumping 
            }
        }

        // get gravity for above method to work 
        // calculate gravity for character controller 
        direction.y -= gravity * Time.deltaTime;

        // if jump 
        // adjust jumpheight

        // move with speed and time step 
        controller.Move(direction * speed * Time.deltaTime);
    }

    public void GrabLedge(Vector3 handPos, Ledge currentLedge)
    {
        controller.enabled = false;
        anim.SetBool("GrabLedge", true);
        anim.SetFloat("Speed", 0);
        anim.SetBool("IsJump", false); 
        Debug.Log("Freeze");
        onLedge = true; 
        transform.position = handPos;
        activeLedge = currentLedge; 
    }

    public void ClimbComplete()
    {
        Debug.Log("Climb Up Complete"); 
        transform.position = activeLedge.GetStandPos();
        anim.SetBool("GrabLedge", false);
        controller.enabled = true; 
    }
}
