using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int force;
    public int maxForce;

    public bool goRight;

    public Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //break direction
        if (Input.GetKey(KeyCode.A))
        {
            goRight = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            goRight = true;
        }

        //build up charge
        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Charge());
        }

        //release charge
        if (Input.GetKeyUp(KeyCode.Space))
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
