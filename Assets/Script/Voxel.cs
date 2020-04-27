using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxel : MonoBehaviour {

    private const int MAX_TYPE = 2;
    private int m_type;
    private int m_typeHashcode;
    private Animator m_ani;
    private Voxel m_child;

    public bool m_AnimationOnce = false;
    public bool m_EndAnimation = true;
    private bool m_initChild = false;

    private void Start()
    {
        m_type = 3;
        m_ani = GetComponent<Animator>();
        m_child = transform.GetComponentInChildren<Voxel>();
    }

    private void Update()
    {
        if (m_ani == null)  m_ani = GetComponent<Animator>();
        if (m_ani != null)  m_ani.SetInteger("Type", m_type);
        if (m_child != null && !m_initChild)
        {
            m_child.Init(m_type);
            m_initChild = true;
        }
    }

    public void ResetObj()
    {
        m_AnimationOnce = false;
        m_EndAnimation = true;
        
        m_ani.SetBool("Reset", true);
    }

    public void Init(int n)
    {
        m_type = n % MAX_TYPE;
    }

    Color MakeColor(float r, float g, float b, float a)
    {
        Color newColor = Color.white;
        newColor.r = r;
        newColor.g = g;
        newColor.b = b;
        newColor.a = a;

        return newColor;
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Player>().EnableMove();
        Debug.Log(collision.gameObject.GetComponent<Player>().m_State);

        if (m_ani == null)  m_ani = GetComponent<Animator>();
        if (m_ani != null)
        {
            Debug.Log("CollEnter");
            m_ani.SetBool("Reset", true);
            m_ani.SetBool("Collision", true);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerOriginPos" && !m_AnimationOnce)
        {
            m_AnimationOnce = true;
            m_EndAnimation = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (m_ani == null) m_ani = GetComponent<Animator>();
        if (m_ani != null)
        {
            m_ani.SetBool("Reset", false);
            m_ani.SetBool("Collision", false);
        }
        m_AnimationOnce = false;
    }
}
