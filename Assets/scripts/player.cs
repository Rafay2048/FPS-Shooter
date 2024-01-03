using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float closerange;
    public float walkingspeed = 10f;
    public float crouchingspeed = 5f;
    public float gravityModifier = 1f;
    public float jumpheight = 10f;
    public float mousesensitivity = 100f;
    public float gunRange = 100f;
    public Transform firingPosition;
    public Transform myCameraHead;
    public Transform myBody;
    public GameObject bullet;
    public GameObject muzzleFlash;
    public GameObject bulletimpact;
    public CharacterController mycc;
    Vector3 normalscale;
    Vector3 yvelocity;
    Vector3 playerscale;
    Vector3 crouchscale = new Vector3(1, 0.5f, 1);
    float cameraVerticalMovement;
    float initialControllerHeight;
    bool isCrouching = false;
    

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
        shooting();
        jump();
        crouch();
        //sprint();


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
    }

    private void EndCrouching()
    {
        myBody.localScale = normalscale;
        mycc.height = initialControllerHeight;
        isCrouching = false;
    }



    private void sprint()
    {





    }


    private void shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            //only log the value using debug if the raycast has actually hiy something
            if (Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, gunRange))
            {
                if (Vector3.Distance(firingPosition.position, hit.point) > closerange)
                {

                firingPosition.LookAt(hit.point);

                }

                Instantiate(bulletimpact, hit.point, Quaternion.LookRotation(hit.normal));

                Instantiate(bullet, firingPosition.position, firingPosition.rotation);

                Debug.Log(hit.transform.name);
            }
            Instantiate(bullet, firingPosition.position, firingPosition.rotation);
            Instantiate(muzzleFlash, firingPosition.position, firingPosition.rotation);
            
        }

    }


    private void playermovement()
    {
        float moveX = Input.GetAxis("Horizontal");

        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveX + transform.forward * moveZ;

        //get cc and use its move functiom
        //use the move function

        if(isCrouching) //if isCrouching is = true
        {

            movement *= (crouchingspeed * Time.deltaTime);

        }
        else
        {

            movement *= (walkingspeed * Time.deltaTime);

        }

        mycc.Move(movement); 

        //adding new physics value to entire previous velocity
        //Vector3 yvelocity = myccivelocity + Physics.gravity;

        yvelocity.y += mycc.velocity.y + Physics.gravity.y * gravityModifier;  //(0, -1, 0)


        if(mycc.isGrounded)
        {

            Debug.Log("is grounded");
            yvelocity.y = Physics.gravity.y * Time.deltaTime;
            // in our y vector = -1 * 0.016 = -0.016

        }



        mycc.Move(yvelocity);

    }

    private void mouseMovement()
    {
        float xmovement = Input.GetAxisRaw("Mouse X") * mousesensitivity * Time.deltaTime;
        transform.Rotate(Vector3.up * xmovement); //rotating left/right





        float ymovement = Input.GetAxisRaw("Mouse Y") * mousesensitivity * Time.deltaTime;
        cameraVerticalMovement = Mathf.Clamp(ymovement, -50f, 50f);
        cameraVerticalMovement += ymovement;

        //data is input in opposite form
        ymovement = ymovement * -1;          
        myCameraHead.localRotation = Quaternion.Euler(cameraVerticalMovement, 0, 0); // movement up/down
        //Debug.Log(ymovement);
        
    }                          

    private void jump()

    {   //only allow to jump if we are touching the ground

        if(Input.GetButtonDown("jump") && mycc.isGrounded)
            {
              yvelocity.y = jumpheight; //yvelocity is being subtracted in playermovement to create an effect of gravity
              mycc.Move(yvelocity);
            }

    }

}


