using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine; // MonoBehaviour, Vector3, Time, Input

public class Player : MonoBehaviour
{
  private float _maxSpeed = 15f;

  // Start is called before the first frame update
  void Start()
  {
    this.transform.position = new Vector3(0, 0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    this.transform.Translate(
        getDirectionInputVector() * getPosibleDistanceMovedSinceLastFrame()
    );
  }
  private float getPosibleDistanceMovedSinceLastFrame()
  {
    return this.getMaxSpeed() * Time.deltaTime;
  }

  private Vector3 getDirectionInputVector()
  {
    return Vector3.ClampMagnitude(
      new Vector3(
        this.getHorizontalInput(), 
        this.getVerticalInput()
        ),
      1f
    );

  }

  private float getMaxSpeed()
  {
    return this._maxSpeed;
  }

  private float getVerticalInput()
  {
    return Input.GetAxis("Vertical");
  }

  private float getHorizontalInput()
  {
    return Input.GetAxis("Horizontal");
  }
}
