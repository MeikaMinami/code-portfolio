using UnityEngine;
using UnityEngine.UI;

public class FlashScript : MonoBehaviour
{
    [SerializeField] Image flash;
    public float flashingTime = 0.0f;
    public float flashingVol = 0.0f;
    private void Update()
    {
        flash.color = new Color(1.0f, 1.0f, 1.0f, flashingVol);
        if (flashingVol < 1.0f && flashingTime == 0.0f)
        {
            flashingVol += 1f;
        }
        else if (flashingVol >= 1.0f && flashingTime < 1.0f)
        {
            flashingTime += Time.deltaTime;
        }
        else if (flashingTime >= 1.0f)
        {
            flashingVol -= 0.05f;
        }
        if (flashingTime >= 1.0f && flashingVol < 0.0f)
        {
            flashingVol = 0.0f;
            flashingTime = 0.0f;
            gameObject.SetActive(false);
        }
    }
}
