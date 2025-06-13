using System.Collections;
using System.Collections.Generic;
using HoangVuCode;
using UnityEngine;

public class BeeHiveObstacle : TrapObstacle
{

  [SerializeField] private GameObject BeeSwarmPrefab;
  [SerializeField] private float timeSpawnDelay ;
  private Rigidbody2D _rigidbody2D;
  BeeHiveObstacle()
  {
    canStun = false;
    timeSpawnDelay = 0.5f;
  }
  void Start()
  {
    _rigidbody2D = GetComponent<Rigidbody2D>();
    _rigidbody2D.bodyType = RigidbodyType2D.Static;
  }
  public override void Active()
  {
    if (!iSActive)
    {
      StartCoroutine(CountdownTimer(timeSpawnDelay, () =>
      {
      BeeSwarm beeSwarm = Instantiate(BeeSwarmPrefab, transform.position, Quaternion.identity).GetComponent<BeeSwarm>();
      beeSwarm.SetPlayerController(playerController);
      beeSwarm.Active();
      }));
      
    }
    iSActive = true;
    _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
    
  }

  public override void DeActive()
  {
    iSActive = false;
  }
    void OnCollisionEnter2D(Collision2D collision)
    {
      if (collision.gameObject.CompareTag("Player") && !iSActive)
      {
        playerController = collision.gameObject.GetComponent<PlayerController>();
        Active();
      }
    }

}
