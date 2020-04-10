using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLevel : MonoBehaviour
{
    public Rigidbody prb;
    public GameObject lvl;
    public Vector3 rotatePoint;

    private bool isRotatingRight;
    private bool isRotatingLeft;
    private bool cooldown;

    public float currentYRot;

    private void Start()
    {
        isRotatingRight = false;
        isRotatingLeft = false;
        prb = GameObject.Find("Player").GetComponent<Rigidbody>();
        lvl = GameObject.FindGameObjectWithTag("Level");
    }
    private void Update()
    {
        if (isRotatingLeft == false && isRotatingRight == false)
        {
            lvl.transform.rotation = Quaternion.Euler(0, currentYRot, 0);
        }
        if (isRotatingRight == true)
        {
            this.transform.position = new Vector3(rotatePoint.x, this.transform.position.y, rotatePoint.z);
            lvl.transform.RotateAround(prb.transform.position, Vector3.up, 172.5f * Time.deltaTime); // 90 degrees
            currentYRot = lvl.transform.rotation.eulerAngles.y;
            
        }
        else if (isRotatingLeft == true)
        {
            this.transform.position = new Vector3(Mathf.Round(rotatePoint.x), this.transform.position.y, rotatePoint.z);
            lvl.transform.RotateAround(prb.transform.position, Vector3.down, 172.5f * Time.deltaTime);// 90 degrees
            currentYRot = lvl.transform.rotation.eulerAngles.y;
            
        }

        //rounding
        if (currentYRot > -5 && currentYRot < 5)
        {
            currentYRot = 0;
        }
        if (currentYRot > 85 && currentYRot < 95 || currentYRot < -85 && currentYRot > -95)
        {
            currentYRot = 90;
        }
        if (currentYRot > 175 && currentYRot < 185 || currentYRot < -175 && currentYRot > -185)
        {
            currentYRot = 180;
        }
        if (currentYRot > 265 && currentYRot < 275 || currentYRot < -265 && currentYRot > -275)
        {
            currentYRot = 270;
        }
        if (currentYRot > 358 || currentYRot < -358)
        {
            currentYRot = 0;
            lvl.transform.rotation = Quaternion.Euler(0, currentYRot, 90);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Rotate" && cooldown == false)
        {
            rotatePoint = other.transform.position;
            if (prb.velocity.x > 0 && isRotatingRight == false) // right
            {
                StartCoroutine(RotateRight());
            }
            if (prb.velocity.x < 0f && isRotatingLeft == false) // left
            {
                StartCoroutine(RotateLeft());
            }
        }
    }

    private IEnumerator RotateRight()
    {
        isRotatingRight = true;
        yield return new WaitForSeconds(0.5f);
        isRotatingRight = false;
        prb.AddForce(5, 0, 0, ForceMode.Impulse);
        cooldown = true;
        yield return new WaitForSeconds(0.1f);
        cooldown = false;
    }

    private IEnumerator RotateLeft()
    {
        isRotatingLeft = true;
        yield return new WaitForSeconds(0.5f);
        isRotatingLeft = false;
        prb.AddForce(-10, 0, 0, ForceMode.Impulse);
        cooldown = true;
        yield return new WaitForSeconds(0.1f);
        cooldown = false;
    }
}
