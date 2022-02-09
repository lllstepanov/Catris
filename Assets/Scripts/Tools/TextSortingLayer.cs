using UnityEngine;

/// <summary>
/// Helps TextMesh draw according game sorting layers
/// </summary>

[ExecuteInEditMode]
[RequireComponent(typeof(TextMesh))]
public class TextSortingLayer : MonoBehaviour
{
    /// <summary>
    /// Name of the Sorting Layer
    /// </summary>
    [SerializeField]
    private string sortingLayerName = "Default";

    /// <summary>
    /// Order in the Sorting Layer
    /// </summary>
    public int orderInLayer = 0;

    /// <summary>
    /// MeshRenderer object
    /// </summary>
    private MeshRenderer meshRenderer;

    private void Start()
    {
        /// Assing meshRenderer object
        meshRenderer = GetComponent<MeshRenderer>();

        /// Sets sorting layer of meshRenderer
        meshRenderer.sortingLayerName = sortingLayerName;
        
        /// Sets order in the layer of the meshRenderer
        meshRenderer.sortingOrder = orderInLayer;
    }
}