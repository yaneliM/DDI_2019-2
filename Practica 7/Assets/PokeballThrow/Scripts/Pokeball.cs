using UnityEngine;
using System.Collections;

public class Pokeball : MonoBehaviour
{
	private Vector3 initialPosition;
	private bool isWind = false;
	private float curveWind = 1f;
	private float curveForce = 0f;
	private float throwPos = 0f;
	private bool isCurve = false;
	private bool ballThrowed = false;
	private IEnumerator jumpCoroutine;

	void OnEnable ()
	{
		initialPosition = transform.position;
	}

	void Start ()
	{
		jumpCoroutine = JumpAround (0.3f);
		StartCoroutine (jumpCoroutine);
	}

	void FixedUpdate ()
	{
		float dist = 0f;
		if (isCurve && dist <= (throwPos - (throwPos / 9.5f)))
		{
			if (!isWind)
				StartCoroutine (wind (0.5f));
			transform.Translate (Vector3.right * -curveForce * curveWind * Time.deltaTime);
		}
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.CompareTag ("Object"))
		{
			Destroy (gameObject);
		}
	}

	private void isThrow (bool flag)
	{
		if (jumpCoroutine != null) {
			StopCoroutine (jumpCoroutine);
			jumpCoroutine = null;
		}
		ballThrowed = flag;
	}

	public void ResetBall ()
	{
		ballThrowed = false;
		StopAllCoroutines ();
		StartCoroutine (GobackToDefaultPosition (0.3f));
	}

	private void SetCurve (float cFactore)
	{
		curveForce = cFactore;
		isCurve = true;
	}

	IEnumerator wind (float t)
	{
		isWind = true;
		float rate = 1.0f / t;
		float i = 0f;
		while (i<1.0f) {
			i += rate * Time.deltaTime;
			curveWind = Mathf.Lerp (1, 26, i);
			yield return 0;
		}
	}

	IEnumerator JumpAround (float tm)
	{
		while (!ballThrowed) {
			transform.position = initialPosition;
			float i = 0f;
			float rate = 1.0f / tm;
			Vector3 from = initialPosition;
			Vector3 to = new Vector3 (from.x, from.y + 0.05f, from.z);
			Vector3 bump = new Vector3 (from.x, from.y - 0.05f, from.z);

			while (i<1.0f) {
				i += rate * Time.deltaTime;
				transform.position = Vector3.Lerp (from, to, i);
				yield return null;
			}

			i = 0f;
			while (i<1.0f) {
				i += rate * Time.deltaTime;
				transform.position = Vector3.Lerp (to, bump, i);
				yield return null;
			}

			i = 0f;
			while (i<1.0f) {
				i += rate * Time.deltaTime;
				transform.position = Vector3.Lerp (bump, from, i);
				yield return null;
			}
		}
	}

	IEnumerator GobackToDefaultPosition (float tm)
	{
		float i = 0f;
		float rate = 1.0f / tm;
		Vector3 from = transform.position;
		Vector3 to = initialPosition;
		while (i<1.0f) {
			i += rate * Time.deltaTime;
			transform.position = Vector3.Lerp (from, to, i);
			yield return null;
		}
		transform.position = initialPosition;
		jumpCoroutine = JumpAround (0.3f);
		StartCoroutine (jumpCoroutine);
	}
}
