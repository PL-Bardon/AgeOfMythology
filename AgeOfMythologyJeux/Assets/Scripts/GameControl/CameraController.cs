using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cam;

    private float width;
    private float height;

    private bool escape;

    void Start()
    {
        width = Screen.width - 10;
        height = Screen.height - 10;
        
        Cursor.lockState = CursorLockMode.Confined; 
        Cursor.visible = true;
        escape = false;
    }
    void Update()
    {
        //x = width
        //y = height
        if(Input.GetKeyDown(KeyCode.Escape))
            escape = !escape;
        if (!escape)
        {
            Vector3 m_pos = Input.mousePosition;

            if (m_pos.x >= width)
            {
                cam.position += new Vector3((m_pos.x - width + 10) * Time.deltaTime / 3 , 0, 0);
            }
            if (m_pos.x <= 10)
            {
                if  (m_pos.x > -10)
                    cam.position += new Vector3((m_pos.x - 10) * Time.deltaTime / 3 ,0,0);
                else 
                    cam.position -= new Vector3((-m_pos.x - 10) * Time.deltaTime / 3 ,0,0);
            }
            if (m_pos.y >= height)
            {
                cam.position += new Vector3(0, 0, (m_pos.y - height + 10) * Time.deltaTime / 3 );
            }
            if (m_pos.y <= 10)
            {
                if  (m_pos.y > 0)
                    cam.position -= new Vector3(0, 0, (m_pos.y + 10) * Time.deltaTime / 3 );
                else
                    cam.position -= new Vector3(0, 0, (-m_pos.y - 10) * Time.deltaTime / 3 );
            }
        }
    }
}
