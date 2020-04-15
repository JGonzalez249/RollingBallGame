using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLevel : MonoBehaviour
{
    public Rigidbody prb;
    public GameObject lvl;
    public Vector3 rotatePoint;
    private GameObject[] Wall1;
    private GameObject[] Wall2;

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
        {
            print("Cannot Find Level");
        }

        Wall1 = GameObject.FindGameObjectsWithTag("Wall1");//what is ghostable wall?
        if (GameObject.FindGameObjectsWithTag("Wall1") == null) // if cannot find any ghostable walls in level
        {
            print("Cannot Find Wall1");
        }

        Wall2 = GameObject.FindGameObjectsWithTag("Wall2");//what is ghostable wall?
        if (GameObject.FindGameObjectsWithTag("Wall2") == null) // if cannot find any ghostable walls in level
        {
            print("Cannot Find Wall2");
        }

        foreach (GameObject go in Wall1) // for every front-rotated front wall
        {
            go.SetActive(false);
        }
        foreach (GameObject go in Wall2) // for every side-rotated front wall
        {
            go.SetActive(true);
        }
    }
    private void Update()
    {

        if (isRotatingLeft == false && isRotatingRight == false)
        {
            lvl.transform.rotation = Quaternion.Euler(0, currentYRot, 0);
        }
        if (isRotatingRight == true)
        {
            this.transform.position = new Vector3(rotatePoint.x, this.transform.position.y, 0);
            lvl.transform.RotateAround(prb.transform.position, Vector3.up, 172.5f * Time.deltaTime); // 90 degrees
            currentYRot = lvl.transform.rotation.eulerAngles.y;

        }
        else if (isRotatingLeft == true)
        {
            this.transform.position = new Vector3(Mathf.Round(rotatePoint.x), this.transform.position.y, 0);
            lvl.transform.RotateAround(prb.transform.position, Vector3.down, 172.5f * Time.deltaTime);// 90 degrees
            currentYRot = lvl.transform.rotation.eulerAngles.y;

        }

        //rounding
        if (currentYRot > -10 && currentYRot < 10)
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

        //walls
        if (currentYRot > -45 && currentYRot < 45 || currentYRot > 135 && currentYRot < 225 || currentYRot < -135 && currentYRot > -225)
        {
            foreach (GameObject go in Wall1) // for every front-rotated front wall
            {
                go.SetActive(false);
            }
            foreach (GameObject go in Wall2) // for every side-rotated front wall
            {
                go.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject go in Wall1) // for every front-rotated front wall
            {
                go.SetActive(true);
            }
            foreach (GameObject go in Wall2) // for every side-rotated front wall
            {
                go.SetActive(false);
            }
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
        prb.AddForce(-5, 0, 0, ForceMode.Impulse);
        cooldown = true;
        yield return new WaitForSeconds(0.1f);
        cooldown = false;
    }
}
