using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {

    public float speed;
    public Transform objectToOrbit;

	void Start ()
    {
        // negative speed = clockwise
        // positive speed = counterclockwise 
        speed = -10f;
	}

    void FixedUpdate ()
    {
        OrbitObject();
	}

    void OrbitObject ()
    {
        transform.RotateAround(objectToOrbit.position, Vector3.forward, speed * Time.deltaTime);
    }
}
