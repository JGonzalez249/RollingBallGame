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

    public float force;

    public int maxForce;
    public int maxMultiplier; // amount of force gained per circle completion

    public bool goRight;
    public bool goLeft;

    public bool upPress;
    public bool downPress;
    public bool leftPress;
    public bool rightPress;
    public bool fire;

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

        //Spinning to go right - EDIT: both ways now

        //   if (upPress == true)
        //{
        //	if (rightPress == true)
        //	{
        //		if (downPress == true)
        //		{
        //			upPress = false;
        //			rightPress = false;
        //			leftPress = false;
        //			downPress = false;

        //                   if (force < maxForce) // don't go over max force
        //                   {
        //                       force += maxMultiplier;
        //                       powerBar.SetSize(force / 100); //update power bar
        //                   }

        //               }
        //	}
        //}

        //Spinning to go left - EDIT: both ways now

        if (upPress == true)
        {
            if (leftPress == true)
            {
                if (downPress == true)
                {

                    upPress = false;
                    leftPress = false;
                    rightPress = false;
                    downPress = false;

                    if (force < maxForce) // don't go over max force
                    {
                        force += maxMultiplier;
                        powerBar.SetSize(force / 100); //update power bar
                    }
                }
            }
        }



        //press fire button to launch stored energy

        if (goRight == true)
        {
            if(fire == true)
            {
                rb.AddForce(transform.right * Time.deltaTime * force * 20, ForceMode.Impulse);
                force = 0;
            }
            
        }
        if (goRight == false)
        {
            if(fire == true)
            {
                rb.AddForce(transform.right * Time.deltaTime * force * -20, ForceMode.Impulse);
                force = 0;
            }
            
        }

        if (force > maxForce)
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

	//private IEnumerator OnLaunch(InputValue value) // fire button
	//{
	//	yield return fire = true;
	//}

	
	private IEnumerator OnMoveLeft(InputValue value)
	{
		yield return goLeft = true;
        yield return goRight = false;

        if(Gamepad.current.leftTrigger.wasReleasedThisFrame)
        {
            yield return fire = true; 
        }
	}

	
	private IEnumerator OnMoveRight(InputValue value)
	{
		yield return goRight = true;
        yield return goLeft = false;

        if (Gamepad.current.rightTrigger.wasReleasedThisFrame)
        {
            yield return fire = true;
        }
    }
}
