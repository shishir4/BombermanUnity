using UnityEngine;

public class Brick : LevelObject
{
    public bool hasItem = false;
    Animator animator;
    [SerializeField] GameObject[] Powers;
    [SerializeField] float powerGenerateProbability = 0.25f;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public override void DestroyIt()
    {
        Tile tile = LevelGenerator.Instance.GetTile((int)transform.position.x, (int)transform.position.y);
        tile.tileType = TileType.Empty;
        tile.levelObject = null;
        // Debug.Log("Brick called");
        animator.SetTrigger("Explode");
        // Destroy(gameObject);
    }

    public void Vanish()
    {
        if (Random.Range(0f, 1f) < powerGenerateProbability)
        {
            Instantiate(Powers[Random.Range(0, 4)], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
