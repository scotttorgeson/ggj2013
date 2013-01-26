using UnityEngine;
using System.Collections.Generic;

public class UnitMovement : MonoBehaviour {
	
	public Path path;
	public int nodeIndex = 0;
	public float moveSpeed = 50;
	public float nodeRadius = 3.0f;
	bool isPlayerUnit;
	public float perceptRadius = 100.0f;
	public float attackRadius = 10.0f;
	public float rotateSpeed = Mathf.PI;
	public float attackInterval = 1.0f;
	
	private GameObject targetEnemy = null;
	private float nextAttackTime = 0.0f;
	
	public GameObject attackParticleEffect;
	public GameObject deathParticleEffect;
	public int health = 30;
	public int damage = 10;
	
	private float killTime = Mathf.Infinity;
	
	// Use this for initialization
	void Start () {
		this.gameObject.transform.position = path.nodes[nodeIndex].position;
		isPlayerUnit = this.tag == "PlayerUnit";
	}
	
	// Update is called once per frame
	void Update () {
		
		if ( killTime < Time.time )
		{
			GameObject.Destroy( gameObject );	
		}
		
		if ( health <= 0 )
			return;
		
		if ( targetEnemy == null )
		{
			var enemies = Utilities.FindObjectsWithinRange(this.transform.position, isPlayerUnit ? "EnemyUnit" : "PlayerUnit", perceptRadius);
			if ( enemies != null && enemies.Length > 0 )
			{
				float closestDistance = Mathf.Infinity;
				foreach( var enemy in enemies )
				{
					if ( Vector3.Distance(enemy.transform.position, this.transform.position) < closestDistance )
					{
						closestDistance = Vector3.Distance(enemy.transform.position, this.transform.position);
						targetEnemy = enemy;
					}
				}
			}
		}
		
		if ( targetEnemy != null )
		{
			float distance = Vector3.Distance(this.transform.position, targetEnemy.transform.position);
			if ( distance < attackRadius )
			{
				// attack enemy
				Attack( targetEnemy );	
			}
			else
			{
				// move towards target enemy
				this.gameObject.transform.position = Vector3.MoveTowards( this.gameObject.transform.position, targetEnemy.gameObject.transform.position, Time.deltaTime * moveSpeed );				
			}			
			
			gameObject.transform.LookAt( targetEnemy.transform );
		}
		else
		{		
			// move towards next path node
			this.gameObject.transform.position = Vector3.MoveTowards( this.gameObject.transform.position, path.nodes[nodeIndex].transform.position, Time.deltaTime * moveSpeed );
			gameObject.transform.LookAt( path.nodes[nodeIndex].transform );
		}
		
		// reach node, target next one
		if ( Vector3.Distance(this.gameObject.transform.position, path.nodes[nodeIndex].transform.position) < nodeRadius )
		{
			if ( nodeIndex < path.nodes.Count - 1 )
				nodeIndex++;
		}
	}
	
	void Attack( GameObject enemy )
	{
		if ( nextAttackTime < Time.time )
		{
			// attack
			enemy.GetComponent<UnitMovement>().Attacked( damage );
			
			GameObject effect = (GameObject)Instantiate( attackParticleEffect, this.transform.position, this.transform.rotation );
			effect.transform.parent = gameObject.transform;
			effect.transform.localPosition = new Vector3(0.0f, 0.0f, 1.0f);
			GameObject.Destroy( effect, 0.5f );
			nextAttackTime = Time.time + attackInterval;
		}
	}
	
	void Attacked( int damage )
	{
		if ( health > 0 )
		{
			health -= damage;
			
			if ( health <= 0 )
			{
				GameObject effect = (GameObject)Instantiate( deathParticleEffect, this.transform.position, this.transform.rotation );
				GameObject.Destroy( effect, 1.0f );
				killTime = Time.time + 1.0f;
			}
		}
	}
}
