using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public static class StatusController
{
    static private float health = 100.0f;
    static private float water = 100.0f;
    static private float love = 100.0f;
    static private int PetType = 1;
    static private int HasPet = 1;
    static private string username = "abc2";
    static private string password = "abc";

    // Start is called before the first frame update

    // Update is called once per frame
    public static void setUsername(string n)
    {
        username = n;
    }
    public static void setPassword(string n)
    {
        password = n;
    }
    public static string getUsername()
    {
        return username;
    }
    public static string getPassword()
    {
        return password;
    }
    public static void setPetType(int n)
    {
        PetType = n;
    }
    public static int getPetType()
    {
        return PetType;
    }
    public static void setHasPet(int n)
    {
        HasPet = n;
    }
    public static int getHasPet()
    {
        return HasPet;
    }
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

    public static void minusLove(float n)
    {
        love -= n;
    }
}
