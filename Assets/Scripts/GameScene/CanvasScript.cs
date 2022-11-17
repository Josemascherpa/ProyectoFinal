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

    private void Awake()
    {
        managerLevels = GameObject.FindGameObjectWithTag("managerLevel");
        proxLevel = this.transform.GetChild(2).gameObject;
        reiLevel = this.transform.GetChild(3).gameObject;
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

    public void Salir()
    {
        Application.Quit();
    }
}
