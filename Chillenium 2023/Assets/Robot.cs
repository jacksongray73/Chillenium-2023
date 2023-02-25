using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour {
    [SerializeField] float _maxSpeed, _acceleration, _jumpHeight;
    [SerializeField] private LayerMask platformLayerMask;
    public string lastInput;
    public float speed;
    private bool _canDoubleJump;
    //public string command = "follow";
    void Start() {
        
    }

    void Update() {

    }

    public void Move() {
        Vector3 position = transform.position;
        float direction = 0;
        if (lastInput.Equals("right")) {
            direction = 1;
        }
        else if (lastInput.Equals("left")) {
            direction = -1;
        }
        if (speed < _maxSpeed) {
            speed += _acceleration * Time.deltaTime;
            //Overshot correction
            if (speed > _maxSpeed) {
                speed = _maxSpeed;
            }
        }
        position.x = transform.position.x + speed * direction * Time.deltaTime;
        transform.position = position;
    }

    public void Slow() {
        //Decrease to 0
        if (speed > 0) {
            speed -= _acceleration;
            //Overshot correction
            if (speed < 0) {
                speed = 0;
            }
        }
    }

    public void Jump() {
        //If touching ground, jump
        if (isGrounded()) {
            _canDoubleJump = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * _jumpHeight;
        }

        //If not touching groud, check for double jump
        else if (_canDoubleJump) {
            _canDoubleJump = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * _jumpHeight;
        }
    }

    bool isGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(GetComponent<BoxCollider2D>().bounds.center,
                                    GetComponent<BoxCollider2D>().bounds.size,
                                    0f, Vector2.down, .01f, platformLayerMask);
        Debug.Log(raycastHit.collider != null);
        return raycastHit.collider != null;
    }
}
