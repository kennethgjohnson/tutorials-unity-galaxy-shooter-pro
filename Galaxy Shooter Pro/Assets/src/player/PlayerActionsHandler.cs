using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vio.spaceshooter.player
{
  class PlayerActionsHandler
  {
    private readonly Player player;
    public PlayerActionsHandler(Player player)
    {
      this.player = player;
    }

    // Update is called once per frame
    public void Update()
    {
      if (this.isPlayerFiring())
      {
        this.player.GetWeapon().AttemptFire();
      }
    }

    private bool isPlayerFiring()
    {
      return Input.GetKey(KeyCode.Space);
    }
  }
}
