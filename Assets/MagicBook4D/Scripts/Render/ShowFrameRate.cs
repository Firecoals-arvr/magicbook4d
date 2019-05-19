using UnityEngine;

public class ShowFrameRate : MonoBehaviour
{
    private float deltaTime = 0.0f;

    private void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;
        GetComponent<UILabel>().text = msec + " \n" + fps + " fps";
    }

}
