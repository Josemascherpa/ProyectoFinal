using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    [SerializeField] private GameObject fox;

    private void Awake()
    {
        
    }
    private void Start()
    {
        Instantiate(fox, this.transform.GetChild(0).transform.position,Quaternion.identity);
    }

    private void Update()
    {
        
    }
}
