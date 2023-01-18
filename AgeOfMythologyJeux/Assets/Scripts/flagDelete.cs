using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flagDelete : MonoBehaviour
{
    private float t = 1.5F;

    void Update()
    {
        t -= Time.deltaTime;
        if (t < 0)
            Destroy(this.gameObject);
    }
}
