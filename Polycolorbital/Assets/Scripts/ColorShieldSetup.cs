using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(EdgeCollider2D))]
public class ColorShieldSetup : MonoBehaviour {

    [SerializeField]
    [Range(1, 12)]
    int level = 1;

    public GameObject colorArc;

    public int segments = 40;
    public float radius = 4f;   

    LineRenderer line;
    EdgeCollider2D ec2d;
    List<Vector2> ecPoints;

    void Start()
    {     
        CreateColorShieldArcs(level);   
    }

    void CreateColorShieldArcs(int numberOfArcs)
    {
        int z = 0;
        int arcAngle = CalcArcAngle(numberOfArcs);
        for (int i = 0; i < numberOfArcs; i++)
        {
            if (i == 0)
                z = 0;
            else
                z += arcAngle;
        
            GameObject _colorArc = Instantiate(colorArc, transform.position, Quaternion.Euler(0, 0, z));

            line = _colorArc.GetComponent<LineRenderer>();
            ec2d = _colorArc.GetComponent<EdgeCollider2D>();
            ecPoints = new List<Vector2>();
            line.positionCount = CalcSegments(numberOfArcs) + 1;
            line.useWorldSpace = false;
            //line.material = new Material(Shader.Find("Sprites/Default"));

            CreatePoints(level, i);

            line.transform.parent = transform;
        }
    }

    int CalcSegments (int arcs)
    {
        arcs = (arcs == 0) ? 1 : arcs;
        // 3 is a special Case
        if (arcs == 3)
            return 30;

        return segments / arcs;
    }

    int CalcArcAngle (int arcs)
    {
        arcs = (arcs == 0) ? 1 : arcs;
        return 360 / arcs;
    }

    // Creates points for ColorArc based on number needed to form circle
    // Based on : https://forum.unity3d.com/threads/linerenderer-to-create-an-ellipse.74028/
    LineRenderer CreatePoints(int numbOfArcs, int colorIndex)
    {
        float x;
        float y;
        float z = 0f;

        float angle = 20f;

        int _arcAngle = CalcArcAngle(numbOfArcs);
        int _segments = CalcSegments(numbOfArcs);

        for (int i = 0; i < (_segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            line.SetPosition(i, new Vector3(x, y, z));
            ecPoints.Add(line.GetPosition(i));

            angle += (_arcAngle / _segments);
        }
        if (ecPoints.Count > 1)
            ec2d.points = ecPoints.ToArray();

        int _index = colorIndex % ColorManager.colors.Length;
        line.startColor = line.endColor = ColorManager.colors[_index];

        return line;
    }
}
