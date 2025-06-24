using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum windLevel
{
    Lv1,
    Lv2,
    Lv3
}
public class LvWindInfo
{
    public windLevel level;
    public Collider2D collision;
    public LvWindInfo(windLevel level, Collider2D collision)
    {
        this.level = level;
        this.collision = collision;
    }
}
