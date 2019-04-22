using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

namespace Firecoals.Color
{
	public class FootballManager : MonoBehaviour
	{
		static int direction = -1;
		Vector3 initBallPos;

		public GameObject ball;
		public FootballerController footballer;
		public GoalkeeperController goalkeeper;
		public GameObject pointTrigger;
		public LeanTouch leanTouch;
        public UILabel score;

		public float ballSpeed;

		private void Awake()
		{
			initBallPos = ball.transform.position;
		}

		// Start is called before the first frame update
		void Start()
		{
			BallReturn();
            Goal.score = 0;
            
		}

		// Update is called once per frame
		void Update()
		{
            score.text = "Score: " + Goal.score;
        }

		void BallReturn()
		{
			ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
			ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
			ball.transform.position = initBallPos;
			ball.transform.rotation = Quaternion.identity;
		}

		public void ToTheRight()
		{
			Debug.Log("<color=blue>Shoot to the right!</color>");
			direction = 0;
			footballer.KickBall();
		}

		public void ToTheLeft()
		{
			Debug.Log("<color=blue>Shoot to the left!</color>");
			direction = 2;
			footballer.KickBall();
		}

		public void ToTheMiddle()
		{
			Debug.Log("<color=blue>Shoot to the middle!</color>");
			direction = 1;
			footballer.KickBall();
		}

		public void BallMove()
		{
			switch (direction)
			{
				case 0:
					ball.GetComponent<Rigidbody>().AddForce(new Vector3(1.765f, Random.Range(0.6f, 0.68f), 0.458f) * ballSpeed);
					direction = -1;
					footballer.ReturnInitPos();
					Invoke("BallReturn", 3);
					break;
				case 1:
					ball.GetComponent<Rigidbody>().AddForce(new Vector3(1.765f, Random.Range(0.6f, 0.68f), 0f) * ballSpeed);
					direction = -1;
					footballer.ReturnInitPos();
					Invoke("BallReturn", 3);
					break;
				case 2:
					ball.GetComponent<Rigidbody>().AddForce(new Vector3(1.765f, Random.Range(0.6f, 0.68f), -0.441f) * ballSpeed);
					direction = -1;
					footballer.ReturnInitPos();
					Invoke("BallReturn", 3);
					break;
			}
		}

		public void EnableTrigger()
		{
			pointTrigger.SetActive(true);
		}
		public void DisableTrigger()
		{
			pointTrigger.SetActive(false);
		}
	}
}

