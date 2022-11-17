using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public static bool touchPlayMainMenu = false;
    public static int Level = 0;
    public static Vector3 positionLevels;
    public static Quaternion rotationLevels;
    
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    
    
}
