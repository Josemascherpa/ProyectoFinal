using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    [SerializeField] private GameObject fox;
    private GameObject foxInstantiate;

    private void Awake()
    {
        
    }
    private void Start()
    {
        foxInstantiate = Instantiate(fox, this.transform.GetChild(0).transform.position,Quaternion.identity);
        Invoke("MoveFox", 3f);
    }        

    public void MoveFox()
    {
        foxInstantiate.GetComponent<TouchEat>().iniciateMove = true;
    }
}
