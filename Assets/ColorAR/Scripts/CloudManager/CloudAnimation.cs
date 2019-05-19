using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Firecoals.Color
{
	public class CloudAnimation : MonoBehaviour
	{
		Transform beginTrans;

		public Vector3 center = new Vector3(0, 0.1f, 0);
		public Vector3 size = new Vector3(0.4f, 0.2f, 0.4f);

		private void Start()
		{
			beginTrans = this.gameObject.transform;
            if (gameObject.transform.parent.transform.parent.gameObject.activeSelf)
            {
                InvokeRepeating("CheckConditionMove", 0.5f, 1f);
            }
		}

		bool isMoving = false;
		IEnumerator moveToX(GameObject obj, Vector3 startPos, Vector3 endPos, float duration)
		{
			if (isMoving)
			{
				yield break;
			}

			isMoving = true;

			float counter = 0;

			while (counter < duration)
			{
				counter += Time.deltaTime;
				obj.transform.position = Vector3.Lerp(startPos, endPos, counter / duration);
				yield return null;
			}

			isMoving = false;
		}

		public void MoveToEndPos() {
			Vector3 endPos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));

			float duration = Random.Range(3f, 4f);
            if (gameObject.activeSelf)
            {
                StartCoroutine(moveToX(this.gameObject, beginTrans.position, endPos, duration));
            }
		}

		public void CheckConditionMove()
		{
			if (!isMoving)
			{
				MoveToEndPos();
			}
		}

	}
}

