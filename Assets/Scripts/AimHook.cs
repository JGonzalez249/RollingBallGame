using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimHook : MonoBehaviour
{
    public Transform aimMarker;
    public GameObject player;

    private int rotationpos;
    private float hoz;
    private float vert;

    private void Start()
    {
        hoz = 0;
        vert = 3;
        rotationpos = 0;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimUp = true;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimRight = false;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimLeft = false;
    }

    private void FixedUpdate()
    {
       aimMarker.transform.position = new Vector3(player.transform.position.x + hoz, player.transform.position.y + vert, this.transform.position.z);
       aimMarker.transform.rotation = Quaternion.Euler(0, 0, rotationpos);
    }


    private IEnumerator OnAimUp(InputValue value) // up
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            yield return hoz = 0;
            yield return vert = 3;
            yield return rotationpos = 0;
            GameObject.Find("Player").GetComponent<GrapplingHook>().aimUp = true;
            GameObject.Find("Player").GetComponent<GrapplingHook>().aimRight = false;
            GameObject.Find("Player").GetComponent<GrapplingHook>().aimLeft = false;
        }
    }

    private IEnumerator OnAimRight(InputValue value) // right
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            yield return hoz = 2;
            yield return vert = 2;
            yield return rotationpos = -45;
            GameObject.Find("Player").GetComponent<GrapplingHook>().aimUp = false;
            GameObject.Find("Player").GetComponent<GrapplingHook>().aimRight = true;
            GameObject.Find("Player").GetComponent<GrapplingHook>().aimLeft = false;
        }
    }

    private IEnumerator OnAimLeft(InputValue value) // left
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            yield return hoz = -2;
            yield return vert = 2;
            yield return rotationpos = 45;
            GameObject.Find("Player").GetComponent<GrapplingHook>().aimUp = false;
            GameObject.Find("Player").GetComponent<GrapplingHook>().aimRight = false;
            GameObject.Find("Player").GetComponent<GrapplingHook>().aimLeft = true;
        }
    }
}
