using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dataCastle : dataObject
{
    public GameObject fighterAxe;
    void Start()
    {
        this.toGo = this.transform.position;
        this.objectName = "Castle";
        this.currentHealth = 1;
        this.maxHealth = 250;
        this.damage = 25;

        this.foodPrice = 1000f;
        this.woodPrice = 0f;
        this.goldPrice = 0f;
        this.prayPrice = 0f;

        this.objDescription = "Chateau";

        this.timeToBuild = 3f;
        this.isBuilded = false;

        this.buyPannel = GameObject.Find("Canvas/AllShop/BuyCastle");

        PA = GameObject.FindWithTag("GameManager").GetComponent<playerAttributes>();
    }
    
    void Update()
    {
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    public void spawnfighterAxe()
    {
        if (PA.totalTroups < PA.maxTroups)
            StartCoroutine(sFighter());
    }
    IEnumerator sFighter()
    {
        GameObject f = Instantiate(fighterAxe, this.transform.position, Quaternion.identity);
        f.gameObject.name = fighterAxe.name;
        IAArmy ia = f.GetComponent<IAArmy>();
        yield return new WaitForSeconds(0.01F);
        PA.totalTroups += 1;
        if (toGo == this.transform.position)
            f.gameObject.GetComponent<IAArmy>().Move(this.transform.position + new Vector3(-1,0,-1));
        else
            f.gameObject.GetComponent<IAArmy>().Move(toGo);
    }
}