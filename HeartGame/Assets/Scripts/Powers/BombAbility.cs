using UnityEngine;
using System.Collections;

public class BombAbility : UnitAbility {
	public int bombDamage;
	
	protected override void ApplyEffect (UnitMovement unit)
	{
		unit.Attacked(bombDamage);
	}
	
	protected override void UnApplyEffect (UnitMovement unit)
	{		
	}
}
