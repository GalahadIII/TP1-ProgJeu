using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimEvent : MonoBehaviour
{
    private SkeletonBehaviour s_Behaviour;
    private Animator m_Anim;
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Start()
    {
        s_Behaviour = GetComponentInParent<SkeletonBehaviour>();
        m_Anim = GetComponent<Animator>();
    }
    
    private void Cooling()
    {
        m_Anim.SetBool(Attack, false);
        s_Behaviour.cooling = true;
    }

}
