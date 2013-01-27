using UnityEngine;
using System.Collections.Generic;

public class UnitMovement : MonoBehaviour
{
	
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
	public float burstRadius = 0;
	private float killTime = Mathf.Infinity;
	private int takeDamage = 0;
	private float lastPerceiveTime = -100.0f;
	private GameObject enemyBase;
	private float currentSpeed;
	public bool attacking;
	
	// Use this for initialization
	void Start ()
	{
		this.gameObject.transform.position = path.nodes [nodeIndex].position;
		if ( path.nodes.Count > nodeIndex + 1 )
			this.gameObject.transform.LookAt( path.nodes[nodeIndex+1].position );
		isPlayerUnit = this.tag == "PlayerUnit";
		
		if (!isPlayerUnit) {
			HideGameObject (true, gameObject);	
		}
		
		if (isPlayerUnit) {
			enemyBase = GameObject.FindGameObjectWithTag ("EnemyBase");
		} else {
			enemyBase = GameObject.FindGameObjectWithTag ("PlayerBase");	
		}
		
		attacking = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		attacking = false;
		
		if (killTime < Time.time) {
			GameObject.Destroy (gameObject);	
		}
		
		if (health <= 0)
			return;
		
		if (targetEnemy == null) {
			var enemies = Utilities.FindObjectsWithinRange (this.transform.position, isPlayerUnit ? "EnemyUnit" : "PlayerUnit", perceptRadius);
			
			if (enemies != null && enemies.Length > 0) {
				float closestDistance = Mathf.Infinity;
				foreach (var enemy in enemies) {
					enemy.GetComponent<UnitMovement> ().lastPerceiveTime = Time.time;
					if (Vector3.Distance (enemy.transform.position, this.transform.position) < closestDistance) {
						closestDistance = Vector3.Distance (enemy.transform.position, this.transform.position);
						targetEnemy = enemy;
					}
				}
			}
		}
		
		if (targetEnemy != null) {
			if (InRangeToAttack (targetEnemy)) {
				// attack enemy
				Attack (targetEnemy);	
			} else {
				// not in range to attack, move towards target enemy
				MoveTo (targetEnemy);
			}
		} else {
			// are we near the base?
			if (Vector3.Distance (this.transform.position, enemyBase.transform.position) < perceptRadius) {
				// move towards the enemy base
				MoveTo (enemyBase);
				
				if (InRangeToAttack (enemyBase)) {
					// attack the enemy base
					Attack (enemyBase);
				}
			} else {			
				// move towards next path node
				MoveTo (path.nodes [nodeIndex]);
			}
		}
		
		// reach node, target next one
		if (Vector3.Distance (this.gameObject.transform.position, path.nodes [nodeIndex].transform.position) < nodeRadius) {
			if (nodeIndex < path.nodes.Count - 1)
				nodeIndex++;
		}
		
		TakeDamage ();
		
		if (!isPlayerUnit) {
			HideGameObject (Time.time - lastPerceiveTime < 3.0f, gameObject);
		}
	}
	
	public bool IsVisible ()
	{
		if(gameObject.renderer == null)
			return false;
		return gameObject.renderer.enabled;
	}
	
	void HideGameObject (bool hide, GameObject hideMe)
	{
		if (hideMe.renderer != null)
			hideMe.renderer.enabled = hide;
		
		foreach (Transform child in hideMe.transform) {
			HideGameObject (hide, child.gameObject);	
		}
	}
	
	void Face(GameObject other)
	{
		Face (other.transform);
	}
	
	void Face(Transform other)
	{
		Face (other.position);	
	}
	
	void Face(Vector3 other)
	{
		// turn us to face other, at a rate of rotateSpeed
		
		Vector3 usToThem =  other - transform.position;
		usToThem.y = 0.0f;
		usToThem.Normalize();
		
		if ( usToThem.sqrMagnitude > 0.0f )
		{
			Quaternion targetRot = Quaternion.LookRotation(usToThem, Vector3.up);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed);
		}
	}
	
	void MoveTo (Transform other)
	{
		MoveTo (other.position);
	}
	
	void MoveTo (GameObject other)
	{
		MoveTo (other.transform);	
	}
	
	void MoveTo (Vector3 other)
	{
		Face ( other );
		
		//gameObject.transform.rigidbody.AddForce(gameObject.transform.forward * moveSpeed, ForceMode.VelocityChange);
		
		this.gameObject.transform.position = Vector3.MoveTowards (this.gameObject.transform.position, other, moveSpeed);
	}
	
	// checks if we are in range and facing the enemy
	bool InRangeToAttack (GameObject enemy)
	{
		float distance = Vector3.Distance (transform.position, enemy.transform.position);
		float angle = Vector3.Angle (transform.forward, enemy.transform.position - transform.position);
		
		if (angle < 30.0f) {
			RadiusScript enemyRadiusScript = enemy.GetComponent<RadiusScript> ();
			RadiusScript ourRadiusScript = gameObject.GetComponent<RadiusScript> ();
			
			if (enemyRadiusScript != null)
				distance -= enemyRadiusScript.radius;
			if (ourRadiusScript != null)
				distance -= ourRadiusScript.radius;
			
			distance -= attackRadius;
			
			return distance < 0.0f;
		}
		
		return false;
	}
	
	void StopMoving()
	{
		//gameObject.rigidbody.velocity = Vector3.zero;
	}
	
	
	void Attack (GameObject enemy)
	{
		StopMoving();
		attacking = true;
		
		if (nextAttackTime < Time.time) {
			// attack
			UnitMovement unitMovement = enemy.GetComponent<UnitMovement> ();
			PlayerScript playerScript = enemy.GetComponent<PlayerScript> ();
			
			if (unitMovement != null) {
				//Handle AE burst
				if (burstRadius <= 0)
					unitMovement.Attacked (damage);
				else {		
					var enemies = Utilities.FindObjectsWithinRange (this.transform.position, isPlayerUnit ? "EnemyUnit" : "PlayerUnit", burstRadius);					
					foreach (var other in enemies) {
						var otherMove = other.GetComponent<UnitMovement> ();
						if (otherMove != null) {
							otherMove.Attacked (damage);
						}
					}
				}
			}
			if (playerScript != null)
				playerScript.Attacked (damage);			
			
			GameObject effect = (GameObject)Instantiate (attackParticleEffect, this.transform.position, this.transform.rotation);
			effect.transform.parent = gameObject.transform;
			effect.transform.localPosition = new Vector3 (0.0f, 0.0f, 1.0f);
			GameObject.Destroy (effect, 0.5f);
			nextAttackTime = Time.time + attackInterval;
		}
	}
	
	public void Attacked (int damage)
	{
		takeDamage += damage;
	}
	
	void TakeDamage ()
	{
		if (health > 0 && takeDamage > 0) {
			health -= takeDamage;
			
			if (health <= 0) {
				GameObject effect = (GameObject)Instantiate (deathParticleEffect, this.transform.position, this.transform.rotation);
				GameObject.Destroy (effect, 1.0f);
				killTime = Time.time + 1.0f;
			}
		}
		
		takeDamage = 0;
	}
}
