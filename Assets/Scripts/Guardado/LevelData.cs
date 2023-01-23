[System.Serializable]//Habilito serializacion de datos para convertir datos a binarios y guardarlos
public class LevelData
{
    public int level;

    public LevelData(int levelPlayer)
    {
        level = levelPlayer;
    }
}
