using UnityEngine;
using System.Collections.Generic;

public abstract class UnitAbility : MonoBehaviour {
	public float bombRadius = 1000;
	public string playerTag = "EnemyUnit";
	public float bombDuration = 50;
	
	private List<UnitMovement> targetList = new List<UnitMovement>();
	private float endTime;
	
	// Use this for initialization
	void Start () {
		endTime = Time.time + bombDuration;
		
		//Find everything in the radius
		var targets = Utilities.FindObjectsWithinRange(gameObject.transform.position, playerTag, bombRadius);
		
		foreach(var target in targets){
			var unit = target.GetComponent<UnitMovement>();
			if(unit !=null) {
				targetList.Add (unit);
				ApplyEffect(unit);
			}
		}
		Debug.Log (System.String.Format("UnitAbility Start({0}) affected {1} units with radius {2}", this, targetList.Count, bombRadius));
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time >= endTime){
			foreach(var unit in targetList){
				UnApplyEffect(unit);
			}
			
			GameObject.Destroy(gameObject);
		}
	}
	
	protected abstract void ApplyEffect(UnitMovement unit);
	protected abstract void UnApplyEffect(UnitMovement unit);
}
