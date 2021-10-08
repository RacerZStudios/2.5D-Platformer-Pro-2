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
    private bool rolling;
    [SerializeField]
    private Vector3 slideDir;
    [SerializeField]
    private Vector3 climbStartDir;
    [SerializeField]
    private Vector3 climbEndDir;
    [SerializeField]
    private Transform climbAPos;
    [SerializeField]
    private Transform climbBPos; 
    private float min = -1f;
    private float max = 1f;
    private float t = 0f; 
    private bool onLadder;
    private ClimbCommand climbPos;
    [SerializeField]
    private Transform playerPos;

    [SerializeField]
    private Vector3 playerStartPos;
    [SerializeField]
    private Transform playerResetPos;
    [SerializeField]
    private bool resetPlayer;
    [SerializeField]
    private GameObject player; 

    private void Start()
    {
        player.transform.position = playerResetPos.transform.position; 

        climbPos = GetComponentInChildren<ClimbCommand>(); 
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>(); 
        if(controller == null)
        {
            return; 
        }
        if (climbPos == null)
        {
            return;
        }
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.R))
        {
            anim.Rebind();
        }

        climbStartDir = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        climbEndDir = new Vector3(transform.position.x, 5f, transform.position.z);

        CalculateMovement(); 

        if(onLedge == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                anim.SetTrigger("IsClimbUp"); 
            }
        }

        if(onLadder == true)
        {
            if(Input.GetKey(KeyCode.W))
            {
                anim.SetBool("ClimbUp", true);

                transform.position = Vector3.MoveTowards(transform.position, climbBPos.position, speed * 75 * Time.deltaTime);

                // transform.position = climbBPos.transform.position - climbAPos.transform.position;
                // transform.position = new Vector3(Mathf.Lerp(climbStartDir.y, climbEndDir.y, 0.1f), 1, 0);
                transform.position += Vector3.up * Time.deltaTime * speed * 75;
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

            if(rolling == true)
            {
                rolling = false;
                anim.SetBool("IsRoll", rolling); 
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

            if(x != 0)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    rolling = true;
                    anim.SetBool("IsRoll", rolling);
                    Roll();
                }
                else if (!Input.GetKey(KeyCode.LeftShift))
                {
                    rolling = false;
                    anim.SetBool("IsRoll", rolling);
                }
            }
        }

        // get gravity for above method to work 
        // calculate gravity for character controller 
        direction.y -= gravity * Time.deltaTime;

        // if jump 
        // adjust jumpheight

        // move with speed and time step 
      
        if(controller.enabled == true)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
        if (controller != isActiveAndEnabled)
        {
            return;
        }
    }

    public void Roll()
    {
       // Debug.Log("Rolling" + slideDir);
        transform.position = slideDir;
        slideDir += controller.velocity;
        rolling = false; 
        anim.SetFloat("Speed", 1);
    }

    public void GrabLedge(Vector3 handPos, Ledge currentLedge)
    {
        controller.enabled = false;
        anim.SetBool("GrabLedge", true);
        anim.SetFloat("Speed", 0);
        anim.SetBool("IsJump", false); 
       // Debug.Log("Freeze");
        onLedge = true; 
        transform.position = handPos;
        activeLedge = currentLedge; 
    }

    public void ClimbComplete()
    {
        // Debug.Log("Ledge Complete"); 
        onLedge = false;
        transform.position = activeLedge.GetStandPos();
        anim.SetBool("GrabLedge", false);
        controller.enabled = true; 
    }

    public void ClimbLadder(Vector3 startClimbPos, ClimbCommand currentClimbPos)
    {
        controller.enabled = false;
        anim.SetBool("ClimbUp", true);
        anim.SetFloat("Speed", 1);
        anim.SetBool("IsJump", false);
        // Debug.Log("Freeze");
        if(this != null)
        {
            onLadder = true;
            transform.position = startClimbPos;
            climbPos = currentClimbPos;
        }
    }

    public void ClimbUpComplete()
    {
        transform.position = climbPos.GetClimbPos(); 
        anim.SetTrigger("IsClimbUp");
        onLadder= false; 
        controller.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ClimbStart" || other.gameObject.tag == "ClimbObj")
        {
            climbPos.StartCoroutine(climbPos.LerpPos(climbStartDir, 3)); 
            onLadder = true;
            anim.SetBool("ClimbUp", true);
        }

        if (other.gameObject.tag == "ClimbEnd")
        {
           // Debug.Log("ClimbEnd");
            gameObject.transform.position = Vector3.Lerp(transform.position, playerPos.position, 0.1f);
            // Destroy(gameObject); // test 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ClimbEnd" || other.gameObject.tag == "ClimbObj")
        {
            onLadder = false;
            anim.SetBool("ClimbUp", false);
            gameObject.transform.position = playerPos.position;
        }
    }
}