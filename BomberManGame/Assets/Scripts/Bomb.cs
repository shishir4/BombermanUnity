
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject Explosion;

    public float explodeTime;
    [SerializeField] float timer;
    int explosionPower = 1;
    bool colliderActive = false;

    public int playerId = -1;
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 1f && !colliderActive)
        {
            colliderActive = true;
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
        if (timer >= explodeTime)
        {
            Explode();
        }
    }

    public void Explode()
    {
        // return;
        GameObject go = Instantiate(Explosion, transform.position, Quaternion.identity);
        go.GetComponent<Explosion>().SetExplosionPower(explosionPower);
        EventManager.BombBlast(playerId);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.Contains("Explode"))
        {
            Explode();
        }
    }

    public void SetPlayerID(int id, int pow)
    {
        playerId = id;
        explosionPower = pow;
    }

}