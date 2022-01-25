using UnityEngine;
namespace vio.spaceshooter.game.asteroid
{
  public class Asteroid : MonoBehaviour
  {
    private Vector2 momentumVector;
    private const float MAX_Y_POS = 12f;
    private const float MIN_Y_POS = -2.5f;
    private const float MAX_X_POS = 15.00f;
    private const float MIN_X_POS = -14.75f;
    private float rotation = 25f;
    // Start is called before the first frame update
    void Start()
    {
      this.randomizeMomentum(false);
    }

    private void randomizeMomentum(bool isRight)
    {
      if (isRight) {
        this.momentumVector = new Vector2(UnityEngine.Random.Range(1f, 3f), UnityEngine.Random.Range(-2f, -5f));
      } else
      {
        this.momentumVector = new Vector2(UnityEngine.Random.Range(-1f, -3f), UnityEngine.Random.Range(-2f, -5f));
      }
    }

    // Update is called once per frame
    void Update()
    {
      this.transform.Rotate(0, 0, rotation * Time.deltaTime);
      this.transform.Translate(this.momentumVector * Time.deltaTime, Space.World);
      /*
      Vector2 downward = Vector2 * speed * Time.deltaTime;
      this.transform.Translate(downward);*/
      if (this.isAsteroidPastBottomOfPlayArea()) moveAsteroidToRandomXLocationAtTop();
    }

    private bool isAsteroidPastBottomOfPlayArea()
    {
      return (this.transform.position.y < MIN_Y_POS);
    }

    private void moveAsteroidToRandomXLocationAtTop()
    {
      this.transform.position = new Vector3(UnityEngine.Random.Range(MIN_X_POS, MAX_X_POS), MAX_Y_POS);
      this.transform.Rotate(0, 0, UnityEngine.Random.Range(0f, 360f));
      if (this.transform.position.x < -9.75f) {
        this.randomizeMomentum(true);
      } else if (this.transform.position.x > 10f)
      {
        this.randomizeMomentum(false);
      } else {
        this.randomizeMomentum(UnityEngine.Random.Range(0, 1) == 1);
      }

    }
  }
}
