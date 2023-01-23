using UnityEngine;
using System.IO;//nos permite trabajar con archivos
using System.Runtime.Serialization.Formatters.Binary;//uso de formateador binario, o sea convertir datos a binarios y guardarlos en el archivo
public static class SaveManager
{

    //Guardado
    public static void SaveLevel(int level)
    {
        LevelData numLevel= new LevelData(level);
        string dataPath = Application.persistentDataPath + "/level.save";//Nos ofrece unity este guardado
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);//Creamos archivo y pasamos la ruta
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, numLevel);//Convierto a binario el leveldata
        fileStream.Close();
        //Dato guardado en el archivo y convertido a binario
    }
    //Carga
    public static LevelData loadLevel()
    {
        string dataPath = Application.persistentDataPath + "/level.save";
        //Verifico si existe archivo
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            LevelData levelData = (LevelData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return levelData;
        }
        else
        {
            Debug.LogError("No se encontro ningun archivo de guardado");
            return null;
        }
    }
    //Borrado
    public static void eliminateData()
    {
        string dataPath = Application.persistentDataPath + "/level.save";
        if (File.Exists(dataPath))
        {
            File.Delete(dataPath);
        }

    }
    public static void OverwriteLevel(int newLevel)
    {
        string dataPath = Application.persistentDataPath + "/level.save";
        //Verifico si existe archivo
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            LevelData levelData = (LevelData)binaryFormatter.Deserialize(fileStream);           
            fileStream.Close();
            
        }
    }
    
}
