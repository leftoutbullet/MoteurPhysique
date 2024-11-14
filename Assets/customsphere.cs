using UnityEngine;

public class CustomSphere : MonoBehaviour
{
    public int latitude = 10; // Number of segments around the sphere (latitude)
    public int longitude = 10; // Number of segments along the sphere (longitude)
    public float radius = 1f; // Radius of the sphere

    void Start()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));

        meshFilter.mesh = CreateSphere();
    }

    Mesh CreateSphere()
    {
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[(longitude + 1) * (latitude + 1)];
        int[] triangles = new int[longitude * latitude * 6];

        // Generate vertices
        int vertIndex = 0;
        for (int i = 0; i <= latitude; i++)
        {
            float lat = Mathf.PI * i / latitude;
            for (int j = 0; j <= longitude; j++)
            {
                float lon = 2 * Mathf.PI * j / longitude;
                float x = radius * Mathf.Sin(lat) * Mathf.Cos(lon);
                float y = radius * Mathf.Cos(lat);
                float z = radius * Mathf.Sin(lat) * Mathf.Sin(lon);
                vertices[vertIndex] = new Vector3(x, y, z);
                vertIndex++;
            }
        }

        // Generate triangles
        int triIndex = 0;
        for (int i = 0; i < latitude; i++)
        {
            for (int j = 0; j < longitude; j++)
            {
                int current = i * (longitude + 1) + j;
                int next = current + longitude + 1;

                // First triangle
                triangles[triIndex++] = current;
                triangles[triIndex++] = next;
                triangles[triIndex++] = current + 1;

                // Second triangle
                triangles[triIndex++] = next;
                triangles[triIndex++] = next + 1;
                triangles[triIndex++] = current + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
