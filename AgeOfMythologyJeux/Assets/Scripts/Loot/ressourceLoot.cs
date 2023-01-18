using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ressourceLoot : dataObject
{
    public float quantityLoot = 100;
    public float maxLoot = 100;

    void Start()
    {
        objectName = "---";
        currentHealth = quantityLoot;
        maxHealth = maxLoot;
        this.buyPannel = null;
    }
    void Update()
    {
        this.currentHealth = quantityLoot;
        if (quantityLoot <= 0)
            Die();
    }
}