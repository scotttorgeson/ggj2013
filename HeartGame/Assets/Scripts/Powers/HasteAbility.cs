using UnityEngine;
using System.Collections;

public class NewBehaviourScript : UnitAbility {
	public float moveSpeedMultiplier;
	public float atkSpeedMultiplier;
	
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
