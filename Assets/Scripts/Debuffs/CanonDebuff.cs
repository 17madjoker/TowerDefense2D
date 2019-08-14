using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonDebuff : Debuff
{


    public override void ApplyDebuff()
    {
        throw new System.NotImplementedException();
    }

    public CanonDebuff(float duration, int procChance, Enemy target, GameManager.typeOfDamage debuffType) : base(duration, procChance, target, debuffType)
    {
    }
}
