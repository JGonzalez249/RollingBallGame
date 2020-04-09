using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLevel : MonoBehaviour
{
    public Rigidbody prb;
    public Transform lvl;

    public float currentRotation;

    public float lvlRotationPos;

    private bool isRotatingRight;
    private bool isRotatingLeft;

    private void Start()
    {
        lvlRotationPos = 0;
        isRotatingRight = false;
        isRotatingLeft = false;
    }
    private void Update()
    {
        currentRotation = lvl.transform.eulerAngles.y;

        if (isRotatingRight == true)
        {
            lvl.transform.Rotate(2, 0, 0);
        }
        if (isRotatingLeft == true)
        {
            lvl.transform.Rotate(-2, 0, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (prb.velocity.x > 0.1f && isRotatingRight == false) // right
        {
            lvlRotationPos = currentRotation + 90;
            if (currentRotation < lvlRotationPos)
            {
                StartCoroutine(RotateRight());
            }
        }
        if (prb.velocity.x < -0.1f && isRotatingLeft == false) // left
        {
            StartCoroutine(RotateLeft());
        }
    }

    private IEnumerator RotateRight()
    {
        isRotatingRight = true;
        yield return new WaitForSeconds(0.75f);
        isRotatingRight = false;
    }

    private IEnumerator RotateLeft()
    {
        isRotatingLeft = true;
        yield return new WaitForSeconds(0.75f);
        isRotatingLeft = false;
    }
}
