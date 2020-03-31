using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public GameObject rightTxt;
    public GameObject leftTxt;
    public GameObject noneTxt;

    public Transform mesh;

    public Rigidbody rb;

    [SerializeField] public PowerBar powerBar;

    public int barMultiplier; // variable multiplier bar increase
    public int speedMultiplier; // variable multiplies the speed
    public int maxSpeed;

    private float spin;
    public float slow;

    public bool goRight;
    public bool goLeft;

    private bool upPress;
    private bool downPress;
    private bool leftPress;
    private bool rightPress;
    private bool fireRight;
    private bool fireLeft;

    public bool holdLeft;
    public bool holdRight;

    public float force;
    public float spinForce;

    private int maxForce = 100;

    // Start is called before the first frame update
    private void Start()
    {
        goRight = false;
        goLeft = false;
        powerBar.SetSize(0.0f);

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        //slow down over time
        rb.velocity = new Vector3(rb.velocity.x * slow, rb.velocity.y);
        
        //too fast right
        if (rb.velocity.x >= maxSpeed)
        {
            rb.velocity = new Vector3(maxSpeed, rb.velocity.y);
        }
        //too fast left
        if (rb.velocity.x <= -maxSpeed)
        {
            rb.velocity = new Vector3(-maxSpeed, rb.velocity.y);
        }
        //too fast up
        if (rb.velocity.y >= maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxSpeed);
        }
        //too fast down
        if (rb.velocity.y <= -maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, -maxSpeed);
        }
        //too fast spin
        if(spinForce >= maxSpeed)
        {
            spinForce = maxSpeed;
        }

        if (GameObject.Find("EventSystem").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            mesh.transform.Rotate(0, 0, spin * spinForce); // spin
            if (spinForce > 0 && holdRight == false && holdLeft == false)
            {
                spinForce -= 0.05f; // slow spin down
            }


            powerBar.SetSize(force / 100); //update power bar

            //right-left text
            if (holdRight == true && holdLeft == false) // right text
            {
                rightTxt.SetActive(true);
                leftTxt.SetActive(false);
                noneTxt.SetActive(false);
            }
            if (holdLeft == true && holdRight == false) // left text
            {
                rightTxt.SetActive(false);
                leftTxt.SetActive(true);
                noneTxt.SetActive(false);
            }
            if (holdLeft == false && holdRight == false || holdLeft == true && holdRight == true) // none text
            {
                rightTxt.SetActive(false);
                leftTxt.SetActive(false);
                noneTxt.SetActive(true);
            }

            //increase Charge
            if (holdLeft == true && holdRight == false || holdRight == true && holdLeft == false) // holding either trigger alone
            {
                if (upPress == true)
                {
                    if (leftPress == true || rightPress == true) // clockwise or counterclockwise
                    {
                        if (downPress == true)
                        {
                            upPress = false;
                            leftPress = false;
                            rightPress = false;
                            downPress = false;

                            if (force < maxForce) // don't go over max force
                            {
                                force += barMultiplier;
                                powerBar.SetSize(force / 100); //update power bar
                                spinForce += 3;
                            }
                        }
                    }
                }
            }

            //press fire button to launch stored energy
            if (goRight == true && holdLeft == false)
            {
                if (fireRight == true)
                {
                    rb.AddForce(transform.right * Time.deltaTime * force * speedMultiplier, ForceMode.Impulse);
                    force = 0;
                    fireRight = false;
                    fireLeft = false; // dont fire left
                }
            }
            if (goLeft == true && holdRight == false)
            {
                if (fireLeft == true)
                {
                    rb.AddForce(transform.right * Time.deltaTime * force * -speedMultiplier, ForceMode.Impulse);
                    force = 0;
                    fireLeft = false;
                    fireRight = false; // don't fire right
                }
            }

            //logic and corrections
            if (force > maxForce) // don't go above max force
            {
                force = maxForce;
            }
        }
    }

    private IEnumerator OnUp(InputValue value) // up
    {
        yield return upPress = true;
    }

    private IEnumerator OnRight(InputValue value) // right
    {
        yield return rightPress = true;
    }

    private IEnumerator OnDown(InputValue value) // down
    {
        yield return downPress = true;
    }

    private IEnumerator OnLeft(InputValue value) // left
    {
        yield return leftPress = true;
    }

    private IEnumerator OnMoveLeft(InputValue value) // left trigger hold
    {
        if (goLeft == false) // if previously going right
        {
            spin = -1;
            spinForce /= 4;
            if (GameObject.Find("Player").GetComponent<GrapplingHook>().hooked == false) // not hooked
            {
                slow = 0.97f;
            }
        }
        yield return goLeft = true;
        yield return goRight = false;
        yield return holdLeft = true;
    }

    private IEnumerator OnMoveRight(InputValue value) // right trigger hold
    {
        if (goRight == false) // if previously going left
        {
            spin = 1;
            spinForce /= 4;
            if (GameObject.Find("Player").GetComponent<GrapplingHook>().hooked == false) // not hooked
            {
                slow = 0.97f;
            }

        }
        yield return goRight = true;
        yield return goLeft = false;
        yield return holdRight = true;
        
    }

    private IEnumerator OnMoveLeftRelease(InputValue value) // left trigger release
    {
        yield return holdLeft = false;

        if (goLeft == true)
        {
            yield return fireLeft = true;
            slow = 1f; // no slowdown
        }
    }

    private IEnumerator OnMoveRightRelease(InputValue value) // right trigger release
    {
        yield return holdRight = false;

        if (goRight == true)
        {
            yield return fireRight = true;
            slow = 1f; // no slowdown
        }
    }
}