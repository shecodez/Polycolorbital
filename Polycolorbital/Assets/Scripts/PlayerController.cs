using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject beamCannon;
    public GameObject projectile;

	//void Start () { }
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            FireZeLazor();        
        }
    }

    void FireZeLazor ()
    {
        // Instantiate the projectile
        GameObject _projectile = Instantiate(projectile);
        _projectile.transform.position = beamCannon.transform.position;
        _projectile.transform.rotation = beamCannon.transform.rotation;
    }   
}
