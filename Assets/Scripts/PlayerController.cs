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
        // if grounded = true 
        // movement direction on user input 
        if(controller.isGrounded == true && controller != null) 
        {
            // if jumping was true preiously 
            if (jumping == true)
            {
                jumping = false;
                anim.SetBool("IsJump", jumping);
            }

            float x = Input.GetAxisRaw("Horizontal"); //Raw instant to 0 or -1 
            anim.SetFloat("Speed", Mathf.Abs(x)); // assign value h and set speed anim // get absolute value 
            direction = new Vector3(0, 0, x) * speed;

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
}
