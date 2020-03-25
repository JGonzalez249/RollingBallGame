using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;

    private void LateUpdate()
    {
        this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 2, this.transform.position.z);
    }
}