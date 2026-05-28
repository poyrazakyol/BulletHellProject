using UnityEngine;
using System.Collections.Generic;

public class LightningTarget : MonoBehaviour
{
    public float duration = 1.5f;
    
    private Renderer[] renderers;
    private Dictionary<Material, Color> originalColors = new Dictionary<Material, Color>();
    private float timer;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            foreach (Material m in r.materials)
            {
                if (m.HasProperty("_Color"))
                {
                    if (!originalColors.ContainsKey(m))
                        originalColors[m] = m.color;
                }
                else if (m.HasProperty("_BaseColor"))
                {
                    if (!originalColors.ContainsKey(m))
                        originalColors[m] = m.GetColor("_BaseColor");
                }
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / duration);

        foreach (Renderer r in renderers)
        {
            if (r == null) continue;
            foreach (Material m in r.materials)
            {
                if (originalColors.TryGetValue(m, out Color origColor))
                {
                    // Hedeften yavaşça kırmızıya geçiş
                    Color lerpedColor = Color.Lerp(origColor, Color.red, t);
                    
                    if (m.HasProperty("_Color")) 
                        m.color = lerpedColor;
                    else if (m.HasProperty("_BaseColor")) 
                        m.SetColor("_BaseColor", lerpedColor);
                }
            }
        }
    }

    void OnDestroy()
    {
        if (renderers == null) return;
        
        foreach (Renderer r in renderers)
        {
            if (r == null) continue;
            foreach (Material m in r.materials)
            {
                if (originalColors.TryGetValue(m, out Color origColor))
                {
                    if (m.HasProperty("_Color")) 
                        m.color = origColor;
                    else if (m.HasProperty("_BaseColor")) 
                        m.SetColor("_BaseColor", origColor);
                }
            }
        }
    }
}
