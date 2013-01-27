using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	
	public int currentMoney;
	public int currentLife;
	
	public int startingMoney = 1000;
	public int startingLife = 10;
	public Spawner[] spawnPoints;
	
	private float targetRotation;
	
	public int[] upgradeCosts;
	
	// Use this for initialization
	void Start () {
		currentMoney = startingMoney;
		currentLife = startingLife;
		targetRotation = transform.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdateRotation();
	}
	
	private void UpdateRotation()
	{
		if ( Input.GetButtonDown("RotateBase") )
		{
			int rotateDirection = 0;
			if ( Input.GetAxis ("RotateBase") > 0.0f )
			{
				targetRotation += 360/5;
				rotateDirection = 1;
			}
			else
			{
				targetRotation -= 360/5;
				rotateDirection = -1;
			}
			
			
			foreach(var spawn in spawnPoints){
				spawn.Rotated(rotateDirection);
			}
		}
		
		//Rotate base
		float rot = gameObject.transform.eulerAngles.y;
		rot = Mathf.MoveTowardsAngle(rot, targetRotation, 3.0f);
		gameObject.transform.eulerAngles = new Vector3(0f, rot, 0f);
	}
	
	public void AddMoney(int amount){
		currentMoney += amount;
	}
	
	public void Attacked( int damage )
	{
		currentLife -= damage;
		
		// if ( currentLife <= 0 ) VictoryOrDefeat();
	}
	
	public void Upgrade(Spawner spawner, UnitUpgrade upgrade)
	{
		// if there are more tiers, and we have the money, upgrade the selected spawner
		if ( spawner!=null && upgrade!=null && currentMoney > upgrade.cost )
		{
			currentMoney -= upgrade.cost;
			spawner.currentUpgrade= upgrade;
		}
	}
}
