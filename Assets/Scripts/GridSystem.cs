using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    Camera cam;
    RaycastHit obj;

    public GameObject playerUnitPrefab;
    public GameObject deletePlayerUnitPrefab;
    public GameObject swordsman;
    public GameObject musketeer;
    public GameObject smg;
    public GameObject machinegun;
    public GameObject cannon;

    public List<GameObject> playerUnits;
    public List<GameObject> enemyUnits;
    public List<float> enemyDistance;
    public List<float> playerDistance;

    Buttons buttons;
    GameObject placedUnit;
    Level level;

    void Start()
    {
        playerUnits = new List<GameObject>();
        enemyUnits = new List<GameObject>();
        playerDistance = new List<float>();
        enemyDistance = new List<float>();

        //D��man birimleri ba�lang��ta diziye atan�r
        enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit").ToList<GameObject>();
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        buttons = GameObject.Find("ButtonManager").GetComponent<Buttons>();
        level = GameObject.Find("Level").GetComponent<Level>();


    }

    void FixedUpdate()
    {


        #region Birlik se�imi
        if (buttons.isSelectedSwordsman)
        {
            playerUnitPrefab = swordsman;
            buttons.isSelectedSwordsman = false;
        }
        if (buttons.isSelectedmusketeer)
        {
            playerUnitPrefab = musketeer;
            buttons.isSelectedmusketeer = false;
        }
        if (buttons.isSelectedSmg)
        {
            playerUnitPrefab = smg;
            buttons.isSelectedSmg = false;
        }
        if (buttons.isSelectedMachinegun)
        {
            playerUnitPrefab = machinegun;
            buttons.isSelectedMachinegun = false;
        }
        if (buttons.isSelectedCannon)
        {
            playerUnitPrefab = cannon;
            buttons.isSelectedCannon = false;
        }
        #endregion

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (level.playerPower < level.playerMaxPower) // Birlik yerle�tirme s�n�r�
        {
            // Birlik yerle�tirme sistemi
            // Mouse a t�kland�ysa, ���n bilgisi out da ise, t�klanan nesne tag � Grid ise, d�v�� ba�lamad�ysa, silme modu a��k de�ilse ve yerle�tirilecek alan bo� ise
            if (Input.GetMouseButton(0) && Physics.Raycast(ray, out obj, 100f) && obj.collider.tag == "Grid" && buttons.isFight == false && buttons.remove == false && obj.collider.gameObject.GetComponent<Grids>().full == false)
            {
                // birlik olu�tur ve oyuncu dizisine ata
                placedUnit = Instantiate(playerUnitPrefab, obj.transform.position + new Vector3(0, 1, 0), Quaternion.Euler(0, 0, 0));
                playerUnits.Add(placedUnit);
                level.AddPower();
            }
        }
        // Birlik silme sistemi
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out obj, 100f) && obj.collider.tag == "PlayerUnit" && buttons.isFight == false && buttons.remove == true)
        {
            deletePlayerUnitPrefab = obj.collider.gameObject;
            playerUnits.Remove(obj.collider.gameObject);
            Destroy(obj.collider.gameObject);
            level.DeletePower();
        }
        // Birlik slindi�inde, tekrar birlik koyabilmek i�in Grids'teki full de�i�keni ayarland�
        if (Input.GetMouseButton(0) && Physics.Raycast(ray, out obj, 100f) && obj.collider.tag == "Grid" && buttons.isFight == false && buttons.remove == true)
        {
            obj.collider.gameObject.GetComponent<Grids>().full = false;
        }
    }

}
