using UnityEngine;
using System.Collections.Generic;

public struct UnitUpgrade {
	public UnitMovement unitSpawn;
	
	public GUIStyle upgradeButtonStyle;
	public GUIContent upgradeButtonContent;
	public Rect upgradeButtonRect;
}

public class Spawner : MonoBehaviour {
	
	public List<Path> pathes;
	public int pathIndex = 0;
	public const int pathCount = 5;
	public List<GameObject> unitSpawns;
	public int tier = 0;
	
	public UnitUpgrade[] upgrades;	
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void Spawn()
	{
		if ( pathIndex < pathes.Count )
		{
			GameObject newUnit = (GameObject)Instantiate(unitSpawns[tier], transform.position, transform.rotation);
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
	
	public void Upgrade()
	{
		if ( tier < unitSpawns.Count - 1 )
		{
			tier++;
		}
	}
}
