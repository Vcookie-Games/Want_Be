using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class RadiusBlackHole : MonoBehaviour
{
  private float pullForce;
  private PlayerController player;
  private Vector3 currentScale;

  public void Init(float pullForce)
  {
    this.pullForce = pullForce;
    currentScale = transform.localScale;
  }
  public void UpdateScale(Transform scaleBlackHole)
  {
    transform.localScale = scaleBlackHole.localScale + currentScale;
  }
  private  void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
    {
      player = collision.GetComponent<PlayerController>();
      player.transform.position = Vector2.MoveTowards(player.transform.position, transform.position, pullForce * Time.fixedDeltaTime);
    }
    } 
}
