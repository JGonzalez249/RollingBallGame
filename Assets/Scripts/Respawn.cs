using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Respawn : MonoBehaviour
{
    public GameObject RespawnPosition;

    public Text RespawnTxt;

    public int respawnFloor;

    public bool death;

    private float lvlRotationReset;
    private Vector3 lvlPosReset;

    // Start is called before the first frame update
    private void Start()
    {
        death = false;
        RespawnTxt.text = "";
        RespawnPosition = GameObject.FindGameObjectWithTag("Respawn");

        lvlPosReset = GameObject.FindGameObjectWithTag("Player").GetComponent<RotateLevel>().lvl.transform.position;
        lvlRotationReset = GameObject.FindGameObjectWithTag("Player").GetComponent<RotateLevel>().currentYRot;
    }

    // Update is called once per frame
    private void Update()
    {
        if (this.transform.position.y <= respawnFloor || death == true)
        {
            StartCoroutine(respawn());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Checkpoint")
        {
            StartCoroutine(checkpoint());
        }
    }

    private IEnumerator respawn()
    {
        //rotation
        GameObject.FindGameObjectWithTag("Player").GetComponent<RotateLevel>().currentYRot = lvlRotationReset;
        GameObject.FindGameObjectWithTag("Player").GetComponent<RotateLevel>().lvl.transform.position = lvlPosReset;

        death = false;

        //move to respawn position
        yield return new WaitForSeconds(0.1f);
        this.transform.position = new Vector3(RespawnPosition.transform.position.x, RespawnPosition.transform.position.y, RespawnPosition.transform.position.z);

        RespawnTxt.text = "Respawn";
        GameObject.Find("Player").GetComponent<Ghosting>().usingGhosting = false; // turn off ghosting
        GameObject.Find("Player").GetComponent<Movement>().force = 0f; // reset force
        GameObject.Find("Player").GetComponent<Movement>().rb.velocity = new Vector3(0, 0, 0);// stop
        GameObject.Find("Player").GetComponent<GrapplingHook>().ReturnHook(); // return hook
        GameObject.Find("Player").GetComponent<Movement>().spinForce = 0; // stop spinning
        yield return new WaitForSeconds(0.5f);
        RespawnTxt.text = "";
    }

    private IEnumerator checkpoint()
    {
        lvlRotationReset = GameObject.FindGameObjectWithTag("Player").GetComponent<RotateLevel>().currentYRot;
        lvlPosReset = GameObject.FindGameObjectWithTag("Player").GetComponent<RotateLevel>().lvl.transform.position;
        RespawnTxt.text = "Checkpoint";
        RespawnPosition = GameObject.FindGameObjectWithTag("Checkpoint");
        yield return new WaitForSeconds(1f);
        RespawnTxt.text = "";
    }
}