using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CanvasScript : MonoBehaviour
{
    public GameObject managerLevels;
    private GameObject proxLevel;
    private GameObject reiLevel;
    private GameObject targetFox;
    private bool sonido = true;
    private GameObject sonidoLevel;

    private void Awake()
    {
        targetFox = GameObject.FindGameObjectWithTag("targetFox");
        managerLevels = GameObject.FindGameObjectWithTag("managerLevel");
        proxLevel = this.transform.GetChild(2).gameObject;
        reiLevel = this.transform.GetChild(3).gameObject;
    }
    private void Update()
    {
        
        if (sonido)
        {
            sonidoLevel = GameObject.FindGameObjectWithTag("Audio");
            sonidoLevel.GetComponent<AudioSource>().UnPause();
        }else if (!sonido)
        {
            sonidoLevel = GameObject.FindGameObjectWithTag("Audio");
            sonidoLevel.GetComponent<AudioSource>().Pause();
        }
    }
    public void ProximoNivel()
    {
        Singleton.Level += 1;
        //Instanciar level
        Instantiate(managerLevels.GetComponent<ManagerLevels>().listaNiveles[Singleton.Level], Singleton.positionLevels, Singleton.rotationLevels);        
        proxLevel.SetActive(false);
        
    }

    public void ReiniciarLevel()
    {
        //Instanciar mismo lvl
        Instantiate(managerLevels.GetComponent<ManagerLevels>().listaNiveles[Singleton.Level], Singleton.positionLevels, Singleton.rotationLevels);        
        reiLevel.SetActive(false);
        proxLevel.SetActive(false);       

    }
    public void Silencio()
    {
        sonido =! sonido;
        
    }

    public void Salir()
    {
        Application.Quit();
    }
}
