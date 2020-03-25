using UnityEngine;

public class PowerBar : MonoBehaviour
{
    public Transform bar;

    // Start is called before the first frame update
    private void Start()
    {
        bar = transform.Find("Bar");
    }

    public void SetSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}