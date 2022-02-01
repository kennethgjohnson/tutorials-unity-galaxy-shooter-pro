using UnityEngine;
using vio.spaceshooter.game.asteroid;
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
    private int difficultyLevel = 0;
    private float displacementFactor;
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
      switch (this.difficultyLevel)
      {
        case 1:
          this.moveEnemyMode1();
          break;

        case 2:
          this.moveEnemyMode2();
          break;

        case 3:
          this.moveEnemyMode3();
          break;

        default:
          this.moveEnemyMode0();
          break;
      }
    }

    private void moveEnemyMode0()
    {
      Vector2 downward = Vector2.down * speed * Time.deltaTime;
      this.transform.Translate(downward);
    }

    private void moveEnemyMode1()
    {
      this.updateDisplacementFactor();
      Vector2 lateral = this.calculateLateralVector(80);
      Vector2 downward = calculateNormalDownwardSpeedVector();
      this.transform.Translate(downward + lateral);
    }

  private void updateDisplacementFactor()
    {
      this.updateCount++;
      float positionInCycle = (float)updateCount / (float)updatesPer360Degrees;
      // 6.28319 - 360 degrees
      this.displacementFactor = positionInCycle * 6.28319f;
    }

    /// <summary>
    ///  Calculates the lateralVector
    /// </summary>
    /// <param name="maxLateralFactor">division factor, less is more lateral movement</param>
    /// <returns></returns>
    private Vector2 calculateLateralVector(int maxLateralFactor)
    {
      return new Vector2(Mathf.Sin(this.displacementFactor) / UnityEngine.Random.Range(maxLateralFactor, 100), 0);
    }

    private Vector2 calculateNormalDownwardSpeedVector()
    {
      return Vector2.down * this.speed * Time.deltaTime;
    }

    private void moveEnemyMode2()
    {
      this.updateDisplacementFactor();
      Vector2 lateral = calculateLateralVector(50);
      Vector2 downward = calculateNormalDownwardSpeedVector();
      this.transform.Translate(downward + lateral);
    }

    private void moveEnemyMode3()
    {
      this.updateDisplacementFactor();
      Vector2 lateral = calculateLateralVector(20);
      Vector2 downward = calculateHardDifficultySpeedVector();
      this.transform.Translate(downward + lateral);
    }

    private Vector2 calculateHardDifficultySpeedVector()
    {
      return calculateNormalDownwardSpeedVector() + Vector2.down * speed * Time.deltaTime * Mathf.Abs(Mathf.Sin(this.displacementFactor));
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

        case "Asteroid":
          asteroidHit(other);
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

      private void asteroidHit(Collider2D other)
    {
      Asteroid asteroid = other.GetComponent<Asteroid>();
      if (asteroid != null)
      {
        asteroid.Damage(this.damageAmount);
      }

      this.speed = this.speed * UnityEngine.Random.Range(0f, 0.25f);
      this.GetComponent<PolygonCollider2D>().enabled = false;
      this.GetComponent<Animator>().SetTrigger("OnEnemyDeath");

      Destroy(this.gameObject, 1.1f);
    }

    public void SetDifficultyLevel(int level)
    {
      this.difficultyLevel = level;
    }
  }
}