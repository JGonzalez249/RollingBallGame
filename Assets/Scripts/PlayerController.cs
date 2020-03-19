using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	public Collider m_Collider;
//	public MyPlayerControls controls;

//    public void OnEnable()
//    {
//        if (controls == null)
//        {
//            controls = new MyPlayerControls();
//            controls.gameplay.SetCallbacks(this);
//        }
//        controls.gameplay.Enable();
//    }

//    public void OnDisable()
//    {
//        controls.gameplay.Disable();
//    }

    private IEnumerator OnGhosting(InputValue value)
    {
        if (m_Collider.enabled == true)
        {
            m_Collider.enabled = !m_Collider.enabled;
            yield return new WaitForSeconds(1);
            m_Collider.enabled = !m_Collider.enabled;
        }
    }


}