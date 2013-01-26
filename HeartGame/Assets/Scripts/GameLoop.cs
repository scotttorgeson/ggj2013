using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour
{
	private float nextSpawnTime;
	private float nextMoneyTime;
	public float spawnTimerDelay;
	public float moneyDepositDelay;
	public int moneyGain;
	public PlayerScript player;
	public PlayerScript enemy;
	
	// Use this for initialization
	void Start () {
		nextSpawnTime = Time.time + spawnTimerDelay;
		nextMoneyTime = Time.time + moneyDepositDelay;
	}
	
	// Update is called once per frame
	void Update () {
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
	}
	
	void GiveMoney () {
		player.AddMoney (moneyGain);
		enemy.AddMoney (moneyGain);
	}
}
