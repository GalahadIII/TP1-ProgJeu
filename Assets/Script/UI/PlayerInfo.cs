using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [SerializeField] private PlayerController s_PC;
    [SerializeField] private KilleableEntity health;
    [SerializeField] private Image healthBar;
    [SerializeField] private Image shadowbar;

    private int maxHp;
    private GameObject gold;
    private TextMeshProUGUI goldAmount;


    // Start is called before the first frame update
    void Start()
    {
        maxHp = health.hp;
        gold = GameObject.Find("Gold");
        goldAmount = gold.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        goldAmount.SetText(s_PC.goldCollected.ToString());
        healthBar.fillAmount = health.hp * 0.1f;
    }
}
