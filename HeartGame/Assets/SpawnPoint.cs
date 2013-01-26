using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
	public GameObject spawnPrefab;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Spawn(){
		if(spawnPrefab !=null)
			Instantiate(spawnPrefab, gameObject.transform.position, Quaternion.identity);
	}
}
