using UnityEngine;

public class ColorManager : MonoBehaviour {

    public static Color32[] colors;

    void Awake ()
    {
        colors = new Color32[6];
    
        colors[0] = new Color32(255, 50, 78, 255);
        colors[1] = new Color32(25, 255, 236, 255);
        colors[2] = new Color32(250, 250, 26, 255);
        colors[3] = new Color32(7, 255, 20, 255);
        colors[4] = new Color32(175, 50, 255, 255);
        colors[5] = new Color32(60, 77, 250, 255);
    }
}
