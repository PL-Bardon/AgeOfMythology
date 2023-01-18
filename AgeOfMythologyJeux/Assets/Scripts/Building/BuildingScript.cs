using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public bool canDrop;
    public Material cantPlace;
    public NavMeshObstacle obstacle;
    void Start()
    {
        canDrop = true;
        obstacle = this.gameObject.GetComponent<NavMeshObstacle>();
        obstacle.enabled = false;
    }
    public void dropBuilding()
    {
        obstacle.enabled = true;
        cantPlace = null;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Floor")
        {
            canDrop = false;
            if (cantPlace != null)
            { 
                List<Material> materialss = createAddList(cantPlace,this.gameObject.GetComponent<MeshRenderer>().materials);
                this.gameObject.GetComponent<MeshRenderer>().materials = materialss.ToArray();
            }
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Floor")
        {
            canDrop = true;
            if (cantPlace != null)
            {                 
                List<Material> materialss = createRemList(cantPlace,this.gameObject.GetComponent<MeshRenderer>().materials);
                this.gameObject.GetComponent<MeshRenderer>().materials = materialss.ToArray();
            }
        }
    }

    public List<Material> createAddList(Material val,Material[] M)
    {
        List<Material> L = new List<Material>();
        for (int i = 0;i<M.Length; i++)
        {
            L.Add(M[i]);
        }
        L.Add(val);
        return L;
    }
    public List<Material> createRemList(Material val,Material[] M)
    {
        List<Material> L = new List<Material>();
        for (int i = 0;i<M.Length-1; i++)
        {
            L.Add(M[i]);
        }
        return L;
    }

}
