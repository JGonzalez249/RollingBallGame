using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject CheckpointTxt;

    private void Start()
    {
        //CheckpointTxt = GameObject.FindWithTag("CheckpointTxt");
        CheckpointTxt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("Player").GetComponent<Respawn>().RespawnPosition.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z);
            StartCoroutine(checkpoint());
        }
    }

    private IEnumerator checkpoint()
    {
        CheckpointTxt.SetActive(true);
        yield return new WaitForSeconds(1f);
        CheckpointTxt.SetActive(false);
        Destroy(this);
    }
}
