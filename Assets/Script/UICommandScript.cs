using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICommandScript : MonoBehaviour {

    const int TYPE_UNKNOWN = -1;
    const int TYPE_ORIGIN_COMMAND = 0;
    const int TYPE_INST_COMMAND = 1;

    public static int COMMAND_GO = 0;
    public static int COMMAND_TL = 1;
    public static int COMMAND_TR = 2;
    public static int COMMAND_LS = 4;
    public static int COMMAND_LE = 3;

    public int m_Type = TYPE_UNKNOWN;
    public int m_CommandType;
    public bool m_Clicked = false;
    public Transform m_Canvas;

    private void Start()
    {
    }

    private void Update()
    {
        if(m_Type == TYPE_UNKNOWN)
        {
            if (this.gameObject.tag == "InstCommand")
                m_Type = TYPE_INST_COMMAND;
            else if (this.gameObject.tag == "Command")
                m_Type = TYPE_ORIGIN_COMMAND;
        }

        if (m_Clicked)
            transform.position = Input.mousePosition - new Vector3(0.0f, 25.0f, 0.0f);
    }

    public void TaskOnClick()
    {
        if (m_Type == TYPE_ORIGIN_COMMAND)
        {
            CreateInstance();
        }
        else if (m_Type == TYPE_INST_COMMAND)
        {
            if (m_Clicked)
            {
                GameManagement.instance.m_LastClickObj = null;
                m_Clicked = false;
            }
            else
            {
                GameManagement.instance.m_LastClickObj = this;
                m_Clicked = true;
            }
        }
    }

    public void CreateInstance()
    {
        this.tag = "InstCommand";
        m_Type = TYPE_INST_COMMAND;

        GameObject obj = Instantiate(this.gameObject);

        obj.transform.SetParent(m_Canvas);
        obj.transform.position = this.transform.position;
        obj.transform.position -= new Vector3(0, 60.0f, 0.0f);
        this.tag = "Command";
        m_Type = TYPE_ORIGIN_COMMAND;
        CommandManagement.instance.Add(obj);
    }
}
