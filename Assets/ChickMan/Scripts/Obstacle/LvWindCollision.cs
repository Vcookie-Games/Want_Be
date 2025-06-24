using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LvWindCollision : MonoBehaviour
{
    public windLevel level;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent.SendMessage("OnChildTriggerEnter2D", new LvWindInfo(level, collision), SendMessageOptions.DontRequireReceiver);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.parent.SendMessage("OnChildTriggerExit2D", new LvWindInfo(level, collision), SendMessageOptions.DontRequireReceiver);
        }
    }
}
