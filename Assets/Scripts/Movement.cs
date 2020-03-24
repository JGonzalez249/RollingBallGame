using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public GameObject RightTxt;
    public GameObject LeftTxt;

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
    private bool hold;

    private float force;

    private int maxForce = 100;

    // Start is called before the first frame update
    void Start()
    {
        goRight = true;
        powerBar.SetSize(0.0f);

        rb = GetComponent<Rigidbody>();
    }





    // Update is called once per frame
    void Update()
    {
        powerBar.SetSize(force / 100); //update power bar

        //right-left text
        if (goRight == true)
        {
            RightTxt.SetActive(true);
            LeftTxt.SetActive(false);
        }
        else
        {
            RightTxt.SetActive(false);
            LeftTxt.SetActive(true);
        }

        //Movement
        if (hold == true) // holding trigger
            {
            if (upPress == true)
            {
                if (leftPress == true || rightPress == true)
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
        if (goRight == true)
        {
            if (fireRight == true)
            {
                rb.AddForce(transform.right * Time.deltaTime * force * speedMultiplier, ForceMode.Impulse);
                force = 0;
                fireRight = false;
                fireLeft = false; // dont fire left
            }

        }
        if (goRight == false)
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
    private IEnumerator OnUp(InputValue value)
    {
        yield return upPress = true;
    }

    private IEnumerator OnRight(InputValue value)
    {
        yield return rightPress = true;
    }

    private IEnumerator OnDown(InputValue value)
    {
        yield return downPress = true;
    }


    private IEnumerator OnLeft(InputValue value)
    {
        yield return leftPress = true;
    }


    private IEnumerator OnMoveLeft(InputValue value)
    {
        yield return goLeft = true;
        yield return goRight = false;
        yield return hold = true;
        rb.drag = 2;
    }


    private IEnumerator OnMoveRight(InputValue value)
    {
        yield return goRight = true;
        yield return goLeft = false;
        yield return hold = true;
        rb.drag = 2;
    }

    private IEnumerator OnMoveRightRelease(InputValue value)
    {
        yield return fireRight = true;
        yield return hold = false;
        rb.drag = 0;
    }

    private IEnumerator OnMoveLeftRelease(InputValue value)
    {
        yield return fireLeft = true;
        yield return hold = false;
        rb.drag = 0;
    }
}
