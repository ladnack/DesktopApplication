using UnityEngine;
using System.Collections;

public class AttachLaserPointerCollider : MonoBehaviour {


	// Use this for initialization
	void Start ()
    {
        
        var LaserPointerScript = GetComponent<SteamVR_LaserPointer>();
        var laser = LaserPointerScript.pointer;
        laser.AddComponent<LaserPoniterCollider>();
        
        /*
        // Another method of finding Laser(Cubu)
        var cube = transform.Find("New Game Object/Cube").gameObject;
        cube.AddComponent<LaserPoniterCollider>();
        */
    }
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
