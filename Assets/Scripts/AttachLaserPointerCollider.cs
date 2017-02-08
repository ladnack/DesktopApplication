using UnityEngine;
using System.Collections;

public class AttachLaserPointerCollider : MonoBehaviour {

    public SteamVR_LaserPointer LaserPointerScript;

    // Use this for initialization
    void Start ()
    {
        if (LaserPointerScript == null)
        {
            LaserPointerScript = GetComponent<SteamVR_LaserPointer>();
        }
        
        if (!LaserPointerScript.addRigidBody)
        {
            Debug.Log("Please set true in addRigidBody ");
            BoxCollider collider = LaserPointerScript.pointer.AddComponent<BoxCollider>();
            collider.isTrigger = true;
            Rigidbody rigidBody = LaserPointerScript.pointer.AddComponent<Rigidbody>();
            rigidBody.isKinematic = true;
        }
        GameObject laser = LaserPointerScript.pointer;
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
