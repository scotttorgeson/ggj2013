using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
	
	public List<Path> pathes;
	public int pathIndex = 0;
	public const int pathCount = 5;
	public GameObject unitSpawn;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if ( Time.frameCount % 300 == 0 )
			Spawn ();
	}
	
	public void Spawn()
	{
		if ( pathIndex < pathes.Count )
		{
			GameObject newUnit = (GameObject)Instantiate(unitSpawn, transform.position, transform.rotation);
			newUnit.name = "SpawnedUnit" + pathIndex;
			UnitMovement unitMovement = newUnit.GetComponent<UnitMovement>();
			unitMovement.path = pathes[pathIndex];
		}
	}
	
	public void Rotated(int direction)
	{
		pathIndex += direction;
		if ( pathIndex < 0 )
			pathIndex = pathCount - 1;
		if ( pathIndex >= pathCount )
			pathIndex = 0;
	}
}
