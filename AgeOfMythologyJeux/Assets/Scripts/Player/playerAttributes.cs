using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class playerAttributes : MonoBehaviour
{
    public string playerName;
    public float foodQuantity;
    public float woodQuantity;
    public float goldQuantity;
    public float prayQuantity;

    public TextMeshProUGUI foodDisplay;
    public TextMeshProUGUI woodDisplay;
    public TextMeshProUGUI goldDisplay;
    public TextMeshProUGUI prayDisplay;

    public int totalTroups;
    public int maxTroups;

    public List<GameObject> villagers = new List<GameObject>();
    public int farmingSpeed;
    public int maxLoot;
    public int totalVillagers;

    public bool isEnemy;

    void Start()
    {
        playerName = "Bebert";
        foodQuantity = 1500F;
        woodQuantity = 1500F;
        goldQuantity = 1500F;
        prayQuantity = 100f;

        totalTroups = 0;
        maxTroups = 10;
        farmingSpeed = 10;
        maxLoot = 50;
        totalVillagers = 0;
    }
    void Update()
    {
        foodDisplay.text = "" + foodQuantity;
        woodDisplay.text = "" + woodQuantity;
        goldDisplay.text = "" + goldQuantity;
        prayDisplay.text = "" + prayQuantity;
    }


    ////////////////////////////////////////////////
    //                  UPGRADE                   //
    ////////////////////////////////////////////////
    public void UpgradeVillager()
    {
        this.farmingSpeed += 5;
        this.maxLoot += 10;
        IAVillager ia = null;
        for (int i = 0; i < totalVillagers; i++)
        {
            ia = villagers[i].GetComponent<IAVillager>();
            ia.farmingSpeed = this.farmingSpeed;
            ia.maxFood = maxLoot;
            ia.maxWood = maxLoot;
            ia.maxGold = maxLoot;
        }
    }
}
