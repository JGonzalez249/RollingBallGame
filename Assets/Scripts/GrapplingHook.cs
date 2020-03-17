using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public GameObject hook;
    public GameObject hookHolder;

    public float hookTravelSpeed;
    public float hookStrength;

    private int shootDirX;

    public bool fired;
    public bool hooked;
    public GameObject hookedObj;

    public float maxDistance;
    private float currentDistance;

    public GameObject target;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && fired == false)
        {
            fired = true;
            shootDirX = 0;
            if (Input.GetKey(KeyCode.E))
            {
                shootDirX++;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                shootDirX--;
            }
        }

        if (fired == true && hooked == false)
        {
            Fire();

            if (currentDistance >= maxDistance)
            {
                ReturnHook();
            }
        }

        if (hooked == true)
        {
            //move toawards hook
            hook.transform.parent = hookedObj.transform;
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * hookStrength);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            //this.GetComponent<Rigidbody>().useGravity = false;

            //player touches hookable object - let go of wall
            if (distanceToHook < 1)
            {
                ReturnHook();
            }
            else
            {
                hook.transform.parent = hookedObj.transform;
                //this.GetComponent<Rigidbody>().useGravity = true;
            }
        }

        if (fired == false && hooked == false)
        {
            ReturnHook();
        }
    }

    void Fire()  //firing the hook
    {
        //Debug.Log("Shot");
        Vector3 hookVel = new Vector3(2 * shootDirX, 2, 0);
        hook.transform.Translate(hookVel * Time.deltaTime * hookTravelSpeed);
        currentDistance = Vector3.Distance(transform.position, hook.transform.position);
    }

    void ReturnHook()
    {
        //Debug.Log("Return");
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
    }
}
