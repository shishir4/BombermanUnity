using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
    Rigidbody2D _rb;
    Animator anim;
    Vector2 movement;

    float moveSpeed = 5f;
    float horizontal, vertical;
    void Awake () {
        _rb = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
    }

    void Update () {
        horizontal = vertical = 0f;
        // Vector2 dirn=Random.Range(0f, 1f)>0.5f?
        horizontal = -1f;
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast (transform.position, -Vector2.up);
        // Debug.DrawRay (transform.position, -Vector2.up);
        // Debug.DrawRay (transform.position.y, -Vector2.down);
        // If it hits something...
        if (hit.collider != null) {
            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            float distance = Mathf.Abs (hit.point.y - transform.position.y);
            // Hspeed = "";
            // float heightError = floatHeight - distance;
            Debug.Log (hit.transform.gameObject.name + " " + distance);
            // The force is proportional to the height error, but we remove a part of it
            // according to the object's speed.
            // float force = liftForce * heightError - rb2D.velocity.y * damping;

            // Apply the force to the rigidbody.
            // rb2D.AddForce(Vector3.up * force);
        }
        // horizontal = Input.GetKey(controlsDict["left"]) && Input.GetKey(controlsDict["right"]) ? 0f : Input.GetKey(controlsDict["left"]) ? -1f : Input.GetKey(controlsDict["right"]) ? 1f : 0f;
        // vertical = Input.GetKey(controlsDict["bottom"]) && Input.GetKey(controlsDict["up"]) ? 0f : Input.GetKey(controlsDict["bottom"]) ? -1f : Input.GetKey(controlsDict["up"]) ? 1f : 0f;
        movement = (((Vector2.right * horizontal + (Vector2.up * vertical))) * moveSpeed * Time.deltaTime);

        // anim.SetFloat("Vspeed", movement.y * 10);
        // anim.SetFloat("Hspeed", movement.x * 10);

    }

    void FixedUpdate () {
        //    myBody.MovePosition(transform.position+(((Vector3.right*Input.GetAxisRaw("Horizontal"))+(Vector3.up*(Input.GetAxisRaw("Vertical"))))*moveSpeed*Time.deltaTime));
        _rb.MovePosition (_rb.position + movement);
        // Debug.Log(myBody.position+" "+movements);
    }

}