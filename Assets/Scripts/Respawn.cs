using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform RespawnPosition;

    public GameObject RespawnTxt;

    public int respawnFloor;

    // Start is called before the first frame update
    void Start()
    {
        RespawnTxt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y <= respawnFloor)
        {
            StartCoroutine(respawn());
        }
    }
    IEnumerator respawn ()
    {
        this.transform.position = new Vector2(RespawnPosition.transform.position.x, RespawnPosition.transform.position.y);
        RespawnTxt.SetActive(true);
        GameObject.Find("Player").GetComponent<Ghosting>().usingGhosting = false; // turn off ghosting
        GameObject.Find("Player").GetComponent<Movement>().force = 0f; // reset force
        GameObject.Find("Player").GetComponent<Movement>().rb.velocity = new Vector3(0, 0, 0);// stop
        yield return new WaitForSeconds(0.5f);
        RespawnTxt.SetActive(false);

    }
}
