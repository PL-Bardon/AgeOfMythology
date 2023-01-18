using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAArmy : IA
{
    private Vector3 oldPos;
    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        this.data = this.gameObject.GetComponent<dataArmy>();
        StartCoroutine(fight());
        oldPos = this.transform.position;

        int rdm = Random.Range(0,2);
        if (rdm == 1)
            data.color = Color.blue;
        else
            data.color = Color.red;
    }
    void Update()
    {
        if (compV3(this.transform.position,oldPos,0.01f) && toAttack == null)
        {
            anim.Play("Idle");
            searchEnemy();
        }
        oldPos = this.transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            projectileScript s = other.gameObject.GetComponent<projectileScript>();
            data.currentHealth -= s.damage;
            Destroy(other.gameObject);
        }
    }
    public void searchEnemy()
    {
        Vector3 troupPos = this.transform.position;
        GameObject mObj = null;
        float m = Mathf.Infinity;
        RaycastHit[] rayHit = Physics.SphereCastAll(troupPos, 20F, transform.forward);

        for (int i = 0; i < rayHit.Length; i++)
        {
            dataObject dataE = rayHit[i].transform.gameObject.GetComponent<dataObject>();
            if (dataE != null && dataE.color != this.data.color) 
            {
                if (rayHit[i].transform.gameObject.tag == "Troups" && rayHit[i].transform.gameObject != this.gameObject)
                {
                    float t = getPhyta(rayHit[i].transform.position,troupPos);
                    if (t < m)
                    {
                        m = t;
                        mObj = rayHit[i].transform.gameObject;
                    }
                }
            }
        }
        toAttack = mObj;
    }
    IEnumerator fight()
    {
        while (true)
        {
            float temp = 1f;
            if (toAttack != null)
            {
                Vector3 troupPos = this.transform.position;
                GameObject mObj = null;
                float m = Mathf.Infinity;
                RaycastHit[] rayHit = Physics.SphereCastAll(troupPos, 20F, transform.forward);

                for (int i = 0; i < rayHit.Length; i++)
                {
                    dataObject dataE = rayHit[i].transform.gameObject.GetComponent<dataObject>();
                    if (dataE != null && dataE.color != this.data.color) 
                    {
                        if (rayHit[i].transform.gameObject.tag == "Troups" && rayHit[i].transform.gameObject != this.gameObject)
                        {
                            float t = getPhyta(rayHit[i].transform.position,troupPos);
                            if (t < m)
                            {
                                m = t;
                                mObj = rayHit[i].transform.gameObject;
                            }
                        }
                    }
                }
                toAttack = mObj;
                if (mObj != null)
                {
                    if (m < 25F)
                    {
                        Reset();
                        dataObject dataEnemi = mObj.GetComponent<dataObject>();
                        if (dataEnemi != null)
                        {
                            if(dataEnemi.currentHealth > data.damage)
                            {
                                anim.Play("MeeleeAttack_OneHanded");
                                yield return new WaitForSeconds(0.55F);
        
                                temp -= 0.55F;
                                dataEnemi.currentHealth -= data.damage;
                            }
                            else
                            {
                                anim.Play("MeeleeAttack_TwoHanded");
                                yield return new WaitForSeconds(0.50F);
        
                                temp -= 0.50F;
                                dataEnemi.currentHealth -= data.damage;
                            }
                        }
                    }
                    else
                    {
                        anim.Play("RunForward");
                        Move(mObj.transform.position);
                    }
                }
                else
                {
                    anim.Play("Idle");
                }   
            }
            yield return new WaitForSeconds(temp);
        }
    }
}

