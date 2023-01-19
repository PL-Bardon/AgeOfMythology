using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class addBuilding : MonoBehaviour
{
    public GameObject[] Objects;
    public Camera cam;
    private GameObject obj;
    public bool isCreated;
    public Vector3 m_pos;

    public LayerMask layerMask;

    public playerAttributes PA;

    //Display UI
    public GameObject pannelStats;
    public TextMeshProUGUI foodDisplay;
    public TextMeshProUGUI woodDisplay;
    public TextMeshProUGUI goldDisplay;
    public TextMeshProUGUI prayDisplay;

    //All Building
    public List<GameObject> cabanes = new List<GameObject>();
    public List<GameObject> towers = new List<GameObject>();

    void Update()
    {
        if (isCreated)
        {
            getMousePosition();
            this.obj.transform.position = m_pos;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (canBuy())
                    drop();
                else
                {
                    Destroy(obj);
                    isCreated = false;
                }
            }
        }   
    }
    #region 
    public void drop()
    {
        if (obj == null) 
            return;
        else
        {
            BuildingScript bs = obj.GetComponent<BuildingScript>();
            if (bs != null && bs.canDrop)
            {
                if (!this.gameObject.GetComponent<mouseController>().onUI)
                {
                    buy();
                    addList();
                    bs.dropBuilding();
                    bs.cantPlace = null;
                    obj = null;
                    isCreated = false;
                }
                else
                {
                    Destroy(obj);
                    isCreated = false;
                }
            }
        }
    }

    public void Create(int index)
    {
        getMousePosition();
        if (obj == null)
        {
            obj = Instantiate(Objects[index], m_pos, Objects[index].transform.rotation);
            obj.name = Objects[index].name;
            isCreated = true;
            BuildingScript bs = obj.GetComponent<BuildingScript>();
            if (bs != null)
                bs.canDrop = true;
              
        }
        else
        {
            Destroy(obj);
            obj = Instantiate(Objects[index], m_pos, Objects[index].transform.rotation);
            obj.name = Objects[index].name;
            BuildingScript bs = obj.GetComponent<BuildingScript>();
            if (bs != null)
                bs.canDrop = true;
        }
    }
    
    void getMousePosition()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Physics.Raycast(ray,out hit,100F,layerMask);
    
        m_pos = new Vector3(hit.point.x, 0, hit.point.z);
    }
    bool canBuy()
    {
        dataObject data = obj.GetComponent<dataObject>();
        if (data != null)
        {
            return (PA.foodQuantity >= data.foodPrice) && (PA.woodQuantity >= data.woodPrice) 
                        && (PA.goldQuantity >= data.goldPrice) 
                        && (PA.prayQuantity >= data.prayPrice);
        }
        return false;
    }
    void buy()
    {
        dataObject data = obj.GetComponent<dataObject>();
        PA.foodQuantity -= data.foodPrice;
        PA.woodQuantity -= data.woodPrice;
        PA.goldQuantity -= data.goldPrice;
        PA.prayQuantity -= data.prayPrice;
        data.createObject();
    }
    #endregion 

    public void showStats(int index)
    {
        IEnumerator cor = smallDelai(index);
        StartCoroutine(cor);
    }
    IEnumerator smallDelai(int index)
    {
        yield return new WaitForSeconds(0.15F);

        Vector3 pos = new Vector3(0f, -50f, 0f);
        GameObject obj = Instantiate(Objects[index], pos, Objects[index].transform.rotation);
        obj.name = Objects[index].name;

        dataObject data = obj.GetComponent<dataObject>();
        TextMeshProUGUI txt = pannelStats.transform.Find("Texte").GetComponent<TextMeshProUGUI>();
        
        yield return new WaitForSeconds(0.001F);
        data.showStats(txt, foodDisplay, woodDisplay, goldDisplay, prayDisplay);
        pannelStats.SetActive(true);
        Destroy(obj);
    }
    public void unshowStats()
    {
        pannelStats.SetActive(false);
        StopAllCoroutines();
    }

    public void addList()
    {
        string s = obj.name;
        if (s.Contains("Tower"))
            towers.Add(obj);
        else if (s.Contains("Cabane"))
            cabanes.Add(obj);
    }
}
