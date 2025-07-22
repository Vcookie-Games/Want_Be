using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TrafficLightState
{
    Red,
    Green,
    Yellow
}
public class TrafficLight : TrapObstacle
{
    [SerializeField] private float timeActiveRedLight;
    [SerializeField] private float timeActiveGreenLight;
    [SerializeField] private float timeActiveYellowLight;
    [SerializeField] private TrafficLightState currentState;
    [Header("Settings")]
    [SerializeField] private Sprite redLightSprite;
    [SerializeField] private Sprite greenLightSprite;
    [SerializeField] private Sprite yellowLightSprite;

    [Header("Spawn Settings")]
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private GameObject ninjaLeadPrefab;
    [SerializeField] private bool spawnAtRight;
    [SerializeField][Range(0.1f, 10f)] private float minTimeRandomSpawn;
    [SerializeField][Range(0.1f, 10f)] private float maxTimeRandomSpawn;
    private float spawnInterval;

    private float spawnTimer;

    TrafficLight()
    {
        canStun = false;
        maxHitCount = 0;
        timeEffect = 0;
        timeStun = 0;
        maxHitCount = 0;
        timeActiveRedLight = 3f;
        timeActiveGreenLight = 3f;
        timeActiveYellowLight = 2f;
        currentState = TrafficLightState.Red;
        spawnInterval = 2f;
        spawnTimer = 0f;
    }
    void OnValidate()
    {
        if (minTimeRandomSpawn > maxTimeRandomSpawn)
        {
            Debug.LogWarning("Min time spawn cannot be greater than max time spawn. Resetting to default values.");
            minTimeRandomSpawn = 1f;
            maxTimeRandomSpawn = 10f;
        }
    }
    void Start()
    {

        spawnInterval = RandomTimeSpawn();
        GetComponent<SpriteRenderer>().sprite = redLightSprite;
        Active();

    }
    void Update()
    {
        if (isActive && ninjaLeadPrefab != null)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnNinjaLead();
                spawnTimer = 0f;
                spawnInterval = RandomTimeSpawn();
            }
        }
    }
    public override void Active()
    {
        isActive = true;
        spawnTimer = 0f;
        GetComponent<SpriteRenderer>().sprite = greenLightSprite;
        currentState = TrafficLightState.Green;
        StartCoroutine(CountdownTimer(timeActiveGreenLight, () =>
        {
            GetComponent<SpriteRenderer>().sprite = yellowLightSprite;
            currentState = TrafficLightState.Yellow;
            StartCoroutine(CountdownTimer(timeActiveYellowLight, () =>
            {
                GetComponent<SpriteRenderer>().sprite = redLightSprite;
                currentState = TrafficLightState.Red;
                DeActive();
            }));
        }));
    }
    public override void DeActive()
    {
        isActive = false;
        StartCoroutine(CountdownTimer(timeActiveRedLight, () =>
        {
            Active();
        }));
    }

    private void SpawnNinjaLead()
    {

        Transform spawnPoint = spawnAtRight ? right : left;
        GameObject ninjaLead = Instantiate(ninjaLeadPrefab, spawnPoint.position, Quaternion.identity);
        Vector3 scale = ninjaLead.transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (spawnAtRight ? 1 : -1);
        ninjaLead.transform.localScale = scale;

        NinjaLeadMovement movement = ninjaLead.GetComponent<NinjaLeadMovement>();
        if (movement != null)
        {
            movement.moveRight = !spawnAtRight;

            //Thay đổi tốc độ của NinjaLeadMovement dựa trên trạng thái đèn giao thông (Đang lỗi)
            /*   switch (currentState)
                  {
                      case TrafficLightState.Yellow:
                          movement.AddSpeed(2);
                          Debug.LogWarning("ok");
                          break;
                      case TrafficLightState.Green:
                          movement.ResetSpeed();
                          Debug.LogWarning("ok");
                          break;

                  } */
        }

    }

    private float RandomTimeSpawn()
    {
        return Random.Range(minTimeRandomSpawn, maxTimeRandomSpawn);
    }

}
