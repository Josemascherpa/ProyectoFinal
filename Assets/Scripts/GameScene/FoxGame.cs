using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FoxGame : MonoBehaviour
{  
   
    public static bool death=false;
    
    void Start()
    {
        var lookPosition = Camera.main.transform.position - this.transform.position;
        lookPosition.y = 0;
        this.transform.rotation= Quaternion.LookRotation(lookPosition);

    }
    

   

}
