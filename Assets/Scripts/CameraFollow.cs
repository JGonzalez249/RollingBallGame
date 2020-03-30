using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public int offset;
    public int maxOffset;
    public float rotation;
    public float maxRotation;
    public bool lookRight;
    public bool lookLeft;

    private void LateUpdate()
    {
        this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 5, this.transform.position.z);

        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    private IEnumerator OnMoveLeft(InputValue value) // left trigger hold
    {
        yield return lookRight = false;
        yield return lookLeft = true;
        yield return maxRotation = -10;
        yield return maxOffset = -10;
        if (lookRight == false && lookLeft == true)
        {
            while (rotation > maxRotation)
            {
                yield return rotation--;
            }
        }

  
    }

    private IEnumerator OnMoveRight(InputValue value) // right trigger hold
    {
        yield return lookRight = true;
        yield return lookLeft = false;
        yield return maxRotation = 10;
        yield return maxOffset = 10;
        
        if (lookRight == true && lookLeft == false)
        {
            while (rotation < maxRotation)
            {
                yield return rotation++;
            }
        }


    }

/*    private IEnumerator OnMoveLeftRelease(InputValue value) // left trigger release
    {
        yield return MaxRotation = 0;
        lookRight = false;
        lookLeft = false;
    }

    private IEnumerator OnMoveRightRelease(InputValue value) // right trigger release
    {
        yield return maxRotation = 0;
        lookRight = false;
        lookLeft = false;
    }*/
}