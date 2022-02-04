using System;
using UnityEngine;
using vio.spaceshooter.game.asteroid;
using vio.spaceshooter.game.player;

namespace vio.spaceshooter.game.player
{
  public class PlayerThruster : MonoBehaviour
  {
    [SerializeField]
    private bool thrustOn = true;

    void Update()
    {
      if ((Input.GetAxis("Vertical") > 0) && (!this.thrustOn))
      {
        this.startThrusters();
      }
      else if ((Input.GetAxis("Vertical") <= 0) && (this.thrustOn))
      {
        this.idleThrusters();

      }
    }

    private void idleThrusters()
    {
      this.transform.localScale = new Vector3(0.25f, 0.25f, 0.5f);
      this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.25f, this.transform.position.z);
      this.thrustOn = false;
    }

    private void startThrusters()
    {
      this.transform.localScale = new Vector3(0.25f, 0.5f, 0.5f);
      this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.25f, this.transform.position.z);
      this.thrustOn = true;
    }

  }
}
