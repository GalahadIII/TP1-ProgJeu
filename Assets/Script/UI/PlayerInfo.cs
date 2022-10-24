using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private PlayerController s_PC;

    private GameObject gold;
    private GameObject bar1;
    private GameObject bar2;

    private TextMeshProUGUI goldAmount;
    private Image healthBar;
    private Image shadowbar;

    // Start is called before the first frame update
    void Start()
    {
        bar1 = GameObject.Find("Bar1");
        bar1 = GameObject.Find("Bar2");
        gold = GameObject.Find("Gold");

        goldAmount = gold.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        goldAmount.SetText(s_PC.goldCollected.ToString());
    }
}
