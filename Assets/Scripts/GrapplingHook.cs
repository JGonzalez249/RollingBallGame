using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrapplingHook : MonoBehaviour
{
    public GameObject target;
    public GameObject hook;
    public GameObject hookHolder;
    public Rigidbody prb; // player
    public Rigidbody rb; // hook
    public Rigidbody rb2; // hook holder
    public ConfigurableJoint cj;

    public float hookTravelSpeed;
    public float hookStrength;

    public int shootDirX;
    public int shootDirY;

    public bool aimRight;
    public bool aimLeft;
    public bool aimUp;

    public bool reel;
    public bool drop;
    public bool isReeling = false;
    public bool fired;
    public bool hooked;
    public GameObject hookedObj;

    public float maxDistance;
    private float currentDistance;

    private void Start()
    {
        cj = GetComponent<ConfigurableJoint>();
        cj.connectedBody = rb2;
        cj.xMotion = ConfigurableJointMotion.Free;
        cj.yMotion = ConfigurableJointMotion.Free;

        aimUp = true;
    }

    private void Update()
    {
        if (currentDistance > maxDistance) // don't let hook go too far
        {
            currentDistance = maxDistance;
            drop = false;
        }

        currentDistance = Vector3.Distance(transform.position, hook.transform.position);

        if (fired == true && hooked == false) // firing
        {
            Fire();

            if (currentDistance >= maxDistance) //don't go too far
            {
                ReturnHook();
            }
        }

        if (fired == false && hooked == false && isReeling == false) // not hooked
        {
            cj.connectedBody = rb2;
            cj.xMotion = ConfigurableJointMotion.Free;
            cj.yMotion = ConfigurableJointMotion.Free;
        }

        if (fired == true && hooked == true && isReeling == false) // hooked something
        {
            cj.connectedBody = rb;
            cj.xMotion = ConfigurableJointMotion.Limited;
            cj.yMotion = ConfigurableJointMotion.Limited;
        }

        if (fired == false && hooked == false && hook.transform.position != hookHolder.transform.position) // didn't hook anything
        {
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, hookHolder.transform.position, 2f);
            ReturnHook();
        }
        if (reel == false && hooked == true && drop == true && currentDistance < maxDistance) // dropping
        {
            if (hookHolder.transform.position != hook.transform.position)
            this.transform.position = Vector3.MoveTowards(this.transform.position, hook.transform.position, -0.2f);
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, hookHolder.transform.position, -0.2f);
            if (prb.velocity.y > 0) // rising
            {
                prb.velocity = new Vector3(prb.velocity.x / 1.01f, 0, 0);
            }
            if (prb.velocity.y < 0) // falling
            {
                prb.velocity = new Vector3(prb.velocity.x / 1.01f, prb.velocity.y, 0);
            }
            isReeling = true;
            cj.connectedBody = rb2;
        }

        if (reel == true && hooked == true && drop == false) // reeling
        {
            if (hookHolder.transform.position != hook.transform.position)
            this.transform.position = Vector3.MoveTowards(this.transform.position, hook.transform.position, 0.2f);
            hook.transform.position = Vector3.MoveTowards(hook.transform.position, hookHolder.transform.position, 0.2f);
            if (prb.velocity.y < 0) // falling
            {
                prb.velocity = new Vector3(prb.velocity.x / 1.01f, 0, 0);
            }
            if (prb.velocity.y > 0)
            {
                prb.velocity = new Vector3(prb.velocity.x / 1.01f, prb.velocity.y, 0);
            }
            isReeling = true;
            cj.connectedBody = rb2;
        }

        if (reel == false && hooked == true && drop == false) // neither reel or drop
        {
            //prb.useGravity = true;
            cj.connectedBody = rb; // update position
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
                    hook.transform.rotation = Quaternion.Euler(0, 0, -45);
                    shootDirX = 1;
                    shootDirY = 1;
                }
                if (aimLeft == true)
                {
                    hook.transform.rotation = Quaternion.Euler(0, 0, 45);
                    shootDirX = -1;
                    shootDirY = 1;
                }
                if (aimUp == true)
                {
                    hook.transform.rotation = Quaternion.Euler(0, 0, 0);
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

    private IEnumerator OnReel(InputValue value) // right bumper
    {
        if (hooked == true && isReeling == false && drop == false)
        {
            yield return reel = true;
            prb.velocity = new Vector3(prb.velocity.x / 2, prb.velocity.y, 0);
        }
    }

    private IEnumerator OnReelRelease(InputValue value) // right bumper release
    {
        if (hooked == true && isReeling == true && drop == false)
        {
            yield return reel = false;
        }
    }

    private IEnumerator OnDrop(InputValue value) // left bumper
    {
        if (hooked == true && isReeling == false && reel == false)
        {
            yield return drop = true;
            prb.velocity = new Vector3(prb.velocity.x / 2, prb.velocity.y, 0);
        }
    }

    private IEnumerator OnDropRelease(InputValue value) // left bumper release
    {
        if (hooked == true && isReeling == true && reel == false)
        {
            yield return drop = false;
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
        Vector3 hookVel = new Vector3(prb.velocity.x / 20, 3 * shootDirY, 0);
        hook.transform.Translate(hookVel * Time.deltaTime * hookTravelSpeed);
        /*if (aimRight == true) // right
        {
            Vector3 hookVel = new Vector3(3 * shootDirX + Mathf.Abs(prb.velocity.x / 20), 3 * shootDirY, 0);
            hook.transform.Translate(hookVel * Time.deltaTime * hookTravelSpeed);
            //currentDistance = Vector3.Distance(transform.position, hook.transform.position);
        }
        if (aimUp == true)
        {
            Vector3 hookVel = new Vector3(prb.velocity.x / 20, 3 * shootDirY, 0);
            hook.transform.Translate(hookVel * Time.deltaTime * hookTravelSpeed);
            //currentDistance = Vector3.Distance(transform.position, hook.transform.position);
        }
        if (aimLeft == true) // left
        {
            Vector3 hookVel = new Vector3(3 * shootDirX - Mathf.Abs(prb.velocity.x / 20), 3 * shootDirY, 0);
            hook.transform.Translate(hookVel * Time.deltaTime * hookTravelSpeed);
            //currentDistance = Vector3.Distance(transform.position, hook.transform.position);
        }*/
    }

    public void ReturnHook()
    {
        hook.transform.rotation = hookHolder.transform.rotation;
        //hook.transform.position = hookHolder.transform.position;
        fired = false;
        hooked = false;
        cj.connectedBody = rb2;
    }
}