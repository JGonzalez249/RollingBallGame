﻿using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingHook : MonoBehaviour
{
    public GameObject hook;
    public GameObject hookHolder;
public Rigidbody prb; // player
    public Rigidbody rb; // hook
    public Rigidbody rb2; // hook holder
    public SpringJoint sj;

    public float hookTravelSpeed;
    public float hookStrength;

    public int shootDirX;
    public int shootDirY;

    public bool aimRight;
    public bool aimLeft;
    public bool aimUp;

    public bool fired;
    public bool hooked;
    public GameObject hookedObj;

    public float maxDistance;
    private float currentDistance;

    public GameObject target;

    private void Start()
    {
        sj = GetComponent<SpringJoint>();
        sj.connectedBody = rb2;

        aimUp = true;
    }

    private void Update()
    {
            if (fired == true && hooked == false) // firing
            {
                Fire();

                if (currentDistance >= maxDistance) //don't go too far
                {
                    ReturnHook();
                }
            }

            if (fired == true && hooked == true) // hooked something
            {
                sj.connectedBody = rb;
            }

            if (fired == false && hooked == false) // didn't hook anything
            {
                ReturnHook();
            }
    }

    private IEnumerator OnHook(InputValue value) // east - right button
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            if (fired == false && hooked == false)
            {
                yield return fired = true;
                if (aimRight == true)
                {
                    shootDirX = 1;
                    shootDirY = 1;
                }
                if (aimLeft == true)
                {
                    shootDirX = -1;
                    shootDirY = 1;
                }
                if (aimUp == true)
                {
                    shootDirY = 1;
                    shootDirX = 0;
                }
            }

            if (fired == true && hooked == true) // return if out
            {
                ReturnHook();
            }
        }
    }


        private void Fire()  //firing the hook
    {
        if (aimRight == true) // right
        {
            Vector3 hookVel = new Vector3(3 * shootDirX + Mathf.Abs(prb.velocity.x / 20), 3 * shootDirY, 0);
            hook.transform.Translate(hookVel * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);
        }
        if (aimUp == true)
        {
            Vector3 hookVel = new Vector3(prb.velocity.x / 20, 3 * shootDirY, 0);
            hook.transform.Translate(hookVel * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);
        }
        if (aimLeft == true) // left
        {
            Vector3 hookVel = new Vector3(3* shootDirX - Mathf.Abs(prb.velocity.x / 20), 3 * shootDirY, 0);
            hook.transform.Translate(hookVel * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);
        }
    }

    public void ReturnHook()
    {
        hook.transform.rotation = hookHolder.transform.rotation;
        hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
        sj.connectedBody = rb2;
    }
}