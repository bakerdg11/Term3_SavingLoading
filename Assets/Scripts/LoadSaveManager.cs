using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadSaveManager : MonoBehaviour {

	// Save game data

	public class GameStateData
	{
		public struct DataTransform
		{
			public float posX;
			public float posY;
			public float posZ;

			public float rotX;
			public float rotY;
			public float rotZ;

			public float scaleX;
			public float scaleY;
			public float scaleZ;

		}

		// Data for enemy
		public class DataEnemy
		{
			//Enemy Transform Data
			public DataTransform posRotScale;

			//Enemy ID
			public int EnemyID;

			//Health
			public int Health;
		}

		// Data for player
		public class DataPlayer
		{
			//Transform Data
			public DataTransform posRotScale;

			//Collected cash
			public float collectedCash;

			//Has Collected Gun 01?
			public bool collectedWeapon;

			//Health
			public int Health;
		}

		// Instance variables
		public List<DataEnemy> enemies = new List<DataEnemy>();
		public DataPlayer player = new DataPlayer();

	}

	// Game data to save/load
	public GameStateData gameState = new GameStateData();


	// Saves game data to XML file
	public void Save(string fileName = "GameData.xml")
	{
		// Save game data
		XmlSerializer serializer = new XmlSerializer(typeof(GameStateData));
		FileStream stream = new FileStream(fileName, FileMode.Create); //This is creating the file name
		serializer.Serialize(stream, gameState); //This is doing the writing
		stream.Flush(); //Takes all the stuff that is being serialized. Dispose the filestream as it's saved to a file.
		stream.Dispose();
		stream.Close(); //Confirms it's a file that is ready to be read from

	}

	// Load game data from XML file
	public void Load(string fileName = "GameData.xml")
	{ 
		XmlSerializer serializer = new XmlSerializer(typeof(GameStateData));
		FileStream stream = new FileStream(fileName, FileMode.Open);
		gameState = serializer.Deserialize(stream) as GameStateData;
		stream.Flush();
		stream.Dispose();
		stream.Close();



	}
}