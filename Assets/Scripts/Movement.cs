using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    }

    void OnMovement(InputValue value)
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
            return; // No gamepad connected.

        //Building charge
        if (gamepad.leftTrigger.wasPressedThisFrame)
        {
            Debug.Log("is pressed");
            StartCoroutine(Charge());
        }
        //release charge
        if (gamepad.leftTrigger.wasReleasedThisFrame)
        {
            rb.AddForce(transform.right * Time.deltaTime * force * 10, ForceMode.Impulse);
            StopCoroutine(Charge());
            force = 0;
            Debug.Log("isReleased");


        }
        if (force > maxForce)
        {
            force = maxForce;
        }
    }
    IEnumerator Charge()
    {
        while (force < maxForce && Gamepad.current.leftTrigger.isPressed) // while force is less than max
        {
            yield return new WaitForSeconds(0.1f);
            force++;
            Debug.Log(force);
        }
    }
}
