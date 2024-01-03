using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float life = 3;
    public float bulletspeed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * bulletspeed;

        life -= Time.deltaTime;

        if(life <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            

        }
    }

}
