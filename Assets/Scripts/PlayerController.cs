﻿using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Collider m_Collider;

    // Start is called before the first frame update
    private void Start()
    {
    }

    private IEnumerator Ghosting()
    {
        if (m_Collider.enabled == true)
        {
            m_Collider.enabled = !m_Collider.enabled;
            yield return new WaitForSeconds(1);
            m_Collider.enabled = !m_Collider.enabled;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            StartCoroutine(Ghosting());
        }
    }
}