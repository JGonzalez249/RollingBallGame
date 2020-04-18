using System.Collections;
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

    public bool reel;
    public bool isReeling = false;
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

        if (fired == false && hooked == false && isReeling == false) // hooked something
        {
            sj.connectedBody = rb2;
        }

        if (fired == true && hooked == true && isReeling == false) // hooked something
        {
            sj.connectedBody = rb;
        }

        if (fired == false && hooked == false) // didn't hook anything
        {
            ReturnHook();
        }
        if (reel == true && hooked == true)
        {
            if (hookHolder.transform.position != hook.transform.position)
            this.transform.position = Vector3.MoveTowards(this.transform.position, hook.transform.position, 0.2f);
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, hookHolder.transform.position, 0.2f);
            prb.velocity = new Vector3(0, 0, 0);
            isReeling = true;
            reel = true;
            sj.connectedBody = rb2;
            prb.useGravity = false;
        }
        if (reel == false && hooked == true)
        {
            prb.useGravity = true;
            sj.connectedBody = rb; // update position
            reel = false;
            isReeling = false;
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

    private IEnumerator OnReel(InputValue value) // east - right button
    {
        if (hooked == true && isReeling == false)
        {
            yield return reel = true;
        }
    }

    private IEnumerator OnReelRelease(InputValue value) // east - right button
    {
        if (hooked == true && isReeling == true)
        {
            yield return reel = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Hookable") // if touched the hoockable object
        {
            if (hooked == true && isReeling == true)
            {
                reel = false;
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
            Vector3 hookVel = new Vector3(3 * shootDirX - Mathf.Abs(prb.velocity.x / 20), 3 * shootDirY, 0);
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