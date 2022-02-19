using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMotion : MonoBehaviour
{
  private const float DEFAULT_SPEED = 10f;
  private const float MIN_Y_POS = -10.9f;
  private const float MAX_Y_POS = 30.1f;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    if (this.transform.position.y < MIN_Y_POS) {
      this.moveTop();
    } else {
      this.move();
    }
  }

  private void moveTop()
  {
    this.transform.Translate(new Vector3(0,MAX_Y_POS,0));
  }

  private void move()
  {
    Vector2 downward = Vector2.down * DEFAULT_SPEED * Time.deltaTime;
    this.transform.Translate(downward);
  }
}
