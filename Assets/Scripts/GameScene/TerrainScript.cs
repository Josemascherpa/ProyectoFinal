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
        foxInstantiate.transform.SetParent(this.gameObject.transform);
        Invoke("MoveFox", 3f);
        switch (Singleton.Level)
        {
            case 0: canvas.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "DEBES PASAR AL OTRO LADO SIN CAERTE EN LA LAVA"; break;
            case 1: canvas.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "DEBES PASAR POR LAS PLATAFORMAS Y LLEGAR AL OTRO LADO"; break;
            case 2: canvas.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "DEBES EMPUJAR LA PELOTA Y JUNTAR LOS POLLITOS"; break;
            case 3: canvas.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "DEBES SUBIR ESQUIVANDO LAS DISTINTAS TRAMPAS"; break;            
            default: break;
        }
    }

    public void MoveFox()
    {
        foxInstantiate.GetComponent<TouchFox>().iniciateMove = true;
    }
}
