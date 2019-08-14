using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDebuff : Debuff
{


    public override void ApplyDebuff()
    {
        throw new System.NotImplementedException();
    }

    public BulletDebuff(float duration, int procChance, Enemy target, GameManager.typeOfDamage debuffType) : base(duration, procChance, target, debuffType)
    {
    }
}
