using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	
	Collider m_Collider;

    // Start is called before the first frame update
    void Start()
    {
        m_Collider = GetComponent<Collider>();
    }


IEnumerator Ghosting(){
if (m_Collider.enabled == true){
		m_Collider.enabled = !m_Collider.enabled;
		yield return new WaitForSeconds(1);
		m_Collider.enabled = !m_Collider.enabled;
        }
}


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
	StartCoroutine(Ghosting());
	}

    }
}
