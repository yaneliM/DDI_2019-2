using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Interactable : MonoBehaviour 
{
	bool isInsideZone;
	public Color newColor;
	public GameObject infoCanvas;
	public AudioSource myAudio;
	public string buttonName = "interact";
	bool play;
	Renderer rend;
	public KeyCode interactionKey = KeyCode.I;
	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>

	protected virtual void Awake()
	{
		rend = GetComponent<Renderer>();
	}
	void Start()
	{
		infoCanvas.SetActive(false);
		play = false;
	}

	void Update()
	{
		if(isInsideZone)
		{
			if(Input.GetKeyDown(interactionKey))
			//if(CrossPlatformInputManager.GetButtonDown(buttonName))
			{
				Interact();
				
			}
			
		}
	}

	/// <summary>
	/// OnTriggerEnter is called when the Collider other enters the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerEnter(Collider other)
	{

		if(!other.CompareTag("Player"))
			return;
		if(rend.CompareTag("JukeBox")){
			Debug.Log("JukeBox");
			myAudio.Play();
		}
		// Debug.Log("Entraste en la esfera!");
		// rend.material.color = newColor;
		isInsideZone = true;
		infoCanvas.SetActive(true);
		
	}

	/// <summary>
	/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerExit(Collider other)
	{
		Debug.Log("OUUT");
		myAudio.Stop();
		infoCanvas.SetActive(false);
		isInsideZone = false;
		
	}

	public virtual void Interact()
	{
		if(rend.CompareTag("Crate")){
			rend.material.color = newColor;
			Debug.Log("Cube");
		}
		if(rend.CompareTag("JukeBox")){
			rend.material.color = newColor;
			Debug.Log("Box");
		}
		if(rend.CompareTag("Barrel")){
			rend.material.color = newColor;
			Debug.Log("Barrel");
		}
	}
}
