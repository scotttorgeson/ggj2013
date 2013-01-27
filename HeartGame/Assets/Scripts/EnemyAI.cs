using UnityEngine;
using System.Collections.Generic;

/*Scout > sniper
Scatter > scout
Heavy > Scatter
Sniper > heavy
Regular is regular, ie bad against everything
*/

public struct EnemyInfo
{
	public string pathName;
	public string unitType;
	public EnemyInfo(string pname, string utype)
	{
		pathName = pname;
		unitType = utype;
	}
}

public struct UpdateEnemyInfo
{
	public List<EnemyInfo> infos;
	public float time;
}

public class EnemyAI : MonoBehaviour {	
	
	public PlayerScript enemyPlayerScript;
	public PlayerScript ourPlayerScript;
	
	List<EnemyInfo> enemyInfos;
	Queue<UpdateEnemyInfo> updateEnemyInfos;
	
	public float updateInfoInterval = 1;
	public float updateInfoDelay = 10;
	float nextInfoUpdate;
	
	public float actInterval = 5;
	float nextActTime;
	
//	public static System.Collections.Generic.Dictionary<string, string> counterDictionary = new System.Collections.Generic.Dictionary<string, string>()
//		{
//			{ "Scout", "Sniper" },
//			{ "Scatter", "Scout" },
//			{ "Heavy", "Scatter" },
//			{ "Sniper", "Heavy" },
//		};
	
	static float[,] counterTable =
	{
		{ 0, -5, 10, -10, -10 },
		{ 5, 0, -10, 10, -10 },
		{ -10, 10, 0, -5, -10 },
		{ 10, -10, 5, 0, -10 },
		{ 10, 10, 10, 10, 0 },
	};
	
	static Dictionary<string, int> counterTableIndex = new Dictionary<string, int>()
	{
		{ "Sniper", 0 },	
		{ "Scatter", 1 },
		{ "Scout", 2 },
		{ "Heavy", 3 },
		{ "Regular", 4 },
	};
	
	void Start()
	{
		updateEnemyInfos = new Queue<UpdateEnemyInfo>();
		enemyInfos = GetEnemyInfo();
		nextInfoUpdate = Time.time + updateInfoInterval;
		nextActTime = Time.time + actInterval;
	}
	
	List<EnemyInfo> GetEnemyInfo()
	{
		List<EnemyInfo> list = new List<EnemyInfo>(5);
		foreach ( Spawner spawnPoint in enemyPlayerScript.spawnPoints )
		{
			EnemyInfo info = new EnemyInfo();
			info.unitType = spawnPoint.currentUpgrade.unitName;
			info.pathName = spawnPoint.GetCurrentPathName();
			list.Add(info);
		}
		return list;
	}
	
	void UpdateInfo()
	{
		if ( Time.time > nextInfoUpdate )
		{
			nextInfoUpdate = Time.time + updateInfoInterval;
			UpdateEnemyInfo updateEnemyInfo = new UpdateEnemyInfo();
			updateEnemyInfo.infos = GetEnemyInfo();
			updateEnemyInfo.time = Time.time + updateInfoDelay;
			updateEnemyInfos.Enqueue(updateEnemyInfo);
		}
		
		
		if ( updateEnemyInfos.Count > 0 && Time.time > updateEnemyInfos.Peek().time )
		{
			enemyInfos = updateEnemyInfos.Dequeue().infos;
		}
	}
	
	void Update()
	{
		UpdateInfo();
		
		Act();
	}
	
	private float[] orientationScores = new float[5];
	private float[] pathScores = new float[3];
	
	private Dictionary<string, int> pathNameToIndex = new Dictionary<string, int>()
	{
		{ "North", 0 },
		{ "Middle", 1 },
		{ "South", 2 },
	};
	
	string GetEnemyUnitTypeOnPath(string pathName)
	{
		foreach(EnemyInfo enemyInfo in enemyInfos)
		{
			if ( enemyInfo.pathName != null && pathName != null )
			{
				if ( enemyInfo.pathName == pathName )
					return enemyInfo.unitType;
			}
		}
		
		Debug.LogError("ERROR NO ENEMY ON PATH: " + pathName);
		return null;
	}
	
	string GetFriendlyUnitOnPath(string pathName, int rotation)
	{
		foreach( Spawner spawnPoint in ourPlayerScript.spawnPoints )
		{
			string otherPathName = spawnPoint.PathIfRotated(rotation);
			
			if ( otherPathName != null && pathName != null )
			{
				if ( pathName == otherPathName )
				{
					return spawnPoint.currentUpgrade.unitName;
				}
			}
		}
		
		Debug.LogError("ERROR NO FRIENDLY ON PATH: " + pathName + ' ' + rotation);
		return null;
	}
	
	float ScorePath(string pathName, int rotation)
	{
		string enemyUnit = GetEnemyUnitTypeOnPath(pathName);
		string friendlyUnit = GetFriendlyUnitOnPath(pathName, rotation);
		
		int enemyUnitIndex = counterTableIndex[enemyUnit];
		int friendlyUnitIndex = counterTableIndex[friendlyUnit];
		return counterTable[enemyUnitIndex, friendlyUnitIndex];
	}
	
