using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PolygonSetup : MonoBehaviour {

    int level = GameManager.level;

    [Range(3, 12)]
    public int sides = 3;
    
    public float radius = 0.3f;

    public Material polyMat;
    PolygonCollider2D pc2d;

    Vector2[] vertices2D;
    
    void Start ()
    {
        DrawPolygon(sides);
    }

    void DrawPolygon (int sides)
    {
        sides = (sides < 3) ? 3 : sides;
        this.sides = sides;

        SetPolyCoords();

        Triangulator tr = new Triangulator(vertices2D);
        int[] indices = tr.Triangulate();

        Vector3[] vertices = new Vector3[vertices2D.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = new Vector3(vertices2D[i].x, vertices2D[i].y, 0);
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = indices;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // Set up game object with mesh;
        MeshRenderer renderer = gameObject.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
       
        pc2d = gameObject.GetComponent<PolygonCollider2D>();
        pc2d.points = vertices2D;
        
        filter.mesh = mesh;

        renderer.material = polyMat;
        int _index = (sides - 1) % ColorManager.colors.Length;
        renderer.material.color = ColorManager.colors[_index];
    }

    // formula :  https://math.stackexchange.com/q/117172
    void SetPolyCoords ()
    {
        float x;
        float y;
        //float z = 0f;

        vertices2D = new Vector2[sides];

        float angle = 2 * Mathf.PI / sides;
        for (int i = 0; i < sides; i++)
        {
            x = radius * Mathf.Cos(i * angle);
            y = radius * Mathf.Sin(i * angle);

            vertices2D[i] = new Vector2(x, y);
        }
    }

    float GetRadius () 
    {
        if (level >= 3)
            return radius;

        float r = 1f;
        switch (level)
        {
            case 1:
                return r;
            case 2:
                r = .05f;
                break;
        }
        return r;
    }
}
