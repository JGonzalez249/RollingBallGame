using UnityEngine;

public class PowerBar : MonoBehaviour
{
    public Transform powerBar;

    // Start is called before the first frame update
    private void Start()
    {
        powerBar = transform.Find("PowerBar");
    }

    public void SetSize(float sizeNormalized)
    {
        powerBar.localScale = new Vector3(sizeNormalized, 1f);
    }
}