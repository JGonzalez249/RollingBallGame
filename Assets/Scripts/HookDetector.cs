using UnityEngine;

public class HookDetector : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (player.GetComponent<GrapplingHook>().fired == true)
        {
            if (other.tag == "Hookable")
            {
                player.GetComponent<GrapplingHook>().hooked = true;
                player.GetComponent<GrapplingHook>().hookedObj = other.gameObject;
            }
        }

    }
}