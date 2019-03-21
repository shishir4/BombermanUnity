using UnityEngine;

public enum TileType
{
    Empty,
    Metal,
    Brick,
    Item,
    Bomb
}
public class Tile
{   
    public TileType tileType = TileType.Empty;
    public LevelObject levelObject = null;
    public Tile()
    {

    }
    public Tile(TileType _tileType)
    {
        tileType = _tileType;
    }
}

public class LevelObject : MonoBehaviour
{
    public virtual void DestroyIt(){

    }
}