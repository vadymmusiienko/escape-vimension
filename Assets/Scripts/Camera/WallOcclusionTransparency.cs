using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Makes walls transparent when they block the view between the camera and player.
/// Attach this script to the camera or a GameObject in the scene.
/// </summary>
public class WallOcclusionTransparency : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;
    [SerializeField] private Camera mainCamera;
    
    [Header("Settings")]
    [Tooltip("Layers that should become transparent (e.g., walls)")]
    [SerializeField] private LayerMask occlusionLayers = -1;
    
    [Tooltip("Target alpha when wall is blocking view (0 = fully transparent, 1 = fully opaque)")]
    [Range(0f, 1f)]
    [SerializeField] private float targetAlpha = 0.3f;
    
    [Tooltip("Offset from player position for raycast target (to avoid hitting ground)")]
    [SerializeField] private Vector3 playerOffset = new Vector3(0, 0.5f, 0);
    
    [Header("Performance")]
    [Tooltip("Update every N frames (0 = every frame, 1 = every 2 frames, etc.)")]
    [SerializeField] private int updateSkipFrames = 0;
    
    // Store original materials and transparency state
    private Dictionary<Renderer, MaterialData> affectedRenderers = new Dictionary<Renderer, MaterialData>();
    private HashSet<Renderer> currentOccludingWalls = new HashSet<Renderer>();
    private int frameCount = 0;
    
    // Cached property IDs for better performance
    private static readonly int ModePropertyID = Shader.PropertyToID("_Mode");
    private static readonly int SrcBlendPropertyID = Shader.PropertyToID("_SrcBlend");
    private static readonly int DstBlendPropertyID = Shader.PropertyToID("_DstBlend");
    private static readonly int ZWritePropertyID = Shader.PropertyToID("_ZWrite");
    
    private class MaterialData
    {
        public Material[] originalMaterials;
        public Material[] transparentMaterials;
        public bool isTransparent;
        
        public MaterialData(Renderer renderer)
        {
            originalMaterials = new Material[renderer.materials.Length];
            transparentMaterials = new Material[renderer.materials.Length];
            
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                originalMaterials[i] = renderer.materials[i];
                // Create a copy for transparency manipulation
                transparentMaterials[i] = new Material(originalMaterials[i]);
                transparentMaterials[i].SetFloat(ModePropertyID, 3); // Fade mode
                transparentMaterials[i].SetInt(SrcBlendPropertyID, (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                transparentMaterials[i].SetInt(DstBlendPropertyID, (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                transparentMaterials[i].SetInt(ZWritePropertyID, 0);
                transparentMaterials[i].DisableKeyword("_ALPHATEST_ON");
                transparentMaterials[i].EnableKeyword("_ALPHABLEND_ON");
                transparentMaterials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                transparentMaterials[i].renderQueue = 3000;
            }
        }
    }
    
    
    private void Update()
    {
        if (player == null || mainCamera == null)
        {
            return;
        }
        
        // Skip frames for performance
        if (updateSkipFrames > 0)
        {
            frameCount++;
            if (frameCount % (updateSkipFrames + 1) != 0)
            {
                return;
            }
        }
        
        // Clear current occluding walls
        currentOccludingWalls.Clear();
        
        // Perform raycast from camera to player
        Vector3 cameraPos = mainCamera.transform.position;
        Vector3 playerPos = player.position + playerOffset;
        PerformRaycast(cameraPos, playerPos);
        
        // Update all affected renderers
        UpdateRendererTransparency();
    }
    
    private void PerformRaycast(Vector3 from, Vector3 to)
    {
        Vector3 direction = to - from;
        float distance = direction.magnitude;
        
        if (distance < 0.001f) return; // Too close, skip
        
        direction /= distance; // Normalize by division (faster than Normalize())
        
        RaycastHit[] hits = Physics.RaycastAll(from, direction, distance, occlusionLayers);
        
        // Pre-allocate HashSet capacity if we know approximate count
        for (int i = 0; i < hits.Length; i++)
        {
            Renderer renderer = hits[i].collider.GetComponent<Renderer>();
            if (renderer != null && renderer.enabled)
            {
                currentOccludingWalls.Add(renderer);
            }
        }
    }
    
    private void UpdateRendererTransparency()
    {
        // Update renderers that are currently occluding - make them transparent
        foreach (Renderer renderer in currentOccludingWalls)
        {
            if (!affectedRenderers.ContainsKey(renderer))
            {
                // First time we see this renderer - create material data
                affectedRenderers[renderer] = new MaterialData(renderer);
            }
            
            MaterialData data = affectedRenderers[renderer];
            
            // Only update if not already transparent
            if (!data.isTransparent)
            {
                // Apply transparent materials
                renderer.materials = data.transparentMaterials;
                
                // Set alpha instantly
                for (int i = 0; i < data.transparentMaterials.Length; i++)
                {
                    Material mat = data.transparentMaterials[i];
                    if (mat != null)
                    {
                        Color color = mat.color;
                        color.a = targetAlpha;
                        mat.color = color;
                    }
                }
                
                data.isTransparent = true;
            }
        }
        
        // Restore renderers that are no longer occluding
        List<Renderer> renderersToRemove = new List<Renderer>();
        
        foreach (var kvp in affectedRenderers)
        {
            Renderer renderer = kvp.Key;
            MaterialData data = kvp.Value;
            
            if (renderer == null)
            {
                renderersToRemove.Add(renderer);
                continue;
            }
            
            // If not occluding and currently transparent, restore to opaque
            if (!currentOccludingWalls.Contains(renderer) && data.isTransparent)
            {
                // Restore original materials instantly
                renderer.materials = data.originalMaterials;
                data.isTransparent = false;
            }
        }
        
        // Clean up null renderers
        if (renderersToRemove.Count > 0)
        {
            foreach (Renderer renderer in renderersToRemove)
            {
                affectedRenderers.Remove(renderer);
            }
        }
    }
    
    private void OnDestroy()
    {
        // Restore all original materials
        foreach (var kvp in affectedRenderers)
        {
            if (kvp.Key != null)
            {
                kvp.Key.materials = kvp.Value.originalMaterials;
            }
        }
        affectedRenderers.Clear();
    }
}

