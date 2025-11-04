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
    
    [Tooltip("Speed at which alpha changes")]
    [SerializeField] private float fadeSpeed = 5f;
    
    [Tooltip("Offset from player position for raycast target (to avoid hitting ground)")]
    [SerializeField] private Vector3 playerOffset = new Vector3(0, 0.5f, 0);
    
    // Store original materials and current alpha values
    private Dictionary<Renderer, MaterialData> affectedRenderers = new Dictionary<Renderer, MaterialData>();
    private HashSet<Renderer> currentOccludingWalls = new HashSet<Renderer>();
    
    private class MaterialData
    {
        public Material[] originalMaterials;
        public Material[] transparentMaterials;
        public float currentAlpha;
        public float targetAlpha;
        
        public MaterialData(Renderer renderer)
        {
            originalMaterials = new Material[renderer.materials.Length];
            transparentMaterials = new Material[renderer.materials.Length];
            
            for (int i = 0; i < renderer.materials.Length; i++)
            {
                originalMaterials[i] = renderer.materials[i];
                // Create a copy for transparency manipulation
                transparentMaterials[i] = new Material(originalMaterials[i]);
                transparentMaterials[i].SetFloat("_Mode", 3); // Fade mode
                transparentMaterials[i].SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                transparentMaterials[i].SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                transparentMaterials[i].SetInt("_ZWrite", 0);
                transparentMaterials[i].DisableKeyword("_ALPHATEST_ON");
                transparentMaterials[i].EnableKeyword("_ALPHABLEND_ON");
                transparentMaterials[i].DisableKeyword("_ALPHAPREMULTIPLY_ON");
                transparentMaterials[i].renderQueue = 3000;
            }
        }
    }
    
    private void Awake()
    {
        // Auto-find references if not assigned
        if (player == null)
        {
            Player playerObj = FindFirstObjectByType<Player>();
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
        
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                mainCamera = FindFirstObjectByType<Camera>();
            }
        }
    }
    
    private void Update()
    {
        if (player == null || mainCamera == null)
        {
            return;
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
        direction.Normalize();
        
        RaycastHit[] hits = Physics.RaycastAll(from, direction, distance, occlusionLayers);
        
        foreach (RaycastHit hit in hits)
        {
            Renderer renderer = hit.collider.GetComponent<Renderer>();
            if (renderer != null && renderer.enabled)
            {
                currentOccludingWalls.Add(renderer);
            }
        }
    }
    
    private void UpdateRendererTransparency()
    {
        // Update renderers that are currently occluding
        foreach (Renderer renderer in currentOccludingWalls)
        {
            if (!affectedRenderers.ContainsKey(renderer))
            {
                // First time we see this renderer - create material data
                affectedRenderers[renderer] = new MaterialData(renderer);
            }
            
            MaterialData data = affectedRenderers[renderer];
            data.targetAlpha = targetAlpha;
            
            // Apply transparent materials if not already applied
            if (renderer.materials[0] != data.transparentMaterials[0])
            {
                renderer.materials = data.transparentMaterials;
            }
        }
        
        // Update alpha values for all tracked renderers
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
            
            // Set target alpha based on whether it's currently occluding
            if (currentOccludingWalls.Contains(renderer))
            {
                data.targetAlpha = targetAlpha;
            }
            else
            {
                data.targetAlpha = 1f; // Fully opaque when not occluding
            }
            
            // Smoothly interpolate current alpha to target
            data.currentAlpha = Mathf.Lerp(data.currentAlpha, data.targetAlpha, fadeSpeed * Time.deltaTime);
            
            // Apply alpha to all materials
            foreach (Material mat in data.transparentMaterials)
            {
                if (mat != null)
                {
                    Color color = mat.color;
                    color.a = data.currentAlpha;
                    mat.color = color;
                }
            }
            
            // If alpha is close to 1 and not occluding, restore original materials
            if (data.currentAlpha > 0.99f && !currentOccludingWalls.Contains(renderer))
            {
                renderer.materials = data.originalMaterials;
                // Optionally remove from dictionary to save memory, but keep for reuse
            }
        }
        
        // Clean up null renderers
        foreach (Renderer renderer in renderersToRemove)
        {
            affectedRenderers.Remove(renderer);
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

