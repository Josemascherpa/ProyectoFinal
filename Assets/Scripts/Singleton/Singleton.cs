using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public static bool touchPlayMainMenu = false;
    public static int Level = 1;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    
}
