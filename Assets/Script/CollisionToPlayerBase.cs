using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionToPlayerBase : MonoBehaviour {

    public Player m_Player;
    public Animator m_PlayerAnimator;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            GameManagement.instance.SetPaticle(transform.position);
            GameManagement.instance.m_NowObject--;
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<SphereCollider>().enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (m_Player == null) return;
      
        if (collision.gameObject.tag == "MapObj")
        {
            m_Player.EnableMove();
            m_PlayerAnimator.SetBool("Jump", false);
        }
    }

}
