using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
   public void Damage(int damageAmount);

    public bool HasTakenDamage {  get; set; }
}
