using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class StatusController
{
    static private float health = 100.0f;
    static private float water = 100.0f;
    static private float love = 100.0f;

    // Start is called before the first frame update

    // Update is called once per frame


    public static void setHealth(float H)
    {
        health = H;
    }
    public static void setWater(float W)
    {
        water = W;
    }
    public static void setLove(float L)
    {
        love = L;
    }
    public static float getHealth()
    {
        return health;
    }
    public static float getWater()
    {
        return water;
    }
    public static float getLove()
    {
        return love;
    }

    public static void minusHealth(float n)
    {
        health -= n;
    }

    public static void minusWater(float n)
    {
        water -= n;
    }
}
