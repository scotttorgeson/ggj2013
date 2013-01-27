using UnityEngine;
using System.Collections;

public class UnitUpgrade : MonoBehaviour {
	public GameObject spawnUnit;
	public UnitUpgrade[] upgrades;
	public int cost;
	public int spawnNumber=2;
	
	public GUIStyle upgradeButtonStyle;
	public GUIContent upgradeButtonContent;
	public Rect upgradeButtonRect;
	public string unitName;
}
