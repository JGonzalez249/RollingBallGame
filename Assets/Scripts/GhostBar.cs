using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBar : MonoBehaviour
{
    public Transform ghostBar;

    // Start is called before the first frame update
    private void Start()
    {
        ghostBar = transform.Find("GhostBar");
    }

    public void SetSize(float sizeNormalized)
    {
        ghostBar.localScale = new Vector3(sizeNormalized, 1f);
    }
}
