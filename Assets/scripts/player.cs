using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //movement
    public float mousesensitivity = 100f;
    public float runningspeed = 30f;
    public float walkingspeed = 15f;
    public CharacterController mycc;
    public Transform myCameraHead;
    float cameraVerticalMovement;

    //crouch
    public float crouchingspeed = 5f;
    Vector3 crouchscale = new Vector3(1, 0.5f, 1);
    public Transform myBody;
    bool isCrouching = false;
    float initialControllerHeight;
    Vector3 normalscale;
    Vector3 playerscale;

    //jumping
    public float jumpheight = 10f;
    public float gravityModifier = 1f;
    Vector3 velocity;


    //animator
    public Animator myAnimator;


    //sliding
    public float slidingspeed = 20f;
    bool isSliding = false;
    bool isRunning = false;
    public float slidetime = 0;
    public float maxSlideTime = 3;



    // public Image crosshair;
    //so we have something to look at
    
    // Start is called before the first frame update
    void Start()
    {
        normalscale = myBody.localScale; //saves players normal height at start of game
        initialControllerHeight = mycc.height;
    }

    

    // Update is called once per frame
    void Update()
    {
        playermovement();
        mouseMovement();
        
        jump();
        crouch();
        sprint();
        sliding();

        //firingPosition.LookAt(crosshair.transform);

        //Debug.DrawRay(firingPosition.position, firingPosition.forward, Color.red);


    }

    private void crouch()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            StartCrouching();
            
        }

        if(Input.GetKeyUp(KeyCode.C))
        {
            EndCrouching();
        }

    }

    private void StartCrouching()
    {

        myBody.localScale = crouchscale;
        mycc.height /= 2;
        isCrouching = true;

        if(isRunning)
        {
            //this velocity is being passed on to mycc by 
            velocity = Vector3.ProjectOnPlane(myCameraHead.transform.forward, Vector3.up);
            slidetime = 0;
            isSliding = true;
        }
    }

    private void EndCrouching()
    {
        myBody.localScale = normalscale;
        mycc.height = initialControllerHeight;
        isCrouching = false;
    }



    private void sprint()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }

        else
        {
            isRunning = false;
        }
    }


   


    private void playermovement()
    {
        float moveX = Input.GetAxis("Horizontal");

        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveX + transform.forward * moveZ;
        if (isSliding)

        { 
            if (isRunning && !isCrouching)
            {
                movement *= (runningspeed * Time.deltaTime);
            }

            else
            {
                if (isCrouching)
                {
                    movement *= (crouchingspeed * Time.deltaTime);

                }
                else
                {
                    movement *= (walkingspeed * Time.deltaTime);
                }
            }
        }


        
        //get cc and use its move functiom
        //use the move function


        //Debug.Log(movement.magnitude);
        mycc.Move(movement);

        //adding new physics value to entire previous velocity
        //Vector3 yvelocity = myccivelocity + Physics.gravity;

        myAnimator.SetFloat("playerspeed", movement.magnitude);
        velocity.y += mycc.velocity.y + Physics.gravity.y * gravityModifier;  //(0, -1, 0)


        if(mycc.isGrounded)
        {

            //Debug.Log("is grounded");
            velocity.y = Physics.gravity.y * Time.deltaTime;
            // in our y vector = -1 * 0.016 = -0.016

        }



        mycc.Move(velocity);

    }

    private void mouseMovement()
    {
        float xmovement = Input.GetAxisRaw("Mouse X") * mousesensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * xmovement); //rotating left/right





        float ymovement = Input.GetAxisRaw("Mouse Y") * mousesensitivity * Time.deltaTime;

        ymovement = ymovement * -1;          
        cameraVerticalMovement += ymovement;

        cameraVerticalMovement = Mathf.Clamp(cameraVerticalMovement, -90f, 90f);


        //data is input in opposite form
        myCameraHead.localRotation = Quaternion.Euler(cameraVerticalMovement, 0, 0); // movement up/down
        //Debug.Log(ymovement);
        
    }

    private void jump()
    {   //only allow to jump if we are touching the ground

        if (Input.GetButtonDown("Jump") && mycc.isGrounded)
        {
            velocity.y = jumpheight; //yvelocity is being subtracted in playermovement to create an effect of gravity
            mycc.Move(velocity);
        }

    }
    private void sliding()
    {
        if (isSliding)
        {

            slidetime -= Time.deltaTime;


        }
        if (slidetime > maxSlideTime)
        {

            isSliding = false;
            velocity = Vector3.zero;
        }
    }

}


