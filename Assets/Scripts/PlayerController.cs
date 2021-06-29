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

    private void Start()
    {
        controller = GetComponent<CharacterController>(); 
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
            float x = Input.GetAxisRaw("Horizontal");
            direction = new Vector3(0, 0, x) * speed;

            if(Input.GetKeyDown(KeyCode.Space))
            {
                direction.y += jumpHeight; 
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
