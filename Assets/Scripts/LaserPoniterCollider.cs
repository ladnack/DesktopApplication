using UnityEngine;
using System.Collections;

public class LaserPoniterCollider : MonoBehaviour {

    public GameObject collidingObject;

    // Use this for initialization
    void Start()
    {

    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collider)
    {
        GameObject collidedObj = collider.gameObject;
        collidingObject = collidedObj;

        // Debug.Log("Trigger Enter => " + collidedObj.name);

    }

    private void OnTriggerStay(Collider collider)
    {
        GameObject collidedObj = collider.gameObject;
        // collidingObject = collidedObj;

        // Debug.Log("Trigger Stay => " + collidedObj.name);

    }

    private void OnTriggerExit(Collider collider)
    {
        GameObject collidedObj = collider.gameObject;
        // collidingObject = null;

        // Debug.Log("Trigger Exit => " + collidedObj.name);

    }


}
