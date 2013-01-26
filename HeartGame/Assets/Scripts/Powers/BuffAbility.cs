using UnityEngine;
using System.Collections.Generic;

public class BuffAbility : UnitAbility {
	public int strengthMod;
	
	protected override void ApplyEffect (UnitMovement unit)
	{
		unit.damage += strengthMod;
	}
	
	protected override void UnApplyEffect (UnitMovement unit)
	{
		unit.damage -= strengthMod;
	}
}
