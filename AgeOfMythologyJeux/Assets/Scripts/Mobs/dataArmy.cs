using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataArmy : dataObject
{
    public IAArmy ia;

    void Start()
    {
        this.objectName = "Soldat";
        this.currentHealth = 150;
        this.maxHealth = 150;
        this.damage = 15;

        this.foodPrice = 100f;
        this.woodPrice = 200f;
        this.goldPrice = 200f;
        this.prayPrice = 0f;

        this.objDescription = "Troupe de combat ";

        this.timeToBuild = 0F;
        this.isBuilded = true;

        this.buyPannel = null;

        ia = this.gameObject.GetComponent<IAArmy>();
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
            Debug.Log("MORT");
            currentHealth = 0;
            Die();
        }
    }
    public override void moveTo(Vector3 pos)
    {
        ia.Move(pos);
    }
}

