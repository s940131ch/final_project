using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusController : MonoBehaviour
{
    private float health;
    private float water;
    private float love;
    public Image healthBar;
    public Image waterBar;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = healthBar.GetComponent<Image>();
        waterBar = waterBar.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = health / 100.0f;
        waterBar.fillAmount = water / 100.0f;
    }

    public void setHealth(float H)
    {
        this.health = H;
    }
    public void setWater(float W)
    {
        this.water = W;
    }
    public void setLove(float L)
    {
        this.love = L;
    }
    public float getHealth()
    {
        return this.health;
    }
    public float getWater()
    {
        return this.water;
    }
    public float getLove()
    {
        return this.love;
    }

    public void minusHealth(float n)
    {
        this.health -= n;
    }

    public void minusWater(float n)
    {
        this.water -= n;
    }
}
