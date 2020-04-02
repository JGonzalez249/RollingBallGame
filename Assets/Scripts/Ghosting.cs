using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Ghosting : MonoBehaviour
{
    private GameObject[] ghostableWall;

    public Text GhostText;

    public bool usingGhosting;

    private void Start()
    {
        ghostableWall = GameObject.FindGameObjectsWithTag("Ghostable");//what is ghostable wall?
    }
    private void Update()
    {
        if (usingGhosting == true)
        {
            foreach (GameObject go in ghostableWall) // for every ghostable wall
            {
                go.SetActive(false);
            }
            GhostText.text = "Ghosting";
        }

        if (usingGhosting == false)
        {
            foreach (GameObject go in ghostableWall) // for every ghostable wall
            {
                go.SetActive(true);
            }
            GhostText.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Death" && usingGhosting == false) // if in wall and not ghosting
        {
            GameObject.Find("Player").GetComponent<Respawn>().death = true;
        }
    }

    private IEnumerator OnGhosting(InputValue value) // press ghosting
    {
        if (GameObject.Find("EventSystem").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            yield return usingGhosting = !usingGhosting;
        }
    }
}