using System.Collections;
using UnityEngine;

public class EnemyErosionController : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;

    [SerializeField] private string thresholdName = "_Threshold";
    [SerializeField] private float aliveValue = 0.1f;
    [SerializeField] private float deadValue  = 2.0f;
    [SerializeField] private float duration = 2.5f;

    private static int _thresholdId = -1;
    private MaterialPropertyBlock _mpb;
    private bool _isEroding;

    // Initialisation
    private void Awake()
    {
        if (renderers == null || renderers.Length == 0)
            renderers = GetComponentsInChildren<Renderer>(true);

        if (_thresholdId == -1) _thresholdId = Shader.PropertyToID(thresholdName);
        _mpb = new MaterialPropertyBlock();
    }

    // Set alive threshold
    private void OnEnable() => SetThreshold(aliveValue);

    public void TriggerErode()
    {
        if (!_isEroding) StartCoroutine(ErodeRoutine());
    }

    private IEnumerator ErodeRoutine()
    {
        _isEroding = true;

        float t = 0f;
        float start = aliveValue;
        float end = deadValue;

        if (duration <= 0f)
        {
            SetThreshold(end);
        }
        else
        {
            while (t < duration)
            {
                t += Time.deltaTime;
                float u = Mathf.Clamp01(t / duration);
                SetThreshold(Mathf.Lerp(start, end, u));
                yield return null;
            }
            SetThreshold(end);
        }

        _isEroding = false;
    }

    private void SetThreshold(float value)
    {
        foreach (var r in renderers)
        {
            if (!r) continue;
            r.GetPropertyBlock(_mpb);
            _mpb.SetFloat(_thresholdId, value);
            r.SetPropertyBlock(_mpb);
        }
    }
}
