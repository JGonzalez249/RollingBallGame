using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()] // rope appears in edit mode

public class GrappleRender : MonoBehaviour
{
    public Transform[] points;
    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.positionCount = points.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<GrapplingHook>().fired == true)
        {
            lineRenderer.enabled = true;
            for (int i = 0; i< points.Length; ++ i)
            {
                lineRenderer.SetPosition(i, points[i].position);
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }

    }
}
