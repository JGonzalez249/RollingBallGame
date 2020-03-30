using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimHook : MonoBehaviour
{
    public Transform aimMarker;
    public GameObject player;

    private float hoz;
    private float vert;

    private void Update()
    {
        if (GameObject.Find("EventSystem").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            aimMarker.transform.position = new Vector3(player.transform.position.x + hoz, player.transform.position.y + vert, this.transform.position.z);
        }
    }


    private IEnumerator OnAimUp(InputValue value) // up
    {
        yield return hoz = 0;
        yield return vert = 3;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimUp = true;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimDown = false;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimRight = false;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimLeft = false;
    }

    private IEnumerator OnAimRight(InputValue value) // right
    {
        yield return hoz = 3;
        yield return vert = 3;

        GameObject.Find("Player").GetComponent<GrapplingHook>().aimUp = false;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimDown = false;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimRight = true;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimLeft = false;




    }

    private IEnumerator OnAimDown(InputValue value) // down
    {
        yield return hoz = 0;
        yield return vert = -3;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimUp = false;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimDown = true;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimRight = false;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimLeft = false;
    }

    private IEnumerator OnAimLeft(InputValue value) // left
    {
        yield return hoz = -3;
        yield return vert = 3;

        GameObject.Find("Player").GetComponent<GrapplingHook>().aimUp = false;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimDown = false;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimRight = false;
        GameObject.Find("Player").GetComponent<GrapplingHook>().aimLeft = true;


    }
}
