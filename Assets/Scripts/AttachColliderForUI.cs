using UnityEngine;
using System.Collections;

public class AttachColliderForUI : MonoBehaviour {

    public Canvas canvas;


	// Use this for initialization
	void Start ()
    {
        // 親であるCanvasにColliderをアタッチ
        canvas.gameObject.AddComponent<BoxCollider>();
        // 子であるUIにColliderをアタッチ
        foreach (Transform child in canvas.transform)
        {
            child.gameObject.AddComponent<BoxCollider>();
        }

    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