	void UpdateScores()
	{
		for ( int i = 0; i < 5; i++ )
		{
			orientationScores[i] = 0.0f;
			
			foreach(KeyValuePair<string, int> pathNameIndex in pathNameToIndex)
			{
				pathScores[pathNameIndex.Value] = ScorePath ( pathNameIndex.Key, i );
				orientationScores[i] += pathScores[pathNameIndex.Value];
			}
		}
	}
	
	void CheckRotate()
	{
		int bestRotation = 0;
		float bestScore = Mathf.NegativeInfinity;
		for ( int i = 0; i < 5; i++ )
		{
			if ( orientationScores[i] > bestScore )
			{
				bestScore = orientationScores[i];
				bestRotation = i;
			}
		}
		
		if ( bestRotation != 0 )
		{
			ourPlayerScript.RotateDirection( bestRotation );	
		}
	}
	
	void CheckUpgrade()
	{
		float worstScore = Mathf.Infinity;
		int worstIndex = 0;
		
		// find worst path score
		for(int i = 0; i < 3; i++)
		{
			if ( pathScores[i] < worstScore )
			{
				worstIndex = i;
				worstScore = pathScores[i];
			}
			else if ( Mathf.Approximately( pathScores[i], worstScore ) )
			{
				if ( Random.value > 0.5f )
				{
					worstIndex = i;
					worstScore = pathScores[i];
				}
			}
		}
		
		if ( !TryToUpgradePath(worstIndex) ) // try the worst scored path
		{
			TryToUpgradePath(Random.Range(0, 2)); // try a random path
		}
	}
	
	bool TryToUpgradePath(int pathIndex) // false if they should try again with another path
	{
		string thePathName = null;
		
		// get the name of the worst path
		foreach(KeyValuePair<string,int> kvp in pathNameToIndex)
		{
			if ( kvp.Value == pathIndex )
				thePathName = kvp.Key;
		}
		
		// get the spawner on the worst path
		Spawner spawner = null;
		foreach(Spawner spawnPoint in ourPlayerScript.spawnPoints)
		{
			string otherPathName = spawnPoint.GetCurrentPathName();
			if ( otherPathName != null )
			{
				if ( otherPathName == thePathName )
				{
					spawner = spawnPoint;
					break;
				}
			}
		}
		
		// try to upgrade this spawner
		if ( spawner != null )
		{
			string enemyUnitType = GetEnemyUnitTypeOnPath(thePathName);
			
			UnitUpgrade chosenUpgrade = null;
			float bestUpgradeScore = Mathf.NegativeInfinity;
			foreach(UnitUpgrade upgrade in spawner.currentUpgrade.upgrades)
			{
				int enemyUnitIndex = counterTableIndex[enemyUnitType];
				int friendlyUnitIndex = counterTableIndex[upgrade.unitName];
				
				if ( counterTable[enemyUnitIndex, friendlyUnitIndex] > bestUpgradeScore )
				{
					bestUpgradeScore = counterTable[enemyUnitIndex, friendlyUnitIndex];
					chosenUpgrade = upgrade;
				}
				else if (  Mathf.Approximately( counterTable[enemyUnitIndex, friendlyUnitIndex], bestUpgradeScore ) )
				{
					if ( Random.value > 0.5f )
					{
						bestUpgradeScore = counterTable[enemyUnitIndex, friendlyUnitIndex];
						chosenUpgrade = upgrade;
					}
				}
			}
			
			if ( chosenUpgrade == null )
				return false;
			
			if ( chosenUpgrade.cost < ourPlayerScript.currentMoney )
			{
				// buy the upgrade
				ourPlayerScript.Upgrade(spawner, chosenUpgrade);
			}
			
			return true;
		}
		else
		{
			Debug.LogError("CANNOT FIND WORST SPAWNER");
			return false;
		}
	}
	
	public int clusterSearchCount = 10;
	public int clusterSize = 10;
	public float clusterRadius = 100.0f;
	
	GameObject[] FindCluster(string tag)
	{
		GameObject[] units = GameObject.FindGameObjectsWithTag(tag);
		for ( int i = 0; i < clusterSearchCount; i++ )
		{
			int index = Random.Range(0, units.Length);
			GameObject[] cluster = Utilities.FindObjectsWithinRange(units[index].transform.position, tag, clusterRadius);
			
			if ( cluster.Length > clusterSize )
				return cluster;
		}
		
		return null; // no cluster found
	}
	
	void CheckSuperPowers()
	{
		string enemyTag = tag == "PlayerBase" ? "EnemyUnit" : "PlayerUnit";
		string friendlyTag = tag == "PlayerBase" ? "PlayerUnit" : "EnemyUnit";
		
		
		float whatPower = Random.value * 100.0f;
		
		bool usedPower = false;
		if ( whatPower > 66 )
		{
			// try enemy cluster
			GameObject[] enemyCluster = FindCluster(enemyTag);
		}
		 if ( ( whatPower < 66 && whatPower > 33 ) || !usedPower )
		{
			// try friendly cluster	
			GameObject[] friendlyCluster = FindCluster(tag);
		}
		
		if ( !usedPower )
		{
			// try buff/stall
			whatPower = Random.value * 100.0f;
			
			if ( whatPower > 50 )
			{
				// buff	
			}
			else
			{
				// stall	
			}
		}
		
		
	}
	
	void Act()
	{
		if ( nextActTime < Time.time )
		{
			nextActTime = Time.time + actInterval;
			UpdateScores();
			CheckRotate();
			CheckUpgrade();
			CheckSuperPowers();
		}
	}
}
