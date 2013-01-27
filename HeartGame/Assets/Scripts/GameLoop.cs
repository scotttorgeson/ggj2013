using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{
	private float nextSpawnTime;
	private float nextMoneyTime;
	
	public float spawnTimerDelay = 10;
	public float moneyDepositDelay = 1;
	public int moneyGain = 25;
	public PlayerScript player;
	public PlayerScript enemy;
	
	public bool skipPlayerSpawn = false;
	public bool skipEnemySpawn = false;
	
	public void SkipPlayerSpawn()
	{
		Transform t = player.transform.FindChild("SkipSpawnSphere");
		if ( t != null )
		{
			t.gameObject.renderer.enabled = true;	
		}
		
		skipPlayerSpawn = true;
	}
	
	public void SkipEnemySpawn()
	{
		Transform t = enemy.transform.FindChild("SkipSpawnSphere");
		if ( t != null )
		{
			t.gameObject.renderer.enabled = true;	
		}
		
		skipEnemySpawn = true;
	}
	
	void TurnOffSpawnSpheres()
	{
		Transform t = player.transform.FindChild("SkipSpawnSphere");
		if ( t != null )
		{
			t.gameObject.renderer.enabled = false;
		}
		
		t = enemy.transform.FindChild("SkipSpawnSphere");
		if ( t != null )
		{
			t.gameObject.renderer.enabled = false;	
		}
	}
	
	// Use this for initialization
	void Start () {
		nextSpawnTime = Time.time + spawnTimerDelay;
		nextMoneyTime = Time.time + moneyDepositDelay;
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( Input.GetAxis("Exit") != 0.0f )
			Application.Quit();
		
		//Check for end-game state
		if (enemy.currentLife > 0 && player.currentLife > 0) {
			if (Time.time >= nextSpawnTime) {
				SpawnUnits ();
				nextSpawnTime += spawnTimerDelay;
			}
			if (Time.time >= nextMoneyTime) {
				GiveMoney ();
				nextMoneyTime += moneyDepositDelay;
			}
		}
	}
	
	void OnGUI () {
		//Check for end-game state
		if (enemy.currentLife <= 0) {
			//Win Game
			GUI.Box (new Rect (10, 10, 100, 10), "You WIN!");
			
			if (GUI.Button (new Rect (20, 40, 80, 20), "New Game")) {
				Application.LoadLevel (1);
			}
			
			if (GUI.Button (new Rect (20, 80, 80, 20), "Main Menu")) {
				Application.LoadLevel (0);
			}
		} else if (player.currentLife <= 0) {
			//Lose Game
			GUI.Box (new Rect (10, 10, 100, 10), "You LOSE!");
			
			if (GUI.Button (new Rect (20, 40, 80, 20), "New Game")) {
				Application.LoadLevel (1);
			}
			
			if (GUI.Button (new Rect (20, 80, 80, 20), "Main Menu")) {
				Application.LoadLevel (0);
			}
		}
	}
	
	void SpawnUnits () {
		if(!skipPlayerSpawn){
			foreach(var spawn in player.spawnPoints){
				spawn.Spawn();
			}
		}
		if(!skipEnemySpawn){
			foreach(var spawn in enemy.spawnPoints){
				spawn.Spawn();
			}
		}
		skipPlayerSpawn = skipEnemySpawn = false;
		TurnOffSpawnSpheres();
	}
	
	void GiveMoney () {
		player.AddMoney (moneyGain);
		enemy.AddMoney (moneyGain);
	}
}
