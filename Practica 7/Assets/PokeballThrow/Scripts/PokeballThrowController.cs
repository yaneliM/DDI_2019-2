using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PokeballThrowController : MonoBehaviour
{
	List<Vector3> ballPos = new List<Vector3> ();
	List<float> ballTime = new List<float> ();
	private GameObject currentBall;
	public bool isCurveReady;
	public Vector3 startPos;
	public Vector3 minThrow;
	public Vector3 maxThrow;
	private bool isCalculatingDir = false;
	private bool isGameStart = true;
	private bool isStartRotate = false;
	private Vector3 lastPos;
	private Vector3 lastBallPosition = Vector3.zero;
	private Vector3 v3Offset;
	float totalX = 0f, totalY = 0f;
	private int angleDirection = 0;
	private Plane plane;
	public GameObject pokeball;

	void Start ()
	{
		StartCoroutine (GetBallNow());
	}

	IEnumerator GetBallNow()
	{
		Debug.Log ("Spawming new pokeball..");
		yield return new WaitForSeconds (3f);
		if (currentBall != null)
		{
			Destroy (currentBall);
		}
		currentBall = Instantiate (pokeball, pokeball.transform.position, Quaternion.identity) as GameObject;
		currentBall.GetComponent<Collider> ().enabled = false;
	}


	void Update ()
	{
		if(currentBall == null)
		{
			return;
		}
		transform.position = currentBall.transform.position;
		if (isCalculatingDir)
		{
			if (Vector3.Distance (currentBall.transform.position, lastBallPosition) > 0f)
			{
				if (!isStartRotate)
				{
					Invoke ("ResetBall", 0.01f);
				}
				else
				{
					Vector3 dir = currentBall.transform.position - lastBallPosition;
					if (dir.x > dir.y)
					{
						totalX += 0.12f;
					}
					else
					{
						if (dir.x != dir.y)
						{
							totalY += 0.12f;
						}
					}
					if (totalX >= 2 && totalY >= 2)
					{
						isCurveReady = true;
					}
				}
				isStartRotate = true;
			}
			lastBallPosition = currentBall.transform.position;
		}
	}

	private void ResetBall ()
	{
		if (!isStartRotate) {
			totalX = 0f;
			totalY = 0f;
			isCurveReady = false;
			startPos = currentBall.transform.position;
			ballPos.Clear ();

			ballTime.Clear ();
			ballTime.Add (Time.time);

			ballPos.Add (currentBall.transform.position);
		}
	}

	void OnMouseDown ()
	{
		if (currentBall == null)
			return;
		plane.SetNormalAndPosition (Camera.main.transform.forward, currentBall.transform.position);
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float dist;
		plane.Raycast (ray, out dist);
		v3Offset = currentBall.transform.position - ray.GetPoint (dist);

		if (!currentBall || !isGameStart)
			return;
		ballPos.Clear ();

		ballTime.Clear ();
		ballTime.Add (Time.time);

		ballPos.Add (currentBall.transform.position);

		totalX = 0f;
		totalY = 0f;

		isCurveReady = false;

		isCalculatingDir = true;
		currentBall.SendMessage ("isThrow", true, SendMessageOptions.RequireReceiver);

		StartCoroutine (GettingDirection ());
	}

	IEnumerator GettingDirection ()
	{
		while (isCalculatingDir) {
			lastPos = startPos;
			startPos = Camera.main.WorldToScreenPoint (currentBall.transform.position);
			startPos.z = currentBall.transform.position.z - Camera.main.transform.position.z;
			startPos = Camera.main.ScreenToWorldPoint (startPos);
			yield return new WaitForSeconds (0.01f);
		}
	}

	void OnMouseDrag ()
	{
		if (!currentBall)
			return;

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float dist;
		plane.Raycast (ray, out dist);
		Vector3 v3Pos = ray.GetPoint (dist);
		v3Pos.z = currentBall.transform.position.z;
		v3Offset.z = 0;
		currentBall.transform.position = v3Pos + v3Offset;

		if (ballPos.Count > 0) {
			if (ballPos.Count <= 4) {
				if (Vector3.Distance (currentBall.transform.position, ballPos [ballPos.Count - 1]) >= 0.01f) {
					ballTime.Add (Time.time);
					ballPos.Add (currentBall.transform.position);
				}
			} else {
				if (Vector3.Distance (currentBall.transform.position, ballPos [ballPos.Count - 1]) >= 0.01f) {
					ballTime.RemoveAt (0);
					ballPos.RemoveAt (0);
					ballTime.Add (Time.time);
					ballPos.Add (currentBall.transform.position);
				}
			}
		} else {
			ballPos.Add (currentBall.transform.position);
		}
	}

	void OnMouseUp ()
	{
		isCalculatingDir = false;

		if (!currentBall || !isGameStart)
			return;

		var endPos = Input.mousePosition;
		endPos.z = currentBall.transform.position.z - Camera.main.transform.position.z;
		endPos = Camera.main.ScreenToWorldPoint (endPos);

		int ballPositionIndex = ballPos.Count - 2;

		if (ballPositionIndex < 0)
			ballPositionIndex = 0;

		Vector3 force = currentBall.transform.position - ballPos [ballPositionIndex];

		if (Vector3.Distance (lastPos, startPos) <= 0.0f ||
			currentBall.transform.position.y <= ballPos [ballPositionIndex].y || //if downside
			force.magnitude < 0.02f) //if not swipe
		{
			currentBall.SendMessage ("ResetBall", SendMessageOptions.RequireReceiver);
			return;
		}

		force.z = force.magnitude;
		force /= (Time.time - ballTime [ballPositionIndex]);
		force.y /= 2f;
		force.x /= 2f;

		force.x = Mathf.Clamp (force.x, minThrow.x, maxThrow.x);
		force.y = Mathf.Clamp (force.y, minThrow.y, maxThrow.y);
		force.z = Mathf.Clamp (force.z, minThrow.z, maxThrow.z);

		currentBall.SendMessage ("isThrow", true, SendMessageOptions.RequireReceiver);

		if (isCurveReady) {
			force.z -= 0.1f;
			if (angleDirection == 1)
			{
				if (force.z < 2.3f)
					force.z = 2.3f;
				currentBall.SendMessage ("SetCurve", -0.2);
			}
			else
			{
				if (force.z < 2.3f)
					force.z = 2.3f;
				currentBall.SendMessage ("SetCurve", 0.2);
			}
		}

		Rigidbody ballRigidbody = currentBall.GetComponent<Rigidbody> ();
		currentBall.GetComponent<Collider> ().enabled = true;
		ballRigidbody.useGravity = true;
		ballRigidbody.AddForce (force * 230.0f);
		StartCoroutine (GetBallNow());
	}
}
