using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dataVillager : dataObject
{
    public IAVillager ia;

    void Start()
    {
        this.objectName = "Villager";
        this.currentHealth = 100;
        this.maxHealth = 100;
        this.damage = 2;

        this.foodPrice = 50f;
        this.woodPrice = 0f;
        this.goldPrice = 0f;
        this.prayPrice = 0f;

        this.objDescription = "Villageois";

        this.timeToBuild = 0F;
        this.isBuilded = true; // A MODIF

        this.buyPannel = GameObject.Find("Canvas/AllShop/BuyVillager");

        ia = this.gameObject.GetComponent<IAVillager>();

    }

    public Vector3 toV3Int(Vector3 position)
    {
        return new Vector3((int)position.x, (int)position.y, (int)position.z);
    }
    public bool isStillMoving()
    {
        return toV3Int(this.transform.position) == toV3Int(toGo);
    }
    void Update()
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    public override void moveTo(Vector3 pos)
    {
        ia.anim.Play("RunForward");
        ia.Move(pos);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            projectileScript s = other.gameObject.GetComponent<projectileScript>();
            this.currentHealth -= s.damage;
            Destroy(other.gameObject);
        }
    }
}
