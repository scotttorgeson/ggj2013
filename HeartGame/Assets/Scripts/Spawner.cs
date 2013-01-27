using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{	
	public List<Path> pathes;
	public int pathIndex = 0;
	public const int pathCount = 5;
	public string spawnTag;
	public UnitUpgrade currentUpgrade;
	
	public string GetCurrentPathName()
	{
		if ( pathIndex < pathes.Count )
		{
			return pathes[pathIndex].pathName;
		}
		
		return null;
	}
	
	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
	
	public void Spawn ()
	{
		if (pathIndex < pathes.Count) {
			for (int i=0; i<currentUpgrade.spawnNumber; i++) {
				currentUpgrade.spawnUnit.tag = spawnTag;				
				GameObject newUnit = (GameObject)Instantiate (currentUpgrade.spawnUnit, transform.position, transform.rotation);
				newUnit.name += pathIndex;
				UnitMovement unitMovement = newUnit.GetComponent<UnitMovement> ();
				unitMovement.path = pathes [pathIndex];
			}
		}
	}
	
	public void Rotated (int direction)
	{
		pathIndex += direction;
		if (pathIndex < 0)
			pathIndex = pathIndex += pathCount;
		if (pathIndex >= pathCount)
			pathIndex = pathIndex -= pathCount;
	}
	
	public string PathIfRotated(int direction)
	{
		int testPathIndex = pathIndex + direction;
		if (testPathIndex < 0)
			testPathIndex = testPathIndex += pathCount;
		if (testPathIndex >= pathCount)
			testPathIndex = testPathIndex -= pathCount;
		if ( testPathIndex < pathes.Count )
		{
			return pathes[testPathIndex].pathName;
		}
		
		return null;
	}
}
