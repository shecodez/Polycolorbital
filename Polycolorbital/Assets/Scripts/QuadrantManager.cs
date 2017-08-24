using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrantManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        /*Quadrant quad = GetQuadrant(r2d.position);
        Debug.Log(quad);
        switch (quad)
        {
            case Quadrant.I:
                speed *= -1;
                r2d.AddForce(transform.up * speed);
                break;
            case Quadrant.II:               
                r2d.AddForce(transform.up * speed);
                break;
            case Quadrant.III:
                r2d.AddForce(transform.up * speed);
                break;
            case Quadrant.IV:
                r2d.AddForce(transform.up * speed);
                break;
            default:
                r2d.velocity = Vector2.zero;
                break;
        }*/
    }

    Quadrant GetQuadrant(Vector2 position)
    {
        Quadrant result = Quadrant.None;

        if (position.x > 0 && position.y > 0)
            result = Quadrant.I;
        if (position.x < 0 && position.y > 0)
            result = Quadrant.II;
        if (position.x < 0 && position.y < 0)
            result = Quadrant.III;
        if (position.x > 0 && position.y < 0)
            result = Quadrant.IV;

        return result;
    }

    public enum Quadrant
    {
        None,
        I,  // ++
        II, // -+
        III,// --
        IV  // +-
    }
}
