using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform cam;

    void LateUpdate()
    {
        Vector3 newPos = cam.position;
        newPos.y = this.transform.position.y;
        newPos.z += 40;

        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90F, cam.eulerAngles.y, 0f);
    }
}
