using UnityEngine;
using System.Collections.Generic;

public class UnitMovement : MonoBehaviour {
	
	public Path path;
	public int nodeIndex = 0;
	public float moveSpeed = 50;
	public float nodeRadius = 3.0f;
	
	// Use this for initialization
	void Start () {
		this.gameObject.transform.position = path.nodes[nodeIndex].position;
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.position = Vector3.MoveTowards( this.gameObject.transform.position, path.nodes[nodeIndex].transform.position, Time.deltaTime * moveSpeed );
		if ( Vector3.Distance(this.gameObject.transform.position, path.nodes[nodeIndex].transform.position) < nodeRadius )
		{
			if ( nodeIndex < path.nodes.Count - 1 )
				nodeIndex++;
		}
	}
}
