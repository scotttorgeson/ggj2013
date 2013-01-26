using UnityEngine;
using System.Collections;

public class HealAbility : UnitAbility {
	public int healAmount;
	
	protected override void ApplyEffect (UnitMovement unit)
	{
		unit.Attacked(-healAmount);
	}
	
	protected override void UnApplyEffect (UnitMovement unit)
	{
	}
}
