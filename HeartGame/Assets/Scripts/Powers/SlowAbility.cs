using UnityEngine;
using System.Collections;

public class SlowAbility : UnitAbility {
	public float speedMod;
	
	protected override void ApplyEffect (UnitMovement unit)
	{
		unit.moveSpeed -= speedMod;
	}
	
	protected override void UnApplyEffect (UnitMovement unit)
	{
		unit.moveSpeed += speedMod;
	}
}
