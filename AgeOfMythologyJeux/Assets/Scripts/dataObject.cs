using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class dataObject : MonoBehaviour
{
    public string objectName;
    public float currentHealth;
    public float maxHealth;
    public float damage;
    public float FinalDamage;
    public Sprite img;
    public Color color;

    public GameObject buyPannel;

    public bool isClicked;

    //Attributes price
    public float foodPrice;
    public float woodPrice;
    public float goldPrice;
    public float prayPrice;

    //Desc
    public string objDescription;

    //Gameplay
    public float timeToBuild;
    public bool isBuilded = false;

    public Vector3 toGo;

    public playerAttributes PA;

    
    void Start()
    {
        FinalDamage = damage * 1.5f;
        objDescription = "Pas de desc";
        isClicked = false;
    }

    public void Print(GameObject allUI, TextMeshProUGUI displayName, TextMeshProUGUI displayHealth, Slider healthBar, Image image)
    {
        if (this.isBuilded && buyPannel != null)
            this.buyPannel.SetActive(true);
        if (this.currentHealth > 0)
        {
            allUI.SetActive(true);
            displayName.text = "" + this.objectName;
            displayHealth.text = this.currentHealth + " / " + this.maxHealth;
            healthBar.maxValue = this.maxHealth;
            healthBar.value = this.currentHealth;
            image.sprite = this.img;
            isClicked = true;
        }
        else
        {
           allUI.SetActive(false);
           isClicked = false; 
        }
    }
    public void showStats(TextMeshProUGUI txt, TextMeshProUGUI foodDisplay,TextMeshProUGUI woodDisplay,TextMeshProUGUI goldDisplay,TextMeshProUGUI prayDisplay)
    {
        txt.text = this.objDescription;
        foodDisplay.text = "" + this.foodPrice;
        woodDisplay.text = "" + this.woodPrice;
        goldDisplay.text = "" + this.goldPrice;
        prayDisplay.text = "" + this.prayPrice;
    }
    public void createObject()
    {
        StartCoroutine(spawnAnimation());
    }
    IEnumerator spawnAnimation()
    {
        this.currentHealth = 0F;
        float healthToAdd = (float) Mathf.Ceil((this.maxHealth / 1000));
        float delai = 0.001F * this.timeToBuild;
        while (this.currentHealth < this.maxHealth)
        {
            this.currentHealth += healthToAdd;
            yield return new WaitForSeconds(delai);
        }
        this.isBuilded = true;
    }

    public void Die()
    {
        buyPannel = null;
        this.gameObject.tag = "Untagged";
        StartCoroutine(dieAnimation());
    }
    IEnumerator dieAnimation()
    {
        for (int i = 0; i < 100; i++)
        {
            this.transform.position -= new Vector3 (0f, 0.003f, 0f);
            yield return new WaitForSeconds(0.01F);
        }
        Destroy(this.gameObject);
    }

    public void getBackToWork(IAVillager villager)
    {
        IEnumerator cor = getBackToWorkCor(villager);
        StartCoroutine(cor);
    }
    IEnumerator getBackToWorkCor(IAVillager villager)
    {
        yield return new WaitForSeconds(1F);
        PA.foodQuantity += villager.currentFood;
        villager.currentFood = 0;

        PA.woodQuantity += villager.currentWood;
        villager.currentWood = 0;

        PA.goldQuantity += villager.currentGold;
        villager.currentGold = 0;
        
        yield return new WaitForSeconds(0.1F);
        villager.isBringing = false; 
    }
    public virtual void moveTo(Vector3 pos){}

}
