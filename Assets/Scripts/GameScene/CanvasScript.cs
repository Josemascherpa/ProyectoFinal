using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject chicken;
    [SerializeField] private GameObject cuchaPrefab;
    [SerializeField] private GameObject ballPrefab;
    private GameObject ball;
    private GameObject cucha;
    [SerializeField] private Text chickenText;
    private GameObject Fox;     
    private bool cuchaInstance = false;    
    [SerializeField] private GameObject chickenCollect;
    public static bool boolBall = false;
    private float timeBall = 0;
    private TMP_Text impScreen;
    private void Start()
    {
        impScreen = GameObject.FindGameObjectWithTag("Text").GetComponent<TMP_Text>();
    }

    void Update()
    {
        SearchObjects();        
        TimeRespawnBall();
        Texts();
    }
    void TimeRespawnBall()
    {
        if (ball == null && boolBall)
        {

            timeBall += Time.deltaTime;
            if (timeBall > 20f)//TIEMPO RESPAWN
            {
                this.transform.GetChild(5).gameObject.SetActive(true);
                boolBall = false;
            }
        }
    }
    void SearchObjects()
    {        
        if (Fox == null)
        {
            Fox = GameObject.FindGameObjectWithTag("fox");
        }
    }

    private void Texts()
    {
        //chickenText.text = Singleton.chicken + "F";
    }

    public void InstantiateChicken()
    {
        impScreen.text = "VE A COMER TUS POLLITOS!!";
        /*if (Singleton.chicken > 0)
        {
            //Instanciar prefabs para conseguir mas pollitos y deben colisionar con la pelota
            Instantiate(chicken, UbicationTerrain(3), Quaternion.identity);
            Singleton.chicken--;
        }
        TouchEat.UIDetect = false;*/
    }

    public void InstantiateBall()
    {
        impScreen.text = "EMPUJA LA PELOTA PARA AGARRAR LOS POLLITOS";
        /*if (ball == null && !boolBall)
        {
            this.transform.GetChild(5).gameObject.SetActive(false);
            timeBall = 0;
            var terreno = GameObject.FindGameObjectWithTag("terrain");
            ball = Instantiate(ballPrefab, terreno.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.transform.position, Quaternion.identity);
            for (int i = 0; i < 15; i++)
            {
                Instantiate(chickenCollect);
            }            
        
        }*/

    }

    public void InstantiateCucha()
    {
        impScreen.text = "ENCUENTRA LA CASITA!!";
        if (!cuchaInstance)
        {
            var terreno = GameObject.FindGameObjectWithTag("terrain");
            cucha = Instantiate(cuchaPrefab, new Vector3(UbicationTerrain(2).x, terreno.transform.position.y+0.01f, UbicationTerrain(2).z), Quaternion.identity);
            cucha.transform.LookAt(Fox.transform);
            cuchaInstance = true;
        }
    }

    private Vector3 UbicationTerrain(int numero)
    {
        var terreno = GameObject.FindGameObjectWithTag("terrain");
        var vector3 = terreno.transform.GetChild(0).gameObject.transform.GetChild(numero).gameObject.transform.position;
        return vector3;
    }

}
