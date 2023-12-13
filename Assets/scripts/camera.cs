using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform playerhead;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;  
    }

    // Update is called once per frame
    void LateUpdate()
    {
      transform.position = playerhead.position;
      transform.rotation = playerhead.rotation;
    }

}
 