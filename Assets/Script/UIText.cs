using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour {

    public string m_BaseString;
    public Text m_Text;

	// Use this for initialization
	void Start () {
        m_Text = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        m_Text.text = m_BaseString + GameManagement.instance.m_JumpCount.ToString();
    }
}
