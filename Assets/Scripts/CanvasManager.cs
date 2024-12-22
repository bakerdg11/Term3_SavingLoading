using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {
	public GameObject pausePanel;
	bool gamePaused;

	Character cRef;
	Enemy eRef;

	// Use this for initialization
	void Start () {
		cRef = GameObject.FindGameObjectWithTag ("Player").GetComponent<Character> ();
		eRef = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<Enemy>();
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape))
			PauseGame ();
		
	}

	public void PauseGame()
	{
		gamePaused = !gamePaused;
		pausePanel.SetActive(gamePaused);

		if (gamePaused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}

	public void SaveGame()
	{
        // Clear existing enemy data
        GameManager.StateManager.gameState.enemies.Clear();

        cRef.SaveGamePrepare ();
		eRef.SaveGamePrepare ();

		GameManager.Instance.SaveGame ();
	}

	public void LoadGame()
	{
        GameManager.Instance.LoadGame();

        cRef.LoadGameComplete ();
		eRef.LoadGameComplete ();



	}

}
