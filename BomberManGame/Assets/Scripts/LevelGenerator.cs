using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LevelGenerator : MonoBehaviour
{
    // [SerializeField] Dictionary<int,TileType> tile;
    public static LevelGenerator Instance;
    public Tile[,] tile;

    [SerializeField] Transform LevelHolder;
    [SerializeField] GameObject mWallPrefab;
    [SerializeField] GameObject bWallPrefab;
    [SerializeField] Vector2 LevelSize;
    [SerializeField] [Range(0f, 1f)] float bWallIntensity;

    int left;
    int bottom;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        CreateLevel();
    }

    public void CreateLevel()
    {
        DeleteLevel();
        if (Mathf.CeilToInt(LevelSize.x) % 2 == 0)
        {
            LevelSize.x += 1;
        }

        if (Mathf.CeilToInt(LevelSize.y) % 2 == 0)
        {
            LevelSize.y += 1;
        }
        int top = Mathf.CeilToInt(LevelSize.y / 2f);
        int right = Mathf.CeilToInt(LevelSize.x / 2f);
        bottom = -Mathf.FloorToInt(LevelSize.y / 2f);
        left = -Mathf.FloorToInt(LevelSize.x / 2f);
        if (Mathf.FloorToInt(LevelSize.x % 2) == 0)
            right++;
        if (Mathf.FloorToInt(LevelSize.y % 2) == 0)
            bottom++;

        tile = new Tile[(int)LevelSize.x, (int)LevelSize.y];

        for (int x = left - 1; x <= right; x++)
        {
            for (int y = bottom - 1; y <= top; y++)
            {

                bool player1Space = (x == left && y == top - 1 || x == left && y == top - 2 || x == left + 1 && y == top - 1);
                bool player2Space = (x == right - 1 && y == bottom || x == right - 1 && y == bottom + 1 || x == right - 2 && y == bottom);
                bool player3Space = (x == left && y == bottom || x == left && y == bottom + 1 || x == left + 1 && y == bottom);
                bool player4Space = (x == right - 1 && y == top - 1 || x == right - 1 && y == top - 2 || x == right - 2 && y == top - 1);

                //Border
                if (x == left - 1 || x == right || y == top || y == bottom - 1)
                {
                    Instantiate(mWallPrefab, new Vector3(x, y, 0), Quaternion.identity, LevelHolder);
                }
                //Inside Metal
                else if ((x - left - 1) % 2 == 0 && (y - bottom - 1) % 2 == 0)
                {
                    tile[x - left, y - bottom] = new Tile(TileType.Metal); //TileType.Metal;

                    Instantiate(mWallPrefab, new Vector3(x, y, 0), Quaternion.identity, LevelHolder);
                }
                //On and Next to player spawn position
                else if (player1Space || player2Space || player3Space || player4Space)
                {

                    tile[x - left, y - bottom] = new Tile(TileType.Empty);


                    continue;
                }
                //Breakable Wall
                else if (UnityEngine.Random.Range(0f, 1f) < bWallIntensity)
                {
                    // tile[x - left, y - bottom] = TileType.Brick;
                    GameObject go = Instantiate(bWallPrefab, new Vector3(x, y, 0), Quaternion.identity, LevelHolder);
                    tile[x - left, y - bottom] = new Tile(TileType.Brick);

                    tile[x - left, y - bottom].levelObject = go.GetComponent<Brick>();

                }
                else
                {
                    tile[x - left, y - bottom] = new Tile(TileType.Empty); //TileType.Empty;

                }
            }
        }
    }

    public void DeleteLevel()
    {
        if (LevelHolder.childCount <= 0)
        {
            return;
        }
        for (int i = LevelHolder.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(LevelHolder.GetChild(i).gameObject);
        }
    }

    public TileType CheckTile(int x, int y)
    {
        int tileWidth = tile.GetLength(0) - 1;
        int tileHeight = tile.GetLength(1) - 1;
        if (x - left > tileWidth || x - left < 0 || y - bottom > tileHeight || y - bottom < 0)
            return TileType.Metal;
        else
            return tile[x - left, y - bottom].tileType;
    }

    public LevelObject GetLevelObject(int x, int y)
    {
        return tile[x - left, y - bottom].levelObject;
    }
    public Tile GetTile(int x, int y)
    {
        return tile[x - left, y - bottom];
    }
}