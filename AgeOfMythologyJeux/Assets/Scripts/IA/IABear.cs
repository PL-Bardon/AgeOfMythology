using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABear : IA
{
    private Vector3 spawnPoint;

    void Start()
    {
        anim = this.gameObject.GetComponent<Animator>();
        this.data = this.gameObject.GetComponent<dataBear>();
        spawnPoint = this.transform.position;
        StartCoroutine(fight());
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
    IEnumerator fight()
    {
        bool isSit = false;
        spawnPoint = this.transform.position;
        while (true)
        {
            float temp = 2f;
            Vector3 bearPos = this.transform.position;
            GameObject mObj = null;
            float m = Mathf.Infinity;
            RaycastHit[] rayHit = Physics.SphereCastAll(bearPos, 20F, transform.forward);

            for (int i = 0; i < rayHit.Length; i++)
            {
                if (rayHit[i].transform.gameObject.tag == "Troups")
                {
                    float t = getPhyta(rayHit[i].transform.position,bearPos);
                    if (t < m)
                    {
                        m = t;
                        mObj = rayHit[i].transform.gameObject;
                    }
                }
            }
            if (mObj != null)
            {
                if (isSit)
                {
                    isSit = false;
                    anim.Play("Buff");
                    yield return new WaitForSeconds(1F);
                    temp -= 1F;
                }
                if (m < 64F)
                {
                    Reset();
                    dataObject dataEnemi = mObj.GetComponent<dataObject>();
                    if (dataEnemi != null && dataEnemi.currentHealth > 0)
                    {
                        float reduce = 0;
                        if (dataEnemi.currentHealth < this.data.FinalDamage)
                        {
                            anim.Play("Attack5");
                            yield return new WaitForSeconds(0.86F);
                            reduce = 0.86F; 
                            dataEnemi.currentHealth -= data.FinalDamage;   
                        }   
                        else
                        {
                            System.Random rn = new System.Random();
                            if (rn.Next(0,2) == 1)
                            {    
                                anim.Play("Attack1");
                                yield return new WaitForSeconds(0.7f);
                            }
                            else
                            {
                                anim.Play("Attack2");
                                yield return new WaitForSeconds(0.7f);   
                            }
                            reduce = 0.7F; 
                            dataEnemi.currentHealth -= data.damage;
                        }
                        temp -= reduce;
                        
                    }
                }
                else
                {
                    anim.Play("RunForward");
                    Move(mObj.transform.position);
                }
            }
            else if (!compV3(this.transform.position,spawnPoint,1F))
            {
                anim.Play("Walk Forward");
                Move(spawnPoint);
            }
            else
            {
                anim.Play("Sit");
                isSit = true;
            }
            temp = Mathf.Clamp(temp,0,5);
            yield return new WaitForSeconds(temp);
        }
    }
}
