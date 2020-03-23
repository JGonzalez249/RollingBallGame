using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public int force;
    public int maxForce;

    public bool goRight;

    public bool upPress;
    public bool downPress;
    public bool leftPress;
    public bool rightPress;
    public bool fire;

    public Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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

	private IEnumerator OnLaunch(InputValue value)
	{
		yield return fire = true;
	}

	
	private IEnumerator OnLeftDpad(InputValue value)
	{
		yield return goRight = false;
	}

	
	private IEnumerator OnRightDpad(InputValue value)
	{
		yield return goRight = true;
	}


    // Update is called once per frame
    void Update()
    {
//Spinning to go right - EDIT: both ways now

	if (upPress == true)
	{
		if (rightPress == true)
		{
			if (downPress == true)
			{
				//resets speed if you spin the opposite direction
				//if (goRight == false)
				//{
				//force = 0;
				//}

				upPress = false;
				rightPress = false;
				leftPress = false;
				downPress = false;

				//goRight = true;
				//StartCoroutine(Charge());

				force++;
				force++;
				force++;
				force++;
				force++;
			}
		}
	}

//Spinning to go left - EDIT: both ways now

	if (upPress == true)
	{
		if (leftPress == true)
		{
			if (downPress == true)
			{
				//resets speed if you spin the opposite direction
				//if (goRight == true)
				//{
				//force = 0;
				//}

				upPress = false;
				leftPress = false;
				rightPress = false;
				downPress = false;

				//goRight = false;
				//StartCoroutine(Charge());

				force++;
				force++;
				force++;
				force++;
				force++;
			}
		}
	}



//press space to launch stored energy

        if (fire == true)
        {
            if (goRight == true)
            {
                rb.AddForce(transform.right * Time.deltaTime * force * 10, ForceMode.Impulse);
                force = 0;
            }
            if (goRight == false)
            {
                rb.AddForce(transform.right * Time.deltaTime * force * -10, ForceMode.Impulse);
                force = 0;
            }
	fire = false;
        }

        if (force > maxForce)
        {
            force = maxForce;
        }
    }

    IEnumerator Charge()
    {
        if (force < maxForce) // while force is less than max
        {
            yield return new WaitForSeconds(0.5f);
            force++;
        }

    }
}
