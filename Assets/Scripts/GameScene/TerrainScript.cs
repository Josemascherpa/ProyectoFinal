using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TerrainScript : MonoBehaviour
{
    [SerializeField] private GameObject fox;
    private GameObject foxInstantiate;
    private GameObject canvas;

   
    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("canvas");
        foxInstantiate = Instantiate(fox, this.transform.GetChild(0).transform.position,Quaternion.identity);
        Invoke("MoveFox", 3f);
        switch (Singleton.Level)
        {
            case 0: canvas.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "DEBES PASAR AL OTRO LADO SIN CAERTE EN LA LAVA"; break;
            case 1: canvas.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "DEBES PASAR POR LAS PLATAFORMAS Y LLEGAR AL OTRO LADO"; break;
            case 2: canvas.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "DEBES EMPUJAR LA PELOTA Y JUNTAR TODOS LOS POLLITOS"; break;
            default: break;
        }
    }

    private void Update()
    {
        
    }
    public void MoveFox()
    {
        foxInstantiate.GetComponent<TouchEat>().iniciateMove = true;
    }
}
