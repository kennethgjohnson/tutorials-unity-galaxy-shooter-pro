using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using vio.spaceshooter.game.player;
using vio.spaceshooter.game.spawnmanager;

namespace vio.spaceshooter.game.powerups
{
  public class UniversalBombPowerup : Powerup
  {

    void Start()
    {
      
    }

    protected override void applyPowerupToPlayer(Player player)
    {
      player.fireShockwave();
    }
  }
}