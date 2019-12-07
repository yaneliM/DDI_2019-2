using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour {
	private bool paused = false;
	private bool mute = true;
	public GameObject pauseMenu;
	//public GameObject sound;



	
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.P))
		{
			paused = !paused;
		}

		if(paused)
		{
			Time.timeScale = 0;
			pauseMenu.SetActive(true);
		}

		else
		{
			Time.timeScale = 1;
			pauseMenu.SetActive(false);
		}

		if(mute)
		{
		//	sound.SetActive(true);
		}

		else
		{
			//sound.SetActive(false);
		}
		
	}

	public void ContinueGame(){
		paused = false;
	}
	public void PauseMusic(){
		mute = !mute;
	}
	public void ReloadScene(){
	  SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}

