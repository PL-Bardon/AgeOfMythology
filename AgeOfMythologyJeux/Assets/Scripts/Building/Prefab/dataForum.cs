using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dataForum : dataObject
{
    public GameObject villager;

    void Start()
    {
        this.toGo = this.transform.position;
        this.objectName = "Forum";
        this.currentHealth = 1;
        this.maxHealth = 100; //1000
        this.damage = 10;

        this.foodPrice = 50f;
        this.woodPrice = 1000f;
        this.goldPrice = 1000f;
        this.prayPrice = 0f;
        
        this.objDescription = "Centre du village";

        this.timeToBuild = 0f; //REMETTRE 10F;
        this.isBuilded = false;

        PA = GameObject.FindWithTag("GameManager").GetComponent<playerAttributes>();
        
        this.buyPannel = GameObject.Find("Canvas/AllShop/BuyForum");
    }
    
    void Update()
    {
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
    public void spawnVillager()
    {
        if(PA.foodQuantity >= 50)
        {
            PA.foodQuantity -= 50;
            StartCoroutine(sVillager());
        }
    }
    IEnumerator sVillager()
    {
        GameObject v = Instantiate(villager, this.transform.position, Quaternion.identity);
        v.gameObject.name = villager.name;
        IAVillager ia = v.GetComponent<IAVillager>();
        yield return new WaitForSeconds(0.01F);
        PA.villagers.Add(v);
        PA.totalVillagers += 1;
        ia.farmingSpeed = PA.farmingSpeed;
        if (toGo == this.transform.position)
            v.gameObject.GetComponent<IAVillager>().Move(this.transform.position + new Vector3(-1,0,-1));
        else
            v.gameObject.GetComponent<IAVillager>().Move(toGo);
    }
}
