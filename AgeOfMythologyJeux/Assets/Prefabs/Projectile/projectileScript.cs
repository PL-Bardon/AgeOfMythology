using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileScript : MonoBehaviour
{
    public int damage = 10;

    void Start()
    {
        StartCoroutine(delete());
    }

    IEnumerator delete()
    {
        yield return new WaitForSeconds(3F);
        Destroy(this.gameObject);
    }
}
