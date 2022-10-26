using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab_Skeleton;
    [SerializeField] private Transform m_RighLimit;
    [SerializeField] private Transform m_LeftLimit;

    private void Start()
    {
        GameObject t_Skeleton = Instantiate(prefab_Skeleton, transform.position, Quaternion.identity, transform);
        SkeletonBehaviour skel = t_Skeleton.GetComponent<SkeletonBehaviour>();
        skel.leftLimit = m_LeftLimit;
        skel.rightLimit = m_RighLimit;
    }

    
}
