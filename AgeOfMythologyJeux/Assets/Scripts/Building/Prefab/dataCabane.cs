using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dataCabane : dataObject
{
    public GameObject[] allCabanes;
    public int level;
    
    public int priceForUpgrade = 100;
     
    void Start()
    {
        this.objectName = this.gameObject.name;
        this.toGo = this.transform.position;
        this.currentHealth = 1;
        this.maxHealth = 250;
        this.damage = 0;

        this.foodPrice = 0f;
        this.woodPrice = 50f;
        this.goldPrice = 50f;
        this.prayPrice = 0f;

        this.objDescription = "Cabane de ressources";

        this.timeToBuild = 3f;
        this.isBuilded = false;

        this.buyPannel = GameObject.Find("Canvas/AllShop/BuyCabane");
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
        this.maxHealth += 100;  
        this.currentHealth = this.maxHealth;
        this.isBuilded = true;
    }
    void Update()
    {
        this.objectName = "Cabane " + this.level;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Troups")
        {
            IAVillager villager = other.gameObject.GetComponent<IAVillager>();
            if (villager != null)
            {
                getBackToWork(villager);
            }
        }
    }
    
}