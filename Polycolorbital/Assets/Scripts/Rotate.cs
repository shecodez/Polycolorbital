using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    [SerializeField]
    private float speed = 100f;

    public float Speed { get; set; }

	void FixedUpdate () {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
	}
}
