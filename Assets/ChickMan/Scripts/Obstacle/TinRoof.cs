using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinRoof : TrapObstacle
{
    [SerializeField] private Color colorActive;
    [SerializeField] private Color colorDeActive;
    [SerializeField] private float timeReActive;
    private GameObject wall;
    private SpriteRenderer spriteRenderer;
    public TinRoof()
    {
        canStun = false;
        timeEffect = 3f;
        timeStun = 0f;  
        maxHitCount = 0;
        colorActive = Color.green;
        colorDeActive = Color.grey;
        timeReActive = 5f;
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        wall = transform.GetChild(0).gameObject; 
        wall.SetActive(true);
        spriteRenderer.color = colorActive; 
    }
    public override void Active()
    {
        isActive = true;
        playerController.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, playerController.transform.position.z);
        playerController.AddSpeed(0);
        StartCoroutine(CountdownTimer(timeEffect, () => DeActive()));
        
    }

    public override void DeActive()
    {
        playerController.ResetSpeed();
        spriteRenderer.color = colorDeActive; 
        wall.SetActive(false);
        StartCoroutine(CountdownTimer(timeReActive, () =>
        {
            isActive = false;
            wall.SetActive(true);
            spriteRenderer.color = colorActive;
        }));
    }
    
}
