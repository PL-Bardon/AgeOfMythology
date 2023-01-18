using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    public GameObject[] targets;
    public dataObject data;
    public NavMeshAgent NavMesh;
    public Animator anim;

    public GameObject toAttack;
    
    public void getComponent()
    {
        this.data = this.gameObject.GetComponent<dataObject>();
        this.NavMesh = this.gameObject.GetComponent<NavMeshAgent>();
    }
    public void Reset()
    {
        NavMesh.ResetPath();
    }
    public void Move(Vector3 pos)
    {
        anim.Play("RunForward");
        NavMesh.SetDestination(pos);
    }
    public float getPhyta(Vector3 pos,Vector3 villa)
    {
        return (pos.x - villa.x)*(pos.x -villa.x) + (pos.z - villa.z)*(pos.z -villa.z);
    }
    public bool compV3(Vector3 p1, Vector3 p2, float threshold)
    {
        float x = Mathf.Abs(p1.x - p2.x);
        float z = Mathf.Abs(p1.z - p2.z);   
        //Debug.Log($"{x:#.##} : {z:#.##}");
        if (x < threshold)
            if (z < threshold)
                return true;
        return false;
    }

    




















    
    /*
    // Update is called once per frame
    void Update()
    {
        if (data.isClicked)
        {
            //targets = GameObject.FindGameObjectsWithTag("Tree");
            //GameObject target = getNearest(targets);
            //Vector3 pos = target.transform.position;
            //Move(pos);
        }
    }
    private GameObject getNearest(GameObject[] objects)
    {
        float m = Mathf.Infinity;
        GameObject ObjM = objects[0];
        for (int i = 0; i < objects.Length;i++)
        {
            Vector3 pos = objects[i].transform.position;
            float dman = pos.x * pos.x + pos.z*pos.z ;
            if (dman < m)
            {
                m = dman;
                ObjM = objects[i];
            }
        }
        return ObjM;
    }
    */
}
