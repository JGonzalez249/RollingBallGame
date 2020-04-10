using System.Collections;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject RespawnPosition;

    public GameObject RespawnTxt;

    public int respawnFloor;

    public bool death;

    // Start is called before the first frame update
    private void Start()
    {
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
        //rotation
        GameObject.FindGameObjectWithTag("Player").GetComponent<RotateLevel>().currentYRot = 0;
        GameObject.FindGameObjectWithTag("Player").GetComponent<RotateLevel>().lvl.transform.position =new Vector3(0, 0, 0);

        death = false;

        //move to respawn position
        this.transform.position = new Vector2(RespawnPosition.transform.position.x, RespawnPosition.transform.position.y);

        RespawnTxt.SetActive(true);
        GameObject.Find("Player").GetComponent<Ghosting>().usingGhosting = false; // turn off ghosting
        GameObject.Find("Player").GetComponent<Movement>().force = 0f; // reset force
        GameObject.Find("Player").GetComponent<Movement>().rb.velocity = new Vector3(0, 0, 0);// stop
        GameObject.Find("Player").GetComponent<GrapplingHook>().ReturnHook(); // return hook
        GameObject.Find("Player").GetComponent<Movement>().spinForce = 0; // stop spinning
        yield return new WaitForSeconds(0.5f);
        RespawnTxt.SetActive(false);
    }
}