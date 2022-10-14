using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject lightChild;
    //private GameObject darkChild;

    //private SpriteRenderer m_DarkSR;
    private SpriteRenderer m_LightSR;

    //private Animator m_DarkAnim;
    private Animator m_LightAnim;

    void Awake()
    {
        lightChild = GameObject.Find("Light");
        //darkChild = GameObject.Find("Dark");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        m_LightSR = lightChild.GetComponent<SpriteRenderer>();
        //m_DarkSR = darkChild.GetComponent<SpriteRenderer>();

        m_LightAnim = lightChild.GetComponent<Animator>();
        //m_DarkAnim = darkChild.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
