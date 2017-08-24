using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class ColorBlockSetup : MonoBehaviour
{

    //int level = GameManager.level;

    public GameObject layerGO;
    public GameObject blockGO;

    [SerializeField]
    int sides = 3;

    int totalNumLayers = 9;
    [SerializeField]
    int outermostLayer = 5;

    float radius = 0.5f;

    LineRenderer line;
    LineRenderer edge;
    PolygonCollider2D pc2d;
    List<Vector2> pcPts;

    public int Sides { get; set; }
    public int TotalNumLayers { get; set; }
    public int OutermostLayer { get; set; }
    public float Radius { get; set; }

    List<GameObject> layers;
    List<List<GameObject>> blocks;

    void Start()
    {
        layers = new List<GameObject>();
        //blocks = new List<List<GameObject>>();

        for (int i = 0; i < totalNumLayers; i++)
        {
            CreateColorBlocksLayer(radius);
            radius += .3f;
        }

        for (int i = 0; i < outermostLayer; i++)
        {
            //blocks.Add(new List<GameObject>());
            for (int j = 0; j < sides; j++)
            {        
                // create each edge from guide line layer
                CreateBlock(layers[i], j, j + 1);
                //blocks[i].Add(block);               
            }
        }
    }

    public void CreateColorBlocksLayer(float radius)
    {
        GameObject _layer = Instantiate(layerGO);
        _layer.transform.parent = transform;

        // Create layer guide line
        CreateLayerGuideLine(_layer, radius);
    }

    void CreateLayerGuideLine(GameObject layer, float radius)
    {
        line = layer.GetComponent<LineRenderer>();

        line.positionCount = sides + 1;
        line.useWorldSpace = false;

        // Create all points in the layer's line
        CreateGuideLinePoints(sides, radius);

        line.transform.parent = layer.transform;

        layers.Add(layer);
        
        /*for (int i = 0; i < sides; i++)
        {
            // create edge from guide line
            CreateBlock(layer, i, i + 1);
        }*/
        
    }

    // formula :  https://math.stackexchange.com/q/117172
    LineRenderer CreateGuideLinePoints(int sides, float radius)
    {
        float x;
        float y;
        float z = 0f;

        float angle = 2 * Mathf.PI / sides;
        for (int i = 0; i < sides + 1; i++)
        {
            x = radius * Mathf.Cos(i * angle);
            y = radius * Mathf.Sin(i * angle);

            line.SetPosition(i, new Vector3(x, y, z));
        }
        line.startColor = line.endColor = Color.clear;
        return line;
    }

    public void CreateBlock(GameObject layer, int vertex1, int vertex2)
    {     
        GameObject colorBlock = Instantiate(blockGO, transform.position, transform.rotation);
        LineRenderer gLine = layer.GetComponent<LineRenderer>();

        edge = colorBlock.GetComponent<LineRenderer>();
        edge.useWorldSpace = false;

        pc2d = colorBlock.GetComponent<PolygonCollider2D>();
        pcPts = new List<Vector2>();

        edge.positionCount = 2;
        edge.SetPosition(0, gLine.GetPosition(vertex1));
        edge.SetPosition(1, gLine.GetPosition(vertex2)); 

       
        float xA = gLine.GetPosition(vertex1).x;
        float xB = gLine.GetPosition(vertex2).x;
        float yA = gLine.GetPosition(vertex1).y;
        float yB = gLine.GetPosition(vertex2).y;

        float w = .03f;
        pcPts.Add(new Vector3(xA - w, yA - w, 0f));
        pcPts.Add(new Vector3(xA + w, yA + w, 0f));
        pcPts.Add(new Vector3(xB + w, yB + w, 0f));
        pcPts.Add(new Vector3(xB - w, yB - w, 0f));        

        if (pcPts.Count > 1)
            pc2d.points = pcPts.ToArray();

        int _index = Random.Range(0, sides) % ColorManager.colors.Length;
        edge.startColor = edge.endColor = ColorManager.colors[_index];

        edge.numCapVertices = 1;

        edge.transform.parent = layer.transform;

        //return colorBlock;
    }

    public void CreateBlockArgs (int[] args)
    {
        args[0] += 1;
        if (args[0] < totalNumLayers)
        { 
            int index = args[1];
            if (index >= 0 && index < sides)
            {
                //if (layers[args[0]].transform.childCount < sides)                  
                CreateBlock(layers[args[0]], index, index + 1);
            }          
        }
    }

    public void RemoveBlock (GameObject colorBlock)
    {
        Destroy(colorBlock);
    }
     
    // Update is called once per frame
    // void Update () { }
}
