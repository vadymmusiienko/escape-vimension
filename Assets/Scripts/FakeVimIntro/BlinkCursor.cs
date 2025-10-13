using UnityEngine;
using UnityEngine.UI;

public class CaretBlinker : MonoBehaviour
{
    public Graphic caretGraphic;
    public float period = 0.5f;
    float t;

    void Reset() { caretGraphic = GetComponent<Graphic>(); }

    void Update()
    {
        if (!caretGraphic) return;
        t += Time.unscaledDeltaTime;
        if (t >= period)
        {
            caretGraphic.enabled = !caretGraphic.enabled;
            t = 0f;
        }
    }
}

