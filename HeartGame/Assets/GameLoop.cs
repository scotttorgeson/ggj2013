using UnityEngine;
using System.Collections;

public class GameLoop : MonoBehaviour {
	private float nextSpawnTime;
	private float nextMoneyTime;
	
	public float spawnTimerDelay;
	public float moneyDepositDelay;
	
	public int playerMoney;
	public int enemyMoney;
	public int moneyGain;
	
	// Use this for initialization
	void Start () {
		nextSpawnTime = Time.time + spawnTimerDelay;
		nextMoneyTime = Time.time + moneyDepositDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= nextSpawnTime){
			SpawnUnits();
			nextSpawnTime += spawnTimerDelay;
		}
		if(Time.time >= nextMoneyTime){
			GiveMoney();
			nextMoneyTime += moneyDepositDelay;
		}
	}
	
	void SpawnUnits(){
		var spawners = GameObject.FindGameObjectsWithTag("spawner");
		foreach(var spawner in spawners){
			spawner.Spawn();
		}
	}
	
	void GiveMoney(){
		playerMoney += moneyGain;
		enemyMoney += moneyGain;
	}
}
