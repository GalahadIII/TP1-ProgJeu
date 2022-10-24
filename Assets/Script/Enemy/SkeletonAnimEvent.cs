using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimEvent : MonoBehaviour
{
    private EnemyBehaviour s_Behaviour;
    private Animator m_Anim;
    
    private void Start()
    {
        s_Behaviour = GetComponentInParent<EnemyBehaviour>();
        m_Anim = GetComponent<Animator>();
    }
    
    private void Cooling()
    {
        m_Anim.SetBool("Attack", false);
        s_Behaviour.cooling = true;
    }
}
