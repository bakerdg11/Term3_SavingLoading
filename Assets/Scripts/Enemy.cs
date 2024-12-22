using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

	public int enemyID;
	public int health;

	private void Start ()
	{
		enemyID = GetInstanceID();
	}
			
	//Function called when saving game
	public void SaveGamePrepare()
	{
		//Create enemy data for this enemy
		LoadSaveManager.GameStateData.DataEnemy data = new LoadSaveManager.GameStateData.DataEnemy();



		//Fill in data for current enemy
		data.EnemyID = enemyID;
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


        //Add enemy to Game State
		GameManager.StateManager.gameState.enemies.Add(data);
    }

	// Function called when loading is complete
	public void LoadGameComplete()
	{
		// Cycle through enemies and find matching ID
		List<LoadSaveManager.GameStateData.DataEnemy> enemies = GameManager.StateManager.gameState.enemies;


		// Reference to this enemy
		LoadSaveManager.GameStateData.DataEnemy data = null;

		for(int i=0; i<enemies.Count; i++)
		{
			if(enemies[i].EnemyID == enemyID)
			{
				// Found enemy. Now break break from loop
				data = enemies[i];
				break;
			}
		}

		// If here and no enemy is found, then it was destroyed when saved. So destroy.
		if (data == null)
		{
			Destroy(gameObject);
			return;
		}

		// Else load enemy data
		enemyID = data.EnemyID;
		health = data.Health;


        //Set position
        transform.position = new Vector3(data.posRotScale.posX, data.posRotScale.posY, data.posRotScale.rotZ);


        //Set rotation
        transform.rotation = Quaternion.Euler(data.posRotScale.rotX, data.posRotScale.rotY, data.posRotScale.rotZ);

        //Set scale
        transform.localScale = new Vector3(data.posRotScale.scaleX, data.posRotScale.scaleY, data.posRotScale.scaleZ);
    }



}
