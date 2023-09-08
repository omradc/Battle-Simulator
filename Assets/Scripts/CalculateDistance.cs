using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CalculateDistance : MonoBehaviour
{
    GridSystem gSystem;
    bool enemyCycle;
    bool playerCycle;
    int enemyUnit�ndex;
    int playerUnit�ndex;
    float eMin;
    float pMin;
    void Start()
    {
        enemyCycle = true;
        playerCycle = true;
        gSystem = GameObject.Find("Grid System").GetComponent<GridSystem>();
    }
    private void Update()
    {

    }
    public GameObject firstEnemy()
    {
        if (enemyCycle) // Birlikler hareket ediyorsa
        {
            gSystem.enemyDistance.Clear(); // d��man de�erleri dizisi her d�ng�de temizlenmeli
            for (int i = 0; i < gSystem.enemyUnits.Count; i++)
            {
                // herhangi bir d��man de�eri bo� ise kald�r
                if (gSystem.enemyUnits[i] == null)
                    gSystem.enemyUnits.Remove(gSystem.enemyUnits[i]);

                // d��man uzakl�k de�erleri distance dizisinde toplan�r
                gSystem.enemyDistance.Add(Vector3.Distance(transform.position, gSystem.enemyUnits[i].transform.position));

            }

            // distance eleman say�s� s�f�r olmamal�          
            if (gSystem.enemyDistance.Count != 0)
            {
                eMin = gSystem.enemyDistance[0];
                enemyUnit�ndex = 0;
                for (int j = 0; j < gSystem.enemyDistance.Count; j++)
                {
                    // min de�eri distance dizisndeki her elmanla kar��la�t�r�l�r. 
                    if (eMin > gSystem.enemyDistance[j]) // Min de�eri, o andaki distance elman�ndan b�y�kse 
                    {
                        eMin = gSystem.enemyDistance[j];
                        enemyUnit�ndex = j; // ***D��man dizisi indexi ile distance dizisi indexlerini ba�lar***
                    }
                }
            }


            enemyCycle = false;
        }

        // D��man birliklerinin hareket etmesi durumunu kontrol eder.
        for (int i = 0; i < gSystem.enemyUnits.Count; i++)
        {
            enemyCycle = gSystem.enemyUnits[i].transform.hasChanged; //cycyle = true
        }

        return gSystem.enemyUnits[enemyUnit�ndex];
    }

    public GameObject firstPlayer()
    {
        //print("first player...");

        if (playerCycle) // Birlikler hareket ediyorsa
        {
            gSystem.playerDistance.Clear(); // d��man de�erleri dizisi her d�ng�de temizlenmeli
            for (int i = 0; i < gSystem.playerUnits.Count; i++)
            {
                if (gSystem.playerUnits[i] == null)
                    gSystem.playerUnits.Remove(gSystem.playerUnits[i]);

                // d��man uzakl�k de�erleri distance dizisinde toplan�r
                gSystem.playerDistance.Add(Vector3.Distance(transform.position, gSystem.playerUnits[i].transform.position));

            }

            // distance eleman say�s� s�f�r olmamal�          
            if (gSystem.playerDistance.Count != 0)
            {
                pMin = gSystem.playerDistance[0];
                playerUnit�ndex = 0;
                for (int j = 0; j < gSystem.playerDistance.Count; j++)
                {
                    // min de�eri distance dizisndeki her elmanla kar��la�t�r�l�r. 
                    if (pMin > gSystem.playerDistance[j]) // Min de�eri, o andaki distance elman�ndan b�y�kse 
                    {
                        pMin = gSystem.playerDistance[j];
                        playerUnit�ndex = j; // ***D��man dizisi indexi ile distance dizisi indexlerini ba�lar***
                    }
                }
            }


            playerCycle = false;
        }

        // D��man birliklerinin hareket etmesi durumunu kontrol eder.
        for (int i = 0; i < gSystem.playerUnits.Count; i++)
        {
            playerCycle = gSystem.playerUnits[i].transform.hasChanged; //cycyle = true
        }

        return gSystem.playerUnits[playerUnit�ndex];
    }
}
