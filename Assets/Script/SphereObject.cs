using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereObject : MonoBehaviour {

    public GameObject m_SphereObj;
    private Animator m_ani;
    private int m_liveHashcode;

    private bool m_live;

    private void Start()
    { 
        m_live = true;
        m_liveHashcode = "Live".GetHashCode();
    }

    private void Update()
    {
        if (m_ani == null)  m_ani = GetComponent<Animator>();
        if (m_ani != null)  m_ani.SetBool(m_liveHashcode, m_live);
        if(m_SphereObj == null) m_SphereObj = transform.GetChild(0).gameObject;
    }

    public void SetLive(bool live)
    {
        m_live = live;
        m_ani.SetBool(m_liveHashcode, m_live);
    }

    public void ResetObj()
    {
        m_SphereObj.GetComponent<MeshRenderer>().enabled = true;
        m_SphereObj.GetComponent<SphereCollider>().enabled = true;
    }
}
