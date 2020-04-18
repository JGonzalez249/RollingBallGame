using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public GameObject rightTxt;
    public GameObject leftTxt;
    public GameObject noneTxt;

    public ParticleSystem rChargeParticle;
    public ParticleSystem lChargeParticle;

    public Transform mesh;

    public Rigidbody rb;

    public AudioSource ChargeSource;
    public AudioSource ChargeRelease;

    [SerializeField] public PowerBar powerBar;

    public int barMultiplier; // variable multiplier bar increase
    public int speedMultiplier; // variable multiplies the speed
    public int maxSpeed;

    private float spin;
    private float slow;

    public bool holdLeft;
    public bool holdRight;
    public bool goRight;
    public bool goLeft;
    public bool upPress;
    public bool downPress;
    public bool leftPress;
    public bool rightPress;
    public bool fireRight;
    public bool fireLeft;


    public float force;
    public float spinForce;

    private int maxForce = 100;

    // Start is called before the first frame update
    private void Start()
    {
        goRight = false;
        goLeft = false;
        powerBar.SetSize(0.0f);
        lChargeParticle.Stop();
        rChargeParticle.Stop();
        ChargeRelease.Stop();
        ChargeSource.Stop();

        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {

            //slow down over time
            rb.velocity = new Vector3(rb.velocity.x * slow, rb.velocity.y);

            mesh.transform.Rotate(0, 0, spin * spinForce + rb.velocity.x); // spin

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
                if (upPress == true) // spin
                {
                    if (leftPress == true)
                    {
                        if (downPress == true)
                        {
                            if (rightPress == true)
                            {
                                upPress = false;
                                leftPress = false;
                                rightPress = false;
                                downPress = false;

                                if (holdRight == true && holdLeft == false)
                                {
                                    rChargeParticle.Play();
                                }
                                if (holdLeft == true && holdRight == false)
                                {
                                    lChargeParticle.Play();
                                }

                                if (force == 0)
                                {
                                    ChargeSource.Play();
                                }

                                if (force < maxForce) // don't go over max force
                                {
                                    StartCoroutine(IncreaseBar());
                                }
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
                    spinForce = 0;
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
                    spinForce = 0;
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
            if (spinForce >= maxSpeed)
            {
                spinForce = maxSpeed;
            }
        }
    }

    private IEnumerator IncreaseBar()
    {
        int i = 1;

        while (i < barMultiplier && force != maxForce && goLeft == false && goRight == false)
        {
            yield return force += 4.15f;
            yield return spinForce++;
            yield return i++;
        }
        StopCoroutine(IncreaseBar());
    }

    private IEnumerator OnUp(InputValue value) // up
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            yield return upPress = true;
        }
    }

    private IEnumerator OnRight(InputValue value) // right
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            yield return rightPress = true;
        }
    }

    private IEnumerator OnDown(InputValue value) // down
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            yield return downPress = true;
        }
    }

    private IEnumerator OnLeft(InputValue value) // left
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            yield return leftPress = true;
        }
    }

    private IEnumerator OnMoveLeft(InputValue value) // left trigger hold
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            spin = -1;
            if (rb.velocity.x > 0) // if previously going right
            {
                
                spinForce /= 4;
                if (GameObject.Find("Player").GetComponent<GrapplingHook>().hooked == false) // not hooked
                {
                    slow = 0.97f;
                }
            }
           
            yield return goLeft = false;
            yield return goRight = false;
            yield return holdLeft = true;
        }
    }

    private IEnumerator OnMoveRight(InputValue value) // right trigger hold
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            if (rb.velocity.x < 0) // if previously going left
            {
                spin = 1;
                spinForce /= 4;
                if (GameObject.Find("Player").GetComponent<GrapplingHook>().hooked == false) // not hooked
                {
                    slow = 0.97f;
                }

            }

            yield return goRight = false;
            yield return goLeft = false;
            yield return holdRight = true;
        }
    }

    private IEnumerator OnMoveLeftRelease(InputValue value) // left trigger release
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            yield return holdLeft = false;

            if(holdLeft == false && holdRight == false && force > 1)
            {
                ChargeRelease.Play();
                ChargeSource.Stop();
            }

            lChargeParticle.Stop();

            if (holdRight == false)
            {
                yield return goLeft = true;
            }
            if (goLeft == true)
            {
                yield return fireLeft = true;
                spin = 1f;
                slow = 1f; // no slowdown
            }
        }
    }

    private IEnumerator OnMoveRightRelease(InputValue value) // right trigger release
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            yield return holdRight = false;

            if (holdLeft == false && holdRight == false && force > 1)
            {
                ChargeRelease.Play();
                ChargeSource.Stop();
            }

            rChargeParticle.Stop();

            if (holdLeft == false)
            {
                yield return goRight = true;
            }
            if (goRight == true)
            {
                yield return fireRight = true;
                slow = 1f; // no slowdown
            }
        }
    }
}