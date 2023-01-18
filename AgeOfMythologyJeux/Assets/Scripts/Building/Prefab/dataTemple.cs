using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dataTemple : dataObject
{
    void Start()
    {
        this.toGo = this.transform.position;
        this.objectName = "Temple";
        this.currentHealth = 1;
        this.maxHealth = 250;
        this.damage = 25;

        this.foodPrice = 0f;
        this.woodPrice = 400f;
        this.goldPrice = 0f;
        this.prayPrice = 0f;

        this.objDescription = "Temple de prière";

        this.timeToBuild = 3f;
        this.isBuilded = false;

        this.buyPannel = GameObject.Find("Canvas/AllShop/BuyTemple");
    }
    
    void Update()
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
}