using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public GameObject RightTxt;
    public GameObject LeftTxt;
    public GameObject NoneTxt;

    public Rigidbody rb;

    [SerializeField] public PowerBar powerBar;

    public int BarMultiplier; // variable multiplier bar increase
    public int speedMultiplier; // variable multiplies the speed

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

    private float force;

    private int maxForce = 100;

    // Start is called before the first frame update
    void Start()
    {
        goRight = false;
        goLeft = false;
        powerBar.SetSize(0.0f);

        rb = GetComponent<Rigidbody>();
    }





    // Update is called once per frame
    void Update()
    {
        powerBar.SetSize(force / 100); //update power bar

        //right-left text
        if (holdRight == true && holdLeft == false) // right text
        {
            RightTxt.SetActive(true);
            LeftTxt.SetActive(false);
            NoneTxt.SetActive(false);
        }
        if (holdLeft == true && holdRight == false) // left text
        {
            RightTxt.SetActive(false);
            LeftTxt.SetActive(true);
            NoneTxt.SetActive(false);
        }
        if (holdLeft == false && holdRight == false || holdLeft == true && holdRight == true) // none text
        {
            RightTxt.SetActive(false);
            LeftTxt.SetActive(false);
            NoneTxt.SetActive(true);
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
                            force += BarMultiplier;
                            powerBar.SetSize(force / 100); //update power bar
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
        yield return goLeft = true;
        yield return goRight = false;
        yield return holdLeft = true;
        rb.drag = 2;
    }

    private IEnumerator OnMoveRight(InputValue value) // right trigger hold
    {
        yield return goRight = true;
        yield return goLeft = false;
        yield return holdRight = true;
        rb.drag = 2;
    }

    private IEnumerator OnMoveLeftRelease(InputValue value) // left trigger release
    {
        yield return holdLeft = false;

        if (goLeft == true)
        {            
            yield return fireLeft = true;
            rb.drag = 0;
        }
    }

    private IEnumerator OnMoveRightRelease(InputValue value) // right trigger release
    {
        yield return holdRight = false;

        if (goRight == true)
        {
            yield return fireRight = true;
            rb.drag = 0;
        }
    }
}
