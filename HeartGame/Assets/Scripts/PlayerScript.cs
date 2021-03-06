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
	
	public float powerCooldownTime = 5.0f;
	public float powerCooldownTimer = 0.0f;
	
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
		
		powerCooldownTimer -= Time.deltaTime;
		powerCooldownTimer = Mathf.Max ( 0.0f, powerCooldownTimer );
	}
	
	private void UpdateRotation()
	{
		if ( this.tag == "PlayerBase" && Input.GetButtonDown("RotateBase") )
		{
			int rotateDirection = 0;
			if ( Input.GetAxis ("RotateBase") > 0.0f )
			{
				rotateDirection = 1;
			}
			else
			{		
				rotateDirection = -1;
			}
			
			RotateDirection( rotateDirection );
		}
		
		//Rotate base
		float rot = gameObject.transform.eulerAngles.y;
		rot = Mathf.MoveTowardsAngle(rot, targetRotation, 3.0f);
		gameObject.transform.eulerAngles = new Vector3(0f, rot, 0f);
	}
	
	public void RotateDirection(int direction)
	{
		targetRotation += 360 / 5 * direction;
		
		foreach(var spawn in spawnPoints)
		{
			spawn.Rotated(direction);
		}
	}
	
	public void AddMoney(int amount){
		EnemyAI ai = GetComponent<EnemyAI>();
		
		if ( ai != null )
		{
			float fAmount = amount;
			
			if ( EnemyAI.difficulty == 0 )
			{
				fAmount *= 0.8f;	
			}
			
			if ( EnemyAI.difficulty == 2 )
			{
				fAmount *= 1.2f;	
			}
			
			amount = (int)fAmount;
		}
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
			spawner.transform.FindChild("Barrack").renderer.material.color = upgrade.barrackColor;
		}
	}
}
