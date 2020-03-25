using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Ghosting : MonoBehaviour
{
    public Collider m_Collider;
    public Text GhostText;

    public bool usingGhosting;

    private void Update()
    {
        if (usingGhosting == true)
        {
            m_Collider.enabled = false;
            GhostText.text = "Ghosting";
        }

        if (usingGhosting == false)
        {
            m_Collider.enabled = true;
            GhostText.text = "";
        }
    }

    private IEnumerator OnGhosting(InputValue value) // press ghosting
    {
        yield return usingGhosting = !usingGhosting;
    }
}