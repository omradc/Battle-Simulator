using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public float health;
    Slider slider;
    public bool timeToDeathPlayer;
    public bool timeToDeathEnemy;
    GridSystem gSystem;
    void Start()
    {
        gSystem = GetComponent<GridSystem>();
        slider = transform.GetChild(1).transform.GetChild(0).GetComponent<Slider>();
        SetMaxHealth(health);
    }
    void Update()
    {

    }

    public void SetHealthBar(float value)
    {
        slider.value -= value;
    }

    public void SetMaxHealth(float value)
    {
        slider.maxValue = value;
        slider.value = value;
    }
    private void OnTriggerEnter(Collider other)
    {
        // karþý birimden gelecek olan silahlardan hasar alýr
        if (transform.tag == "PlayerUnit" && other.gameObject.tag == "EnemyWeapon")
        {
            health -= other.gameObject.GetComponent<WeaponsDamage>().damage;
            SetHealthBar(other.gameObject.GetComponent<WeaponsDamage>().damage);
        }

        if (transform.tag == "EnemyUnit" && other.gameObject.tag == "PlayerWeapon")
        {
            health -= other.gameObject.GetComponent<WeaponsDamage>().damage;
            SetHealthBar(other.gameObject.GetComponent<WeaponsDamage>().damage);
        }


    }
}
