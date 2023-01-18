using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAVillager : IA
{
    public RaycastHit[] rayHit;
    public LayerMask layerMask;
    public string tags;

    public bool isBringing;
    public bool isWorking;

    public int currentFood;
    public int currentWood;
    public int currentGold;

    public int maxFood;
    public int maxWood;
    public int maxGold;

    public int farmingSpeed;
    public float farmingDelai;
    public int closeToLoot;
    public float distanceFromLoot;
    public GameObject CurrentLootingTarget;

    public Vector3 oldPos;
    private GameObject Axe;
    private GameObject Pickaxe;

    void Start()
    {
        getComponent();
        
        tags = "Wood,Food,Gold";
        isBringing = false;
        //Villager
        currentFood = 0;
        currentWood = 0;
        currentGold = 0;

        maxFood = 50;
        maxWood = 50;
        maxGold = 50;

        farmingSpeed = 0;//10
        farmingDelai = 1F;
        distanceFromLoot = 15f;

        isWorking = false;

        anim = this.gameObject.GetComponent<Animator>();
        oldPos = this.transform.position;

        Pickaxe = this.transform.Find("Armature/Root_M/Spine1_M/Spine2_M/Chest_M/Scapula_R/Shoulder_R/Elbow_R/Wrist_R/Pickaxe").gameObject;
        Axe = this.transform.Find("Armature/Root_M/Spine1_M/Spine2_M/Chest_M/Scapula_R/Shoulder_R/Elbow_R/Wrist_R/Axe").gameObject;
        Pickaxe.SetActive(false);
        Axe.SetActive(false);

    }
    void Update()
    {
        //Anim
        if (compV3(this.transform.position,oldPos,0.01f) && !isWorking && toAttack == null)
            anim.Play("Idle");
        //Loot
        if (isBringing)
            return;
        if (compV3(this.transform.position,this.data.toGo,0.02f))
        {
            getRessources();
        }
        else if (this.CurrentLootingTarget != null)
        {
            if (closeToLoot <= 0)
            {
                anim.Play("RunForward");
                Move(this.CurrentLootingTarget.transform.position);
            }
            else if (!isWorking)
                StartCoroutine(farmRessource());
        }
        
        oldPos = this.transform.position;
    }
    public void bringRessources()
    {
        isBringing = true;
        GameObject mObj = null;
        float m = Mathf.Infinity;
        rayHit = Physics.SphereCastAll(this.transform.position, 1000F, transform.forward);

        for (int i = 0; i < rayHit.Length; i++)
        {
            if (rayHit[i].transform.gameObject.tag == "Ressources")
            {
                float t = getPhyta(rayHit[i].transform.position,this.transform.position);
                (mObj,m) = (t <m)?(rayHit[i].transform.gameObject,t):(mObj,m);
            }
        }
        if (mObj != null)
        {
            anim.Play("RunForward");
            Move(mObj.transform.position);
            
        }
    }

    bool isFull()
    {
        if (this.CurrentLootingTarget == null)
            return false;
        switch (this.CurrentLootingTarget.tag)
        {
            case "Food":
                return (currentFood >= maxFood);
            case "Wood":
                return (currentWood >= maxWood);
            case "Gold":
                return (currentGold >= maxGold);
            default:
                return false;
        }
    }
    void addLoot(int toAdd)
    {
        switch (CurrentLootingTarget.tag)
        {
            case "Food":
                currentFood += toAdd;
                break;
            case "Wood":
                currentWood += toAdd;
                break;
            case "Gold":
                currentGold += toAdd;
                break;
        }
    }
    int maxToMake()
    {
        if (this.CurrentLootingTarget == null)
            return 0;
        switch (this.CurrentLootingTarget.tag)
        {
            case "Food":
                return (maxFood - currentFood);
            case "Wood":
                return (maxWood - currentWood);
            case "Gold":
                return (maxGold - currentGold);
            default:
                return 0;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (this.tags.Contains(other.tag))
            closeToLoot += 1;
    }
    void OnTriggerExit(Collider other)
    {
        if (this.tags.Contains(other.tag))
            closeToLoot -= 1;
    }

    public void getRessources()
    {
        if (this.CurrentLootingTarget == null)
        {
            rayHit = Physics.SphereCastAll(this.transform.position, this.distanceFromLoot, transform.forward);
            
            GameObject mObj = null;
            float m = Mathf.Infinity;
            for (int i = 0; i < rayHit.Length;i++)
            {
                if (this.tags.Contains(rayHit[i].transform.gameObject.tag) && rayHit[i].transform.gameObject.tag != "")
                {
                    float t = getPhyta(rayHit[i].transform.position,this.transform.position);
                    (mObj,m) = (t < m)?(rayHit[i].transform.gameObject,t):(mObj,m);
                }
            }
            if (mObj != null)
            {
                CurrentLootingTarget = mObj;
                Move(CurrentLootingTarget.transform.position);
                anim.Play("RunForward");
            }
        }
    
    }
    IEnumerator farmRessource()
    {
        isWorking = true;
        int maxToTake = 0;
        if (CurrentLootingTarget != null && closeToLoot > 0)
        {
            ressourceLoot loot;
            loot = CurrentLootingTarget.GetComponent<ressourceLoot>();
            anim.Play("MiningLoop");
            if(CurrentLootingTarget.tag == "Gold")
                Pickaxe.SetActive(true);
            if(CurrentLootingTarget.tag == "Wood")
                Axe.SetActive(true);
            while (loot != null && !isFull())
            {
                maxToTake = maxToMake();
                if (loot.quantityLoot >= farmingSpeed)
                {
                    if (maxToTake >= farmingSpeed)
                    {
                        loot.quantityLoot -= farmingSpeed;
                        addLoot(farmingSpeed);
                    }
                    else
                    {
                        loot.quantityLoot -= maxToTake;
                        addLoot(maxToTake);
                    }
                }
                else
                {
                    if (loot.quantityLoot <= maxToTake)
                    {
                        loot.quantityLoot = 0;
                        addLoot((int)loot.quantityLoot);
                    }
                    else
                    {
                        loot.quantityLoot -= maxToTake;
                        addLoot(maxToTake);
                    }
                }
                yield return new WaitForSeconds(farmingDelai);
            }
            if(isFull())
                bringRessources();
            else
                getRessources();
        }
        Pickaxe.SetActive(false);
        Axe.SetActive(false);
        isWorking = false;
    }
    public void fightEnemi()
    {
        if (getPhyta(this.transform.position, toAttack.transform.position) > 3)
        {
            anim.Play("RunForward");
            Move(this.toAttack.transform.position);
        }
        StartCoroutine(fight());
    }
    IEnumerator fight()
    {
        float delai = 1.7F;
        dataObject dataO = toAttack.gameObject.GetComponent<dataObject>();
        while(dataO != null && toAttack != null)
        {
            float pita = getPhyta(this.transform.position, toAttack.transform.position);
            Debug.Log(pita);
            if (pita <= 5)
            {
                Reset();
                anim.Play("PunchRight");
                yield return new WaitForSeconds(0.7F);
                delai -= 0.7F;
                dataO.currentHealth -= data.damage;
            }
            else
            {
                anim.Play("RunForward");
                Move(toAttack.transform.position);
            }
            yield return new WaitForSeconds(delai);
        }
    }
}
