using UnityEngine;
using System.Collections.Generic;

public class Path : MonoBehaviour {
	
	public static List<Path> Pathes = new List<Path>();
	
	public List<Transform> nodes;

	// Use this for initialization
	void Start () {
		Pathes.Add(this);
	}	
}
