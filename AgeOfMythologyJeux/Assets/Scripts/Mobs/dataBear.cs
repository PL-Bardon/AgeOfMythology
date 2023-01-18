using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dataBear : dataObject
{
    public IABear ia;
    public GameObject deadBear;

    void Start()
    {
        this.objectName = "Ours sauvage";
        this.currentHealth = 100;
        this.maxHealth = 100;
        this.damage = 30;

        this.foodPrice = 0f;
        this.woodPrice = 0f;
        this.goldPrice = 0f;
        this.prayPrice = 0f;

        this.objDescription = "Ours de la nature. ";

        this.timeToBuild = 0F;
        this.isBuilded = true;

        this.buyPannel = null;

        ia = this.gameObject.GetComponent<IABear>();
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
        this.currentHealth -= Time.deltaTime*30;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            DieBear();
        }
    }
    public void DieBear()
    {
        buyPannel = null;
        StartCoroutine(dieAnimationBear());
    }
    IEnumerator dieAnimationBear()
    {
        for (int i = 0; i < 100; i++)
        {
            this.transform.position -= new Vector3 (0f, 0.002f, 0f);
            yield return new WaitForSeconds(0.01F);
        }
        GameObject obj = Instantiate(deadBear, (this.transform.position - new Vector3(0,0.5F,0)), deadBear.transform.rotation);
        obj.name = deadBear.name;
        Destroy(this.gameObject);
    }
    public override void moveTo(Vector3 pos)
    {
        ia.Move(pos);
    }
}

