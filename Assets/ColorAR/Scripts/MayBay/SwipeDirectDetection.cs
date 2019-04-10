using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Firecoals.Color
{
	/// <summary>
	/// 
	/// </summary>
	public class SwipeDirectDetection : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler
	{
		private enum DraggedDirection
		{
			UP,
			DOWN,
			RIGHT,
			LEFT
		}

		// Start is called before the first frame update
		void Start()
		{

		}

		// Update is called once per frame
		void Update()
		{

		}

		public void OnEndDrag(PointerEventData eventData)
		{
			Debug.Log("Press position + " + eventData.pressPosition);
			Debug.Log("End position + " + eventData.position);
			Vector3 dragVectorDirection = (eventData.position - eventData.pressPosition).normalized;
			Debug.Log("norm + " + dragVectorDirection);
			GetDragDirection(dragVectorDirection);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{

		}

		public void OnDrag(PointerEventData eventData)
		{

		}

		private DraggedDirection GetDragDirection(Vector3 dragVector)
		{
			float positiveX = Mathf.Abs(dragVector.x);
			float positiveY = Mathf.Abs(dragVector.y);
			DraggedDirection draggedDir;

			if (positiveX > positiveY)
			{
				draggedDir = (dragVector.x > 0) ? DraggedDirection.RIGHT : DraggedDirection.LEFT;
			}
			else
			{
				draggedDir = (dragVector.y > 0) ? DraggedDirection.UP : DraggedDirection.LEFT;
			}
			Debug.Log(draggedDir);
			return draggedDir;
		}
	}
}

