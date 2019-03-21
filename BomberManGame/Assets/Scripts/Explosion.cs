using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] GameObject explodeCenterPrefab;
    [SerializeField] GameObject explodeHBridgePrefab;
    [SerializeField] GameObject explodeVBridgePrefab;
    [SerializeField] GameObject explodeTopPrefab;
    [SerializeField] GameObject explodeRightPrefab;
    [SerializeField] GameObject explodeBottomPrefab;
    [SerializeField] GameObject explodeLeftPrefab;

    [HideInInspector]
    int explosionPower = 1;



    void Start()
    {
        Init();
    }

    public void SetExplosionPower(int pow)
    {
        explosionPower = pow;
    }

    void Init()
    {
        Instantiate(explodeCenterPrefab, transform.position, Quaternion.identity);

        int topExpRange = 0;
        int leftExpRange = 0;
        int downExpRange = 0;
        int rightExpRange = 0;

        //top
        bool blocked = false;
        for (int i = 1; i <= explosionPower; i++)
        {
            switch (LevelGenerator.Instance.CheckTile((int)transform.position.x, (int)transform.position.y + i))
            {
                case TileType.Empty:
                    topExpRange++;
                    break;
                case TileType.Metal:
                    blocked = true;
                    break;
                case TileType.Brick:
                    blocked = true;
                    LevelGenerator.Instance.GetLevelObject((int)transform.position.x, (int)transform.position.y + i).DestroyIt();
                    break;
                case TileType.Item:
                    blocked = true;

                    break;
                default:
                    blocked = true;
                    break;
            }
            if (blocked)
            {
                break;
            }

        }
        //top
        for (int i = topExpRange; i > 0; i--)
        {
            if (i == topExpRange && !blocked)
            {
                Instantiate(explodeTopPrefab, transform.position + Vector3.up * i, Quaternion.identity);
            }
            else
            {
                Instantiate(explodeVBridgePrefab, transform.position + Vector3.up * i, Quaternion.identity);
            }
        }

        //left
        blocked = false;
        for (int i = 1; i <= explosionPower; i++)
        {
            switch (LevelGenerator.Instance.CheckTile((int)transform.position.x - i, (int)transform.position.y))
            {
                case TileType.Empty:
                    leftExpRange++;
                    break;
                case TileType.Metal:
                    blocked = true;
                    break;
                case TileType.Brick:
                    blocked = true;
                    LevelGenerator.Instance.GetLevelObject((int)transform.position.x - i, (int)transform.position.y).DestroyIt();
                    break;
                case TileType.Item:
                    blocked = true;

                    break;
                default:
                    blocked = true;
                    break;
            }
            if (blocked)
            {
                break;
            }

        }

        for (int i = leftExpRange; i > 0; i--)
        {
            if (i == leftExpRange && !blocked)
            {
                Instantiate(explodeLeftPrefab, transform.position + Vector3.left * i, Quaternion.identity);
            }
            else
            {
                Instantiate(explodeHBridgePrefab, transform.position + Vector3.left * i, Quaternion.identity);
            }
        }

        //bottom
        blocked = false;
        for (int i = 1; i <= explosionPower; i++)
        {
            switch (LevelGenerator.Instance.CheckTile((int)transform.position.x, (int)transform.position.y - i))
            {
                case TileType.Empty:
                    downExpRange++;
                    break;
                case TileType.Metal:
                    blocked = true;
                    break;
                case TileType.Brick:
                    blocked = true;
                    LevelGenerator.Instance.GetLevelObject((int)transform.position.x, (int)transform.position.y - i).DestroyIt();
                    break;
                case TileType.Item:
                    blocked = true;

                    break;
                default:
                    blocked = true;
                    break;
            }
            if (blocked)
            {
                break;
            }

        }
        for (int i = downExpRange; i > 0; i--)
        {
            if (i == downExpRange && !blocked)
            {
                Instantiate(explodeBottomPrefab, transform.position + Vector3.down * i, Quaternion.identity);
            }
            else
            {
                Instantiate(explodeVBridgePrefab, transform.position + Vector3.down * i, Quaternion.identity);
            }
        }
        //right
        blocked = false;
        for (int i = 1; i <= explosionPower; i++)
        {
            switch (LevelGenerator.Instance.CheckTile((int)transform.position.x + i, (int)transform.position.y))
            {
                case TileType.Empty:
                    rightExpRange++;
                    break;
                case TileType.Metal:
                    blocked = true;
                    break;
                case TileType.Brick:
                    blocked = true;
                    LevelGenerator.Instance.GetLevelObject((int)transform.position.x + i, (int)transform.position.y).DestroyIt();
                    break;
                case TileType.Item:
                    blocked = true;
                    break;
                default:
                    blocked = true;
                    break;
            }
            if (blocked)
            {
                break;
            }

        }
        for (int i = rightExpRange; i > 0; i--)
        {
            if (i == rightExpRange && !blocked)
            {
                Instantiate(explodeRightPrefab, transform.position + Vector3.right * i, Quaternion.identity);
            }
            else
            {
                Instantiate(explodeHBridgePrefab, transform.position + Vector3.right * i, Quaternion.identity);
            }
        }

        Destroy(gameObject);
    }

}