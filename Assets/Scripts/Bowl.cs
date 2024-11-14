using UnityEngine;

public class Bowl : MonoBehaviour
{
    public float bowlRadius = 5f;  // The radius of the virtual collider
    public Material glassMaterial; // Reference to the glass material

    private void Start()
    {
        // Ensure the bowl has a mesh renderer with a glass-like material
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null && glassMaterial != null)
        {
            meshRenderer.material = glassMaterial; // Set glass material
        }

        // Add a SphereCollider if not already added
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.radius = bowlRadius;  // Set collider's radius to match virtual size
        collider.isTrigger = true;  // Optionally make it a trigger

        // Scale the visual mesh to match the collider's size (assuming it's a sphere mesh)
        transform.localScale = new Vector3(bowlRadius * 2, bowlRadius * 2, bowlRadius * 2);
    }

    public void CheckMarbleCollisions(Marble marble)
    {
        Vector3 directionToCenter = marble.transform.position - transform.position;
        Vector3 normal = directionToCenter.normalized;
        marble.velocity = Vector3.Reflect(marble.velocity, normal);
        marble.transform.position = transform.position + normal * (bowlRadius - marble.radius);
    }
}
