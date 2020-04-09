using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBarAnimate : MonoBehaviour
{
    Animator powerAnim;

    // Start is called before the first frame update
    void Start()
    {
        powerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<Movement>().force == 0)
        {
            powerAnim.SetInteger("GetPower", 0);
        }
        if (GameObject.Find("Player").GetComponent<Movement>().force >= 24 && GameObject.Find("Player").GetComponent<Movement>().force < 49)
        {
            powerAnim.SetInteger("GetPower", 1);
        }
        if (GameObject.Find("Player").GetComponent<Movement>().force >= 49 && GameObject.Find("Player").GetComponent<Movement>().force < 74)
        {
            powerAnim.SetInteger("GetPower", 2);
        }
        if (GameObject.Find("Player").GetComponent<Movement>().force >= 74 && GameObject.Find("Player").GetComponent<Movement>().force < 99)
        {
            powerAnim.SetInteger("GetPower", 3);
        }
        if (GameObject.Find("Player").GetComponent<Movement>().force > 99)
        {
            powerAnim.SetInteger("GetPower", 4);
        }
    }
}
