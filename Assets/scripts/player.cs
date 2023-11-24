using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float mousesensitivity = 100f;
    public Transform myCameraHead;
    float cameraVerticalMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playermovement();
        mouseMovement();
    }

    private void mouseMovement()
    {


        float xmovement = Input.GetAxisRaw("Mouse X") * mousesensitivity * Time.deltaTime;


        transform.Rotate(Vector3.up * xmovement);

        ////////////////////////////////////////////////////////////////////////////////////

        cameraVerticalRotation += ymovement;
        float ymovement = Input.GetAxisRaw("Mouse Y") * mousesensitivity * Time.deltaTime;

        float cameraVerticalMovement = Mathf.Clamp(ymovement, -50f, 50f);

        ymovement = ymovement * -1;
        ymovement = Mathf.Clamp(ymovement, -50f, 50f);

        // myCameraHead.transform.Rotate(Vector3.right * ymovement);

        myCameraHead.rotation = Quaternion.Euler(cameraVerticalMovement), 0, 0);

  
        //Debug.Log(ymovement);

    }

    private void playermovement()
    {
        float moveX = Input.GetAxis("Horizontal");

        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveX + transform.forward * moveZ;

        GetComponent<CharacterController>().Move(movement);

    }
        
}


