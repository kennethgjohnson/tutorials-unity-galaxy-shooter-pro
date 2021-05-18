using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vio.spaceshooter.player.weapon
{
  public abstract class PlayerWeapon
  {
    protected GameObject weaponPrefab;
    protected Player player;
    public PlayerWeapon(Player player, GameObject weaponPrefab)
    {
      this.player = player;
      this.weaponPrefab = weaponPrefab;      
    }

    abstract public void AttemptFire();
  }
}
