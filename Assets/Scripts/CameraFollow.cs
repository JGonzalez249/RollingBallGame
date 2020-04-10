using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    private GameObject target;

    public int offset;
    public int maxOffset;
    public float rotation;
    public float maxRotation;
    public bool lookRight;
    public bool lookLeft;

    private void Start()
    {
        target = GameObject.Find("Player");
    }

    private void Update()
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            if (target.GetComponent<Movement>().holdLeft == true && target.GetComponent<Movement>().holdRight == false && lookLeft == false) // look left
            {
                StartCoroutine(LookLeft());
            }
            if (target.GetComponent<Movement>().holdLeft == false && target.GetComponent<Movement>().holdRight == true && lookRight == false) // look right
            {
                StartCoroutine(LookRight());
            }
        }
    }

    private void LateUpdate()
    {
        if (GameObject.Find("Canvas").GetComponent<PauseMenu>().gameIsPaused == false)
        {
            this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 5, target.transform.position.z - 20);

            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    private IEnumerator LookLeft() // left trigger hold
    {
        
            yield return lookRight = false;
            yield return lookLeft = true;
            yield return maxRotation = -10;
            yield return maxOffset = -10;
            if (lookRight == false && lookLeft == true)
            {
                while (rotation > maxRotation)
                {
                    yield return rotation-=0.5f;
                }
            }
    }

    private IEnumerator LookRight() // right trigger hold
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
}