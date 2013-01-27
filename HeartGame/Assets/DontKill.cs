using UnityEngine;
using System.Collections;

public class DontKill : MonoBehaviour {
	public AudioClip music;
	
	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad(gameObject);
	}
}
