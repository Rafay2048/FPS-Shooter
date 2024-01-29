using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunsystem : MonoBehaviour
{   //bullet//gun
    public Transform myCameraHead;
    public float gunRange = 100f;
    public GameObject bullet;
    public GameObject muzzleFlash;
    public GameObject bulletimpact;
    public Transform firingPosition;
    public float closerange;

    //fire

    public int magazinesize = 15;
    public int currentbullets = 0;
    public int totalbullets = 100;

    public bool canAutoFire = false;

    bool shootingInput;
    //reload






    // Start is called before the first frame update
    void Start()
    {
        currentbullets = magazinesize;
        currentbullets -= magazinesize;
    }

    // Update is called once per frame
    void Update()
    {
        shooting();
        reload();
    }

    private void shooting()
    {
        if (canAutoFire)
        {
            shootingInput = Input.GetMouseButton(0);
        }

        else 
        {
            shootingInput = Input.GetMouseButtonDown(0);
        }


        
        if (shootingInput)
        {
            if (currentbullets > 0)
            {


                RaycastHit hit;
                //only log the value using debug if the raycast has actually hiy something
                if (Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, gunRange))
                {
                    if (Vector3.Distance(firingPosition.position, hit.point) > closerange)
                    {

                        firingPosition.LookAt(firingPosition.transform.forward * closerange);

                    }
                    else
                    {

                        firingPosition.LookAt(hit.point);
                    }

                    Instantiate(bulletimpact, hit.point, Quaternion.LookRotation(hit.normal));

                    Instantiate(bullet, firingPosition.position, firingPosition.rotation);

                    //Debug.Log(hit.transform.name);
                }
                Instantiate(bullet, firingPosition.position, firingPosition.rotation);
                Instantiate(muzzleFlash, firingPosition.position, firingPosition.rotation);
                totalbullets--;
            }
        }

    }

    private void reload()
    {
        if(Input.GetKey(KeyCode.R) && currentbullets < magazinesize)
        {
            int bulletsToAdd = magazinesize - currentbullets;
            if(totalbullets>bulletsToAdd)
            {
                totalbullets -= bulletsToAdd;
                currentbullets += bulletsToAdd;
            }
            else
            {
                currentbullets += totalbullets;
                totalbullets = 0;
            }

        }
    }
}
