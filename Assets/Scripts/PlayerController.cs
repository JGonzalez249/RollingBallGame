using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
<<<<<<< HEAD
    private Collider m_Collider;

=======
>>>>>>> master
    // Start is called before the first frame update
    private void Start()
    {
        
    }

<<<<<<< HEAD
    private IEnumerator Ghosting()
    {
        if (m_Collider.enabled == true)
        {
            m_Collider.enabled = !m_Collider.enabled;
            yield return new WaitForSeconds(1);
            m_Collider.enabled = !m_Collider.enabled;
        }
    }

=======
>>>>>>> master
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
<<<<<<< HEAD
            StartCoroutine(Ghosting());
=======
		
>>>>>>> master
        }
    }
}