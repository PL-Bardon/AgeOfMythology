using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildingDefense : MonoBehaviour
{
    public GameObject arrow;

    private Vector3 attackPoint;
    private Vector3 targetPoint;
    private float shootForce;

    void Start()
    {
        shootForce = 25;
    }
    void Update()
    {
        attackPoint = this.transform.position;
    }
    public void startDefense()
    {
        StartCoroutine(infiniteCoroutine());
    }
    IEnumerator infiniteCoroutine()
    {
        attackPoint += new Vector3(0,10,0);
        while (true)
        {
            RaycastHit[] rayHit = Physics.SphereCastAll(attackPoint, 20F, this.transform.forward);
            int cpt = 0;
            for (int i = 0; i < rayHit.Length; i++)
            {
                GameObject obj = rayHit[i].transform.gameObject;
                if (obj.tag == "Troups" && cpt == 0)
                {
                    cpt += 1;

                    targetPoint = obj.transform.position + new Vector3(0,2,0);

                    Vector3 direction = targetPoint - attackPoint;

                    GameObject currentBullet = Instantiate(arrow, attackPoint, arrow.transform.rotation); //store instantiated bullet in currentBullet

                    currentBullet.transform.forward = direction.normalized;

                    currentBullet.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);
                
                }
            }
            yield return new WaitForSeconds(1F);
        }
    }
}
