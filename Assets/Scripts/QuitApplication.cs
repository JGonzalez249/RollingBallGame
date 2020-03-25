using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class QuitApplication : MonoBehaviour
{
    private bool beGone;

    private void Update()
    {
        if (beGone == true)
        {
            Application.Quit();
            Debug.Log("You have been Exiled! Be Gone! (Quit)");
        }
    }

    private IEnumerator OnQuit(InputValue value)
    {
        yield return beGone = true;
    }
}
