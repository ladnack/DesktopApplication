using UnityEngine;
using System.Collections;

public class Translate : MonoBehaviour {

    private float rate = 0.1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += new Vector3(0f, 0f, rate);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position -= new Vector3(0f, 0f, rate);
        }


    }
}
