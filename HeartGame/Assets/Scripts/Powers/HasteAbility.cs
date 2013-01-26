using UnityEngine;
using System.Collections;

public class HasteAbility : UnitAbility {
	public float moveSpeedMultiplier = 10;
	public float atkSpeedMultiplier = 100;
	
	protected override void ApplyEffect (UnitMovement unit)
	{
		unit.attackInterval *= atkSpeedMultiplier;
		unit.moveSpeed *= moveSpeedMultiplier;
	}
	
	protected override void UnApplyEffect (UnitMovement unit)
	{
		unit.attackInterval /= atkSpeedMultiplier;
		unit.moveSpeed /= moveSpeedMultiplier;
	}
}
