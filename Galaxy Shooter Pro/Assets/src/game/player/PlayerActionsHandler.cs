using UnityEngine;

namespace vio.spaceshooter.game.player
{
  class PlayerActionsHandler
  {
    private readonly Player player;
    public PlayerActionsHandler(Player player)
    {
      this.player = player;
    }
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
