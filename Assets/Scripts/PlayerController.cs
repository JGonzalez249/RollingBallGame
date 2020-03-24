using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public Collider m_Collider;
	public Text GhostText;

    private IEnumerator OnGhosting(InputValue value)
    {
m_Collider.enabled = !m_Collider.enabled;
        if (m_Collider.enabled == true)
        {
yield return GhostText.text = "";
//            m_Collider.enabled = !m_Collider.enabled;
//            yield return new WaitForSeconds(1);
//            m_Collider.enabled = !m_Collider.enabled;
        }
        if (m_Collider.enabled == false)
        {
yield return GhostText.text = "Ghosting";
        }
    }


}