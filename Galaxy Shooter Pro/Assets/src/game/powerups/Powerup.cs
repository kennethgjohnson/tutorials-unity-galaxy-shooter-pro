using UnityEngine;
using vio.spaceshooter.game.player;

namespace vio.spaceshooter.game.powerups
{
  public abstract class Powerup : MonoBehaviour
  {
    private const float DEFAULT_SPEED = 5f;
    private const float MIN_Y_POS = -2.5f;

    void Update()
    {
      this.applyMovement();
      if (this.isOutOfBoundry())
      {
        this.removeObject();
      }
    }

    private void applyMovement()
    {
      this.transform.Translate(
                  Vector3.down
                  * this.getPosibleDistanceMovedSinceLastFrame()
              );
    }

    private float getPosibleDistanceMovedSinceLastFrame()
    {
      return DEFAULT_SPEED * Time.deltaTime;
    }

    private bool isOutOfBoundry()
    {
      return (this.transform.position.y < MIN_Y_POS);
    }

    private void removeObject()
    {
      Destroy(this.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      switch (other.tag)
      {
        case "Player":
          handlePlayerCollision(other);
          break;
      }
    }

    private void handlePlayerCollision(Collider2D other)
    {
      Player player = other.GetComponent<Player>();
      if (player != null)
      {
        this.applyPowerupToPlayer(player);
      }
      Destroy(this.gameObject);
    }

    protected abstract void applyPowerupToPlayer(Player player);
  }
}