using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CommandManagement : MonoBehaviour {

    static public CommandManagement instance;
    public List<GameObject> m_Commands;
    // Use this for initialization

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    void Start () {
        
        m_Commands = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        for (int i = m_Commands.Count; i > 0; --i)
            if (m_Commands[i - 1] == null)
                m_Commands.RemoveAt(i - 1);

    }

    public void Add(GameObject command)
    {
        m_Commands.Add(command);
    }

    public void ResetCommandObjects()
    {
        foreach(var commandObj in m_Commands)
        {
            Destroy(commandObj);
        }

        m_Commands.Clear();
    }

    public List<int> CreateCommandList()
    {
        
        List<int> commandList = new List<int>();
        // 람다 함수를 통해 맵에 인스턴싱 되어있는 커맨드 객체를 람다함수를 이용해 왼쪽 -> 오른쪽 순으로 정렬  
        m_Commands.Sort((GameObject a, GameObject b) => {
            if (a.transform.position.x < b.transform.position.x)
                return -1;
            else if (a.transform.position.x == b.transform.position.x)
                return 0;
            else
                return 1;
            });

        //정렬한 객체를 타입에 맞춰 List에 삽입
        for (int i = 0; i < m_Commands.Count; ++i)
        {
            int command = m_Commands[i].GetComponent<UICommandScript>().m_CommandType;

            // Loop의 경우 안에 있는 숫자 텍스트를 반영하여 저장 
            if (command == UICommandScript.COMMAND_LS)
                command += Convert.ToInt32(m_Commands[i].transform.GetChild(0).GetChild(2).GetComponent<Text>().text);
            
            commandList.Add(command);
      
        }
        
        return commandList;
    }
}
    