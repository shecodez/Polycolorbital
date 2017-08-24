using UnityEngine;

public class ColorBlock : MonoBehaviour {

    public int instanceID;
    public int startIndex;

    public static int StartIndex { get; set; }

    void Start ()
    {
        instanceID = this.gameObject.GetInstanceID();
        startIndex = this.transform.GetSiblingIndex();
    }
	
	// Update is called once per frame
	//void Update () { }

    void OnTriggerEnter2D(Collider2D colInfo)
    {
        if (colInfo.tag == "ColorArc")
        {
            // Game Over
            //GameManager.GameOver;
            Debug.Log("GAME OVER!");
        }
    }
}
