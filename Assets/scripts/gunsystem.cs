using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunsystem : MonoBehaviour
{   //bullet//gun
    public float gunRange = 100f;
    public GameObject bullet;
    public GameObject muzzleFlash;
    public GameObject bulletimpact;
    public Transform firingPosition;
    public float closerange;

    //fire

    bool autofire = false;
    public int magazinesize = 15;






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shooting();
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

                //Debug.Log(hit.transform.name);
            }
            Instantiate(bullet, firingPosition.position, firingPosition.rotation);
            Instantiate(muzzleFlash, firingPosition.position, firingPosition.rotation);

        }

    }
}
