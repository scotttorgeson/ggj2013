using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	public int currentMoney;
	public int currentLife;
	
	public GameObject playerBase;
	public int startingMoney;
	public int startingLife;
	
	// Use this for initialization
	void Start () {
		currentMoney = startingMoney;
		currentLife = startingLife;
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void AddMoney(int amount){
		currentMoney += amount;
	}
}
