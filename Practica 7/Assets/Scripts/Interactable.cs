using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Interactable : MonoBehaviour 
{
	public bool isInsideZone;

	public string buttonName = "interact";

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

	}

	void Update()
	{
		if(isInsideZone)
		{
			//if(Input.GetKeyDown(interactionKey))
		//	if(CrossPlatformInputManager.GetButtonDown(buttonName))
		//	{
				Interact();
				
		//	}
			
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
		if(rend.CompareTag("Object")){
		//	Destroy(other.gameObject);
			Debug.Log("vnkjfbnvjgf"+other);
		}
		// Debug.Log("Entraste en la esfera!");
		// rend.material.color = newColor;
		isInsideZone = true;

		
	}

	/// <summary>
	/// OnTriggerExit is called when the Collider other has stopped touching the trigger.
	/// </summary>
	/// <param name="other">The other Collider involved in this collision.</param>
	void OnTriggerExit(Collider other)
	{

		isInsideZone = false;
		
	}

	public virtual void Interact()
	{
		if(rend.CompareTag("Object")){

			Debug.Log("Cube");
		}
	}
}
