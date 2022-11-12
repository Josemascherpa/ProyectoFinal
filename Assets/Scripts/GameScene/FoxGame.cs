using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FoxGame : MonoBehaviour
{
    private bool sitStart = false;
    private Animator anim;
    private GameObject eat;   
    [SerializeField] private GameObject terrain;
    public static float funny=100;
    private float speed = 1f;
    private float timeFunny = 0;
    private bool relax = false; 
    public static bool death=false;
    private void Awake()
    {
        terrain = GameObject.FindGameObjectWithTag("terrain");
        anim = GetComponent<Animator>();  
    }
    void Start()
    {
        var lookPosition = Camera.main.transform.position - this.transform.position;
        lookPosition.y = 0;
        this.transform.rotation= Quaternion.LookRotation(lookPosition);

    }
    void Update()
    {       
        if (death)
        {
            SceneManager.LoadScene("Death");
        }
        Funny();        
    }

    private void Funny()
    {
        if (funny < 0)
        {
            death = true;
            funny = 0;
        }else if (funny > 100)
        {
            funny = 100;
        }
        timeFunny += Time.deltaTime;
        if (timeFunny >= 1f)//FALTA PONER LIMITE DE MAXIMO Y MINIMO PARA LA BARRA
        {
            
            funny -= 0.5f;
            
            timeFunny = 0;
        }

        if (funny < 50)
        {
            this.transform.GetChild(3).gameObject.SetActive(true);
            this.transform.GetChild(2).gameObject.SetActive(false);
        }
        else {
            this.transform.GetChild(2).gameObject.SetActive(true);
            this.transform.GetChild(3).gameObject.SetActive(false);
        }

    }  

    private void OnCollisionExit(Collision collision)
    {
        /*for (int i = 0; i < 3; i++)
        {
            print("ASDF");
            //this.transform.GetChild(i).gameObject.SetActive(true);
        }*/
    }
   
}
