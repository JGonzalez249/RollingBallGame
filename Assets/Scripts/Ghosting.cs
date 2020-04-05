using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Ghosting : MonoBehaviour
{
    private GameObject[] ghostableWall;

    [SerializeField] public GhostBar ghostBar;

    public float div = 50; // ghost time division -- higher = more time

    public Text GhostText;

    public bool usingGhosting;

    private bool startCooldown;
    private bool ready = true;

    private void Start()
    {
        ghostBar.SetSize(1.0f); // set ghost bar to 1
        ghostableWall = GameObject.FindGameObjectsWithTag("Ghostable");//what is ghostable wall?
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Death" && usingGhosting == false) // if in wall and not ghosting
        {
            GameObject.Find("Player").GetComponent<Respawn>().death = true;
        }
    }

    private IEnumerator OnGhosting(InputValue value) // press ghosting
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            
            if (usingGhosting == false && ready == true)
            {
                int i = 1;

                yield return usingGhosting = true;

                foreach (GameObject go in ghostableWall) // for every ghostable wall
                {
                    go.SetActive(false);
                }

                yield return GhostText.text = "Ghosting";

                while (i <= div) // repeat by division count
                {
                    ghostBar.SetSize(1 - (i / div)); // subtract division relative to time from ghost time
                    yield return new WaitForSeconds (0.02f);
                    yield return i++;
                }
                
                foreach (GameObject go in ghostableWall) // for every ghostable wall
                {
                    go.SetActive(true);
                }

                yield return startCooldown = true; // start cooldown
                yield return usingGhosting = false;
            }

            if (startCooldown == true)
            {
                int x = 1;

                yield return startCooldown = false;
                yield return ready = false;

                yield return GhostText.text = "Cooldown";

                while (x <= div)
                {
                    ghostBar.SetSize(x / div); // subtract division relative to time from ghost time
                    yield return new WaitForSeconds(0.02f);
                    yield return x++;
                }

                yield return ready = true;
                yield return GhostText.text = "";
            }
        }
    }
}