using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
	public int health;
	public float cash;
	public bool collectWeapon;

	//Function called when saving game
	public void SaveGamePrepare()
	{
		// Get Player Data Object (from LoadSaveManager)
		LoadSaveManager.GameStateData.DataPlayer data = GameManager.StateManager.gameState.player;

		// Fill in player data for save game
		data.collectedCash = cash;
		data.collectedWeapon = collectWeapon;
		data.Health = health;

		data.posRotScale.posX = transform.position.x;
        data.posRotScale.posY = transform.position.y;
        data.posRotScale.posZ = transform.position.z;

        data.posRotScale.rotX = transform.rotation.x;
        data.posRotScale.rotY = transform.rotation.y;
        data.posRotScale.rotZ = transform.rotation.z;

        data.posRotScale.scaleX = transform.localScale.x;
        data.posRotScale.scaleY = transform.localScale.y;
        data.posRotScale.scaleZ = transform.localScale.z;

    }

	// Function called when loading is complete
	public void LoadGameComplete()
	{
        // Get Player Data Object
        LoadSaveManager.GameStateData.DataPlayer data = GameManager.StateManager.gameState.player;


		//Load data back to Player
		health = data.Health;
		cash = data.collectedCash;
		collectWeapon = data.collectedWeapon;

        //Give player weapon, activate and destroy weapon power-up
        if (collectWeapon)
		{
			//Find weapon powerup in level
			GameObject weaponPowerUp = GameObject.Find("Weapon_Powerup");

			//Send OnTriggerEnter message
			weaponPowerUp.SendMessage("OnTriggerEnter2D", GetComponent<Collider2D>(), SendMessageOptions.DontRequireReceiver);

		}

		//Set position
		transform.position = new Vector3(data.posRotScale.posX, data.posRotScale.posY, data.posRotScale.rotZ);


        //Set rotation
        transform.rotation = Quaternion.Euler(data.posRotScale.rotX, data.posRotScale.rotY, data.posRotScale.rotZ);

        //Set scale
        transform.localScale = new Vector3(data.posRotScale.scaleX, data.posRotScale.scaleY, data.posRotScale.scaleZ);
    }
}
