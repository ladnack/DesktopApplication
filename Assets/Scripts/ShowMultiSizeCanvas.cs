using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowMultiSizeCanvas : MonoBehaviour {

	public Camera mainCamera;
    public Canvas canvas;
    public float horizontalDegree = 30.0f;
	public float verticalDegree = 30.0f;


	// Use this for initialization
	void Start () {

		// Cameraがセットされていなかったら、Main Cameraを探す
		if (mainCamera == null) {
			Debug.Log ("This mainCamera is null.");
			mainCamera = GameObject.Find ("Main Camera").GetComponent<Camera> ();
		}

        if (canvas == null)
        {
            Debug.Log("This canvas is null.");
            // Canvasを探す
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

            if (canvas == null)
            {
                // Canvasを生成する
                canvas = CreateCanvasObject ().GetComponent<Canvas> ();
            }
            
        }


        // CanvasをMainCamera正面に配置
        canvas.ReconfigureToScreenToWorldSpace(mainCamera, 10.0f);

		// 距離を設定し、それに対応してAutoSizingする
		canvas.AutoSizingFor(10.0f);

		// 水平方向に指定した視野角にスケーリングする
		canvas.HorizontalRextScaling (horizontalDegree);
		// 垂直方向に指定した視野角にスケーリングする
		canvas.VerticalRextScaling (verticalDegree);

        // 子要素のcolliderをCanvasのスケーリングに合わせて調整する
        ColliderSizeAdjusting(canvas.gameObject);


    }


	// Update is called once per frame
	void Update () {
		
	}


	// Canvasを生成
	private GameObject CreateCanvasObject() {
		// デフォルトの設定
		var _canvas = new GameObject ("Canvas");
		_canvas.AddComponent<RectTransform> ();
		_canvas.AddComponent<Canvas> ();
		_canvas.AddComponent<CanvasRenderer> ();
		_canvas.AddComponent<CanvasScaler> ();
		_canvas.AddComponent<GraphicRaycaster> ();

		// 背景色をつける
		_canvas.AddComponent<RawImage> ();
		// RawImage.colorはいじらず、CanvasRendererで色を変更（RawImageがないと色がつかない？）
		float alpha = 0.6f;
		_canvas.GetComponent<CanvasRenderer> ().SetColor (new Color (Random.value, Random.value, Random.value, alpha));

		return _canvas;
	}

    // オブジェクト自身とその子のColliderのサイズを合わせる
    private void ColliderSizeAdjusting(GameObject canvas)
    {
        var canvasCollider = canvas.GetComponent<BoxCollider>();
        if (canvasCollider != null)
        {
            float x = canvas.GetComponent<RectTransform>().rect.width;
            float y = canvas.GetComponent<RectTransform>().rect.height;
            canvasCollider.size = new Vector3(x, y, 0f);
        }

        foreach (Transform child in canvas.transform)
        {
            var collider = child.GetComponent<BoxCollider>();
            if (collider != null)
            {
                float x = child.GetComponent<RectTransform>().rect.width;
                float y = child.GetComponent<RectTransform>().rect.height;
                collider.size = new Vector3(x, y, 0f);
            }
        }
    }

}

// Canvasクラスの機能拡張
static class CanvasExtensions {

	public static void ReconfigureToScreenToWorldSpace(this Canvas canvas, Camera camera, float distance) {
		canvas.renderMode = RenderMode.ScreenSpaceCamera;
		canvas.worldCamera = camera;
		// Canvasのsizeを調整
		canvas.planeDistance = distance;
		canvas.renderMode = RenderMode.WorldSpace;

	}

	public static void AutoSizingFor(this Canvas canvas, float distance) {
		// MainCameraからの距離をdistanceに変更（Cameraのpositionが（0, 0, 0）の時）
		canvas.transform.Translate(0f, 0f, distance - canvas.transform.position.z);
		// distanceに基づいて一定の比率でscaleを調整
		var scale = distance / canvas.planeDistance * canvas.transform.localScale.x;
		canvas.transform.localScale = new Vector3 (scale, scale, scale);

	}

	public static void HorizontalRextScaling(this Canvas canvas, float degree) {
		// 三角形ABCを考える
		// A、Cがわかっている時、角ACBが直角をなすように角BAC＝θに従って、点Bの位置を求める

		// degreeは0〜90に設定する
		if (degree <= 0 || 90 <= degree) { return; }

		// アタッチしたMain Cameraを取得（Camera位置がスケーリングの基準となる）
		Camera _camera = canvas.worldCamera;

		Vector3 vectAC = _camera.transform.forward;
		float distanceAC = Vector3.Distance (_camera.transform.position, canvas.transform.position);

		// 距離ABをcosθを利用して求める
		float distanceAB = distanceAC / Mathf.Cos (degree * Mathf.Deg2Rad);
		Vector3 vectAB = Quaternion.Euler (0f, degree, 0f) * vectAC;

		// 点Bを求める
		Vector3 pointB = _camera.transform.position + vectAB * distanceAB;

		// 設定するwidthは距離ABの2倍 -> scale対応させる
		float width = Vector3.Distance(canvas.transform.position, pointB) * 2f * (1 / canvas.transform.localScale.x);

		RectTransform rectTransform = canvas.GetComponent<RectTransform> ();
		rectTransform.sizeDelta = new Vector2 (width, rectTransform.rect.height);

	}

	public static void VerticalRextScaling(this Canvas canvas, float degree) {
		// 三角形ABCを考える
		// A、Cがわかっている時、角ACBが直角をなすように角BAC＝θに従って、点Bの位置を求める

		// degreeは0〜90に設定する
		if (degree <= 0 || 90 <= degree) { return; }

		// アタッチしたMain Cameraを取得（Camera位置がスケーリングの基準となる）
		Camera _camera = canvas.worldCamera;

		Vector3 vectAC = _camera.transform.forward;
		float distanceAC = Vector3.Distance (_camera.transform.position, canvas.transform.position);

		// 距離ABをcosθを利用して求める
		float distanceAB = distanceAC / Mathf.Cos (degree * Mathf.Deg2Rad);
		Vector3 vectAB = Quaternion.Euler (degree, 0f, 0f) * vectAC;

		// 点Bを求める
		Vector3 pointB = _camera.transform.position + vectAB * distanceAB;

		// 設定するwidthは距離ABの2倍 -> scale対応させる
		float height = Vector3.Distance(canvas.transform.position, pointB) * 2f * (1 / canvas.transform.localScale.x);

		RectTransform rectTransform = canvas.GetComponent<RectTransform> ();
		rectTransform.sizeDelta = new Vector2 (rectTransform.rect.width, height);

	}

}
