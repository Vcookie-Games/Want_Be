using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonstersMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform endPosition;
    private Transform currentPoint;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentPoint = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentPoint.position, speed * Time.deltaTime);


        if (currentPoint == startPosition && transform.localScale.x < 0 || currentPoint == endPosition && transform.localScale.x > 0)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.1f)
        {
            currentPoint = currentPoint == startPosition ? endPosition : startPosition;
        }
    }


}
