using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dataTower : dataObject
{
    public GameObject[] allTowers;
    public int level;
    
    public int priceForUpgrade = 250;

    void Start()
    {
        this.objectName = this.gameObject.name;
        this.toGo = this.transform.position;
        this.objectName = "Tower " + this.level;
        this.currentHealth = 1;
        this.maxHealth = 250;
        this.damage = 25;

        this.foodPrice = 1000f;
        this.woodPrice = 0f;
        this.goldPrice = 0f;
        this.prayPrice = 0f;

        this.objDescription = "Tour de défense.";

        this.timeToBuild = 3f;
        this.isBuilded = false;

        this.buyPannel = GameObject.Find("Canvas/AllShop/BuyTower");
        PA = GameObject.FindWithTag("GameManager").GetComponent<playerAttributes>();
        
        if (this.level == 4 && this.buyPannel != null)
        {
            Transform p = buyPannel.transform.Find("Upgrade");
            if (p != null)
            {
                GameObject pb = p.gameObject;
                if (pb != null)
                    Destroy(pb);
            }
        }
       
        for (int i = 1; i < level; i++)
        {
            this.upgradeValue();
        }

    }
    public void upgradeValue()
    {
        this.maxHealth *= 2;
        this.currentHealth = this.maxHealth;
        this.isBuilded = true;
        this.damage *= 2;
        buildingDefense def = this.gameObject.GetComponent<buildingDefense>();
        def.startDefense();
    }
    void Update()
    {
        this.objectName = "Tower " + this.level;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
}