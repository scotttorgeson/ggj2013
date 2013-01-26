using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	private int lastFrameRotate = 0;
	
	public int currentMoney;
	public int currentLife;
	
	public int startingMoney = 1000;
	public int startingLife = 10;
	public Spawner[] spawnPoints;
	
	// Use this for initialization
	void Start () {
		currentMoney = startingMoney;
		currentLife = startingLife;
	}
	
	// Update is called once per frame
	void Update () {		
		var baseRotate = (int)Input.GetAxis("RotateBase");
		
		if(baseRotate != 0 && lastFrameRotate == 0){
			float rot = (float)baseRotate * 360/5;
			//Rotate base
			gameObject.transform.Rotate(new Vector3(0f, rot, 0f));
			foreach(var spawn in spawnPoints){
				spawn.Rotated(baseRotate);
			}
		}		
		lastFrameRotate = baseRotate;
	}
	
	public void AddMoney(int amount){
		currentMoney += amount;
	}
}
