using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ProjectileStats 
{
    [SerializeField] 
    private typeOfDamage damageType;
    
    public enum typeOfDamage
    { Bullet, Canon, Rocket }
}
