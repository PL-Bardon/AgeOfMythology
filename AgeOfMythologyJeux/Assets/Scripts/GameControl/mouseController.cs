using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class mouseController : MonoBehaviour
{
    public Camera cam;
    [SerializeField] GameObject currentObj;
    private GameObject currentFlag;
    public GameObject flag;

    //Display in UI
    public playerAttributes PA;
    public GameObject buyActive;
    public bool onUI;

    public GameObject allUI;
    public TextMeshProUGUI objectName;
    public TextMeshProUGUI currentHealth;
    public Slider healthBar;
    public Image img;

    private dataObject data;
    private addBuilding addBuild;

    public Texture2D cursorClassic;
    public Texture2D cursorSword;

    void Start()
    {
        currentObj = null;
        currentFlag = null;        
        cam = Camera.main;
        allUI.SetActive(false);
        buyActive = null;//NEWGAME
        onUI = false;

        PA = GameObject.FindWithTag("GameManager").GetComponent<playerAttributes>();
        addBuild = this.gameObject.GetComponent<addBuilding>();
    }
    void Update()
    {
        
        //if (currentObj == null)
            //Cursor.SetCursor(cursorClassic, Vector2.zero, CursorMode.ForceSoftware);
        if (currentObj != null)
            cursorHit();
        if (Input.GetKeyDown(KeyCode.Mouse0) && !onUI)
        {
            getHitObject();
        }
        if (currentObj != null && data != null)
        {
            if (buyActive != null)
                buyActive.SetActive(false);
            data.Print(allUI, objectName, currentHealth, healthBar, img);
            if(data.isBuilded)
                buyActive = data.buyPannel;
            if (Input.GetKeyDown(KeyCode.Mouse1) && data.isBuilded && !onUI && currentObj.gameObject.tag != "Gaia")
            {
                if(PA.isEnemy && data.tag == "Troups")
                {
                    getHitEnemi();
                }
                else
                {
                    placeFlag();
                    if (data.GetType() == typeof(dataVillager) || data.GetType() == typeof(dataArmy) )
                    {
                        data.moveTo(data.toGo);
                    }
                }
            }
        }
        else
        {
            if (buyActive != null)
                buyActive.SetActive(false);
            allUI.SetActive(false);
        }
    }
    void cursorHit()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100F))
        {
            if(data != null && hit.transform.gameObject != data.gameObject)
            {
                if (data != null && hit.transform.gameObject.tag != "Gaia" && hit.transform.gameObject.tag != "Floor" && currentObj.tag == "Troups")
                {
                    //Cursor.SetCursor(cursorSword, Vector2.zero, CursorMode.ForceSoftware);
                    PA.isEnemy = true;
                }
                else
                {
                    //Cursor.SetCursor(cursorClassic, Vector2.zero, CursorMode.ForceSoftware);
                    PA.isEnemy = false;
                }
            }
        }    
        else
        {
            //Cursor.SetCursor(cursorClassic, Vector2.zero, CursorMode.ForceSoftware);
            PA.isEnemy = false;
        }
    }
    public void enterUI(){onUI = false;}
    public void quitUI(){onUI = true;}
    void getHitObject()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray,out hit,100F))
        {
            currentObj = hit.transform.gameObject;

            //Debug.Log(currentObj.name);

            if (data != null)
                data.isClicked = false;

            data = currentObj.GetComponent<dataObject>();
            if (data != null)
            {
                data.Print(allUI, objectName, currentHealth, healthBar, img);
            }
            else
            {
                currentObj = null;
                allUI.SetActive(false);
            }
        }
    }
    public void getHitEnemi()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        IAVillager ia = currentObj.GetComponent<IAVillager>();
        if (ia != null)
        {
            if(Physics.Raycast(ray,out hit,100F))
            {
                ia.toAttack = hit.transform.gameObject;
                ia.fightEnemi();
            }
        }
        else 
        {
            IAArmy iaa = currentObj.GetComponent<IAArmy>();
            if (iaa != null)
            {
                if(Physics.Raycast(ray,out hit,100F))
                {
                    iaa.toAttack = hit.transform.gameObject;
                }
            }
        }
    }
    public void placeFlag()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray,out hit,100F);

        Vector3 m_pos = new Vector3(hit.point.x, 0.1F, hit.point.z);

        if (currentFlag != null)
            Destroy(currentFlag);

        currentFlag = Instantiate(flag, m_pos, flag.transform.rotation);

        m_pos.y = 0F;
        data.toGo = m_pos;

        IAVillager iaVill = currentObj.GetComponent<IAVillager>();
        if (iaVill != null) 
        {
            iaVill.toAttack = null;
            iaVill.isBringing = false;
        }
    }


    ////////////////////////////////////////////////
    //                SPAWN TROUPS                //
    ////////////////////////////////////////////////  
    public void SpawnVillager()
    {
        dataForum dataF = (dataForum) data;
        dataF.spawnVillager();
    }
    public void SpawnfighterAxe()
    {
        dataCastle dataC = (dataCastle) data;
        dataC.spawnfighterAxe();
    }

    ////////////////////////////////////////////////
    //                  UPGRADE                   //
    ////////////////////////////////////////////////
    public void Upgrade(string name)
    {
        switch (name)
        {
            case "Cabane":
                StartCoroutine(UpgradeCabane());
                break;
            case "Tower":
                StartCoroutine(UpgradeTower());
                break;
        }
    }
    IEnumerator UpgradeCabane()
    {
        dataCabane d = currentObj.GetComponent<dataCabane>();
        float price = d.priceForUpgrade * d.level;
        if (d.level < 4 && PA.foodQuantity > price && PA.woodQuantity > price && PA.goldQuantity > price)
        {
            PA.UpgradeVillager();
            PA.foodQuantity -= price;
            PA.woodQuantity -= price;
            PA.goldQuantity -= price;

            GameObject newObj = null;
            addBuild.Objects[2] = d.allCabanes[d.level];
            
            for (int i = 0; i < addBuild.cabanes.Count; i++)
            {
                dataCabane dc = addBuild.cabanes[i].GetComponent<dataCabane>();
                newObj = Instantiate(d.allCabanes[d.level], dc.transform.position, d.allCabanes[d.level].transform.rotation);
                newObj.name = d.allCabanes[d.level].name;
                newObj.GetComponent<dataCabane>().level = d.level + 1;

                BuildingScript bs = newObj.GetComponent<BuildingScript>();
                if(bs != null)
                    bs.dropBuilding();
                

                GameObject toDestroy = addBuild.cabanes[i];
                addBuild.cabanes[i] = newObj;
                Destroy(toDestroy);
            }
            yield return new WaitForSeconds(0.01F);
            Destroy(d);
        }
    }
    IEnumerator UpgradeTower()
    {
        dataTower d = currentObj.GetComponent<dataTower>();
        float price = d.priceForUpgrade * d.level;
        if (d.level < 4 && PA.foodQuantity > price && PA.woodQuantity > price && PA.goldQuantity > price)
        {
            PA.foodQuantity -= price;
            PA.woodQuantity -= price;
            PA.goldQuantity -= price;

            GameObject newObj = null;
            addBuild.Objects[3] = d.allTowers[d.level];
            
            for (int i = 0; i < addBuild.towers.Count; i++)
            {
                dataTower dc = addBuild.towers[i].GetComponent<dataTower>();
                newObj = Instantiate(d.allTowers[d.level], dc.transform.position, d.allTowers[d.level].transform.rotation);
                newObj.name = d.allTowers[d.level].name;
                newObj.GetComponent<dataTower>().level = d.level + 1;

                BuildingScript bs = newObj.GetComponent<BuildingScript>();
                if(bs != null)
                    bs.dropBuilding();
                

                GameObject toDestroy = addBuild.towers[i];
                addBuild.towers[i] = newObj;
                Destroy(toDestroy);
            }
            yield return new WaitForSeconds(0.01F);
            Destroy(d);
        }
    }
}
