using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	private int lastFrameRotate = 0;
	
	public int currentMoney;
	public int currentLife;
	
	public int startingMoney = 1000;
	public int startingLife = 10;
	public Spawner[] spawnPoints;
	
	private float targetRotation = 0.0f;
	
	// Use this for initialization
	void Start () {
		currentMoney = startingMoney;
		currentLife = startingLife;
	}
	
	// Update is called once per frame
	void Update () {		
		var baseRotate = (int)Input.GetAxis("RotateBase");
		
		if(baseRotate != 0 && lastFrameRotate == 0){
			
			targetRotation += (float)baseRotate * 360/5;
			
			
			foreach(var spawn in spawnPoints){
				spawn.Rotated(baseRotate);
			}
		}
		
		//Rotate base
		float rot = gameObject.transform.eulerAngles.y;
		rot = Mathf.MoveTowards(rot, targetRotation, 1.0f);
		gameObject.transform.eulerAngles = new Vector3(0f, rot, 0f);
		
		lastFrameRotate = baseRotate;
	}
	
	public void AddMoney(int amount){
		currentMoney += amount;
	}
	
	public void Attacked( int damage )
	{
		currentLife -= damage;
		
		// if ( currentLife <= 0 ) VictoryOrDefeat();
	}
}
