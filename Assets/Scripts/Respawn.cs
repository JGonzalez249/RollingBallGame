using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Respawn : MonoBehaviour
{
    public GameObject RespawnPosition;
    public GameObject RespawnTxt;

    public int respawnFloor;

    public bool death;

    // Start is called before the first frame update
    private void Start()
    {
        RespawnTxt = GameObject.FindWithTag("RespawnTxt");
        death = false;
        RespawnTxt.SetActive(false);
        RespawnPosition = GameObject.FindGameObjectWithTag("Respawn");
    }

    // Update is called once per frame
    private void Update()
    {
        if (this.transform.position.y <= respawnFloor || death == true)
        {
            StartCoroutine(respawn());
        }
    }

    private IEnumerator respawn()
    {
        this.transform.position = RespawnPosition.transform.position;
        death = false;

        //move to respawn position
        RespawnTxt.SetActive(true);
        GameObject.Find("Player").GetComponent<Ghosting>().usingGhosting = false; // turn off ghosting
        GameObject.Find("Player").GetComponent<Movement>().force = 0f; // reset force
        GameObject.Find("Player").GetComponent<Movement>().rb.velocity = new Vector3(0, 0, 0);// stop
        GameObject.Find("Player").GetComponent<GrapplingHook>().ReturnHook(); // return hook
        GameObject.Find("Player").GetComponent<Movement>().spinForce = 0; // stop spinning
        yield return new WaitForSeconds(1f);
        RespawnTxt.SetActive(false);
    }
}