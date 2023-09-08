using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitController : MonoBehaviour
{
    public float speed;
    public float attcakRange;
    public float attackSpeed;
    public float rotateSpeed = 100;
    //public float attcakPower;
    public float qounterDeadTime;
    float distance;
    float estimatedTime;
    [HideInInspector] public bool isMoveing;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public GameObject qounterUnit;
    CalculateDistance cDistance;
    GridSystem gSystem;
    Buttons buttons;
    Effects effects;
    void Start()
    {
        cDistance = GetComponent<CalculateDistance>();
        gSystem = GameObject.Find("Grid System").GetComponent<GridSystem>();
        buttons = GameObject.Find("ButtonManager").GetComponent<Buttons>();
        effects = transform.GetChild(0).GetComponent<Effects>();

    }

    // Update is called once per frame
    void Update()
    {
        // Script oyuncu birimlerinde ise, ve sadece düþman sayýsý 0 dan büyükse hesap eder
        if (gameObject.CompareTag("PlayerUnit") && gSystem.enemyUnits.Count > 0)
        {
            // tüm düþmanlar ölürse Idle animasyonu oynar
            if (gSystem.enemyUnits.Count <= 0)
                effects.IdleAnim();


            // en yakýn hedef qounterUnit dir
            qounterUnit = cDistance.firstEnemy();

            //Düþman ile Oyuncu arasýndaki mesafe;
            distance = Vector3.Distance(transform.position, qounterUnit.transform.position);
        }

        if (gameObject.CompareTag("EnemyUnit") && gSystem.playerUnits.Count > 0)
        {
            if (gSystem.playerUnits.Count <= 0)
                effects.IdleAnim();

            qounterUnit = cDistance.firstPlayer();
            distance = Vector3.Distance(transform.position, qounterUnit.transform.position);
        }

        //Birim hareket kontrolü; en yakýn karþý birime doðru, saldýrý menziline kadar hareket eder
        if (buttons.isFight == true && qounterUnit != null)
        {
            
            direction = (qounterUnit.transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(direction); // Bakýþ yonü


            if (distance > attcakRange)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);

                //hareket edip etmeme durumu kontrol edilir
                isMoveing = true;
                effects.RunAnim();
            }
            else
                isMoveing = false;

        }


        // saldýrý menziline girdiðinde /  distance != 0 olamlý çünkü oyun baþýnda distance 0 deðerini alýyor / 
        //butona bastýðýnda saldýrmalý.
        if (distance <= attcakRange && distance != 0 && buttons.isFight == true && qounterUnit != null)
        {
            Attack(attackSpeed);

            if (qounterUnit.GetComponent<HealthBar>().health <= 0)
            {
                Dead();
            }
        }





    }

    // sadece animasyon oynar
    void Attack(float delay)
    {
        if (Time.time >= estimatedTime)
        {
            //saldýrý animasyonu baþlar
            effects.AttackAnim();

            estimatedTime = Time.time + delay;
        }
    }

    public void Dead()
    {
        if (gameObject.CompareTag("PlayerUnit"))  // Script oyuncu birimlerinde ise;
        {
            // Kraþý birim ölmeden önce enemyUnits dizisinden kaldýrýr
            gSystem.enemyUnits.Remove(qounterUnit);
        }
        if (gameObject.CompareTag("EnemyUnit"))// Script düþman birimlerinde ise;
        {
            // Kraþý birim ölmeden önce playerUnits dizisinden kaldýrýr
            gSystem.playerUnits.Remove(qounterUnit);
        }
        Destroy(qounterUnit, qounterDeadTime);

    }
}
