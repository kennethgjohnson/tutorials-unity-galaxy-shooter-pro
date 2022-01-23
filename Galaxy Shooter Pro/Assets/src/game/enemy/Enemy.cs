using UnityEngine;
using vio.spaceshooter.game.player;

namespace vio.spaceshooter.game.enemy
{
  public class Enemy : MonoBehaviour
  {
    private const float DEFAULT_ENEMY_SPEED = 4f;
    private const float MIN_Y_POS = -2.5f;
    private const float MAX_Y_POS = 12f;
    private const float MAX_X_POS = 10.00f;
    private const float MIN_X_POS = -9.75f;
    private int damageAmount = 1;
    private float speed = DEFAULT_ENEMY_SPEED;
    private int updateCount = 0;
    private const int updatesPer360Degrees = 500;
    private float spawnX;
    private bool isLateralMovementOn = false;
    void Update()
    {
      this.moveEnemy();
      if (this.isEnemyPastBottomOfPlayArea())
      {
        this.moveEnemyToRandomXLocationAtTop();
      }
    }
    private void moveEnemy()
    {
      Vector2 downward = Vector2.down * speed * Time.deltaTime;
      if (this.isLateralMovementOn) {
        this.updateCount++;
        float positionInCycle = (float)updateCount / (float)updatesPer360Degrees;
        // 6.28319 - 360 degrees
        float displacement = positionInCycle * 6.28319f;
        Vector2 lateral = new Vector2(Mathf.Sin(displacement) / UnityEngine.Random.Range(20, 100), 0);
        this.transform.Translate(downward + lateral);
      } else
      {
        this.transform.Translate(downward);
      }
    }

    private bool isEnemyPastBottomOfPlayArea()
    {
      return (this.transform.position.y < MIN_Y_POS);
    }

    private void moveEnemyToRandomXLocationAtTop()
    {
      this.transform.position = new Vector3(UnityEngine.Random.Range(MIN_X_POS, MAX_X_POS), MAX_Y_POS);
      if (isLateralMovementOn)
      {
        this.updateCount = UnityEngine.Random.Range(0, 630);
      }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
      switch (other.tag)
      {
        case "Laser":
          laserHit(other);
          break;

        case "Player":
          playerHit(other);
          break;
      }
    }
    private void laserHit(Collider2D other)
    {
      this.GetComponentInParent<Game>().IncreaseScore(10);
      Destroy(other.gameObject);
      this.speed = this.speed * UnityEngine.Random.Range(0f, 0.5f);
      this.GetComponent<PolygonCollider2D>().enabled = false;
      this.GetComponent<Animator>().SetTrigger("OnEnemyDeath");
      Destroy(this.gameObject, 1.1f);
    }

    private void playerHit(Collider2D other)
    {
      Player player = other.GetComponent<Player>();
      if (player != null)
      {
        player.Damage(this.gameObject, this.damageAmount);
      }

      this.speed = this.speed * UnityEngine.Random.Range(0f, 0.25f);
      this.GetComponent<PolygonCollider2D>().enabled = false;
      this.GetComponent<Animator>().SetTrigger("OnEnemyDeath");

      Destroy(this.gameObject, 1.1f);
    }

    public void SetLateralMovement(bool state)
    {
      this.isLateralMovementOn = state;
    }
  }
}