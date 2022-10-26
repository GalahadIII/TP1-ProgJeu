using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    [SerializeField] private GameObject _vcam;
    private Vector3 _vcamPos;
    
    private void Awake()
    {
    }

    private void Update()
    {
        _vcamPos = _vcam.transform.position;
        transform.position = new Vector3(_vcamPos.x, _vcamPos.y + 0.85f, transform.position.z);
    }
}
