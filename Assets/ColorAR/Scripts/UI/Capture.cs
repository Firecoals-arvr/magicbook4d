using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class Capture : MonoBehaviour
	{
		public Camera cam;
		public GameObject mainPanel;
		public GameObject reviewPanel;
		public UI2DSprite unitySprite;
		private Texture2D screenShot;
		Sprite tempSprite;

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public IEnumerator TakePhoto()
		{
			RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
			cam.targetTexture = rt;
			screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
			cam.Render();
			RenderTexture.active = rt;
			screenShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
			screenShot.Apply();
			cam.targetTexture = null;
			RenderTexture.active = null; // JC: added to avoid errors
			Destroy(rt);
			yield return new WaitForEndOfFrame();
			tempSprite = Sprite.Create(screenShot, new Rect(0, 0, Screen.width, Screen.height), new Vector2(0, 0));
			unitySprite.sprite2D = tempSprite;
		}

		public void Snap()
		{
			StartCoroutine(TakePhoto());
		}

		public void BackScreenShot()
		{
			reviewPanel.SetActive(false);
			mainPanel.SetActive(true);
		}

		public void Save()
		{

		}
	}
}
