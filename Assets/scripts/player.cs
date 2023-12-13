using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    
    public CharacterController mycc;
    public Transform firingPosition;
    public float mousesensitivity = 100f;
    public Transform myCameraHead;
    float cameraVerticalMovement;
    public float gunRange = 100f;
    public GameObject bullet;
    public GameObject muzzleFlash;
    public GameObject bulletimpact;
    public float closerange;
   // public Image crosshair;
    //so we have something to look at
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playermovement();
        mouseMovement();
        shooting();

        //  firingPosition.LookAt(crosshair.transform);

        //Debug.DrawRay(firingPosition.position, firingPosition.forward, Color.red);


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
        mycc.Move(movement);

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
        myCameraHead.localRotation = Quaternion.Euler(cameraVerticalMovement, 0, 0);// movement up/down
        //Debug.Log(ymovement);

    }


}


