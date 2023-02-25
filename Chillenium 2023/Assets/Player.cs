using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float _maxSpeed, _acceleration, _jumpHeight;
    [SerializeField] private LayerMask platformLayerMask;
    private float _speed = 0;
    private bool _left, _right, _jump, _flip, _canDoubleJump;
    private string _lastInput = "";
    // Start is called before the first frame update
    void Start() {
        _canDoubleJump = true;
    }

    // Update is called once per frame
    void Update() {
        //If grounded, reset double jump
        if (isGrounded()) {
            _canDoubleJump = true;
        }
        //Check vertical acceleration

        //Detect inputs
        DetectInputs();
        //Move
        if (_right || _left) {
            //Move and accelerate
            Move();
        }
        else {
            //Decrease speed
            Slow();
        }
        //Jump
        if (_jump) {
            Jump();
        }
        
        //Flip
        if (_flip) {

        }
    }

    void DetectInputs() {
        //Detect last L/R input
        if (Input.GetKeyDown(KeyCode.D)) {
            if (_lastInput.Equals("left")) {
                _speed = 0;
            }
            _lastInput = "right";
        }
        else if (Input.GetKeyDown(KeyCode.A)) {
            if (_lastInput.Equals("right")) {
                _speed = 0;
            }
            _lastInput = "left";
        }
        //Move right
        if (Input.GetKey(KeyCode.D)) {
            _right = true;
        }
        else {
            _right = false;
        }
        //Move left
        if (Input.GetKey(KeyCode.A)) {
            _left = true;
        }
        else {
            _left = false;
        }
        //Jump
        if (Input.GetKeyDown(KeyCode.J)) {
            _jump = true;
        }
        else {
            _jump = false;
        }
        //Flip
        if (Input.GetKeyDown(KeyCode.K)) {
            _flip = true;
        }
        else {
            _flip = false;
        }
    }

    void Move(){
        Vector3 position = transform.position;
        float direction = 0;
        if (_lastInput.Equals("right")) {
            direction = 1;
        }
        else if (_lastInput.Equals("left")) {
            direction = -1;
        }
        if (_speed < _maxSpeed) {
            _speed += _acceleration * Time.deltaTime;
            //Overshot correction
            if (_speed > _maxSpeed) {
                _speed = _maxSpeed;
            }
        }
        position.x = transform.position.x + _speed * direction * Time.deltaTime;
        transform.position = position;
    }

    void Slow() {
        //Decrease to 0
        if (_speed > 0) {
            _speed -= _acceleration;
            //Overshot correction
            if (_speed < 0) {
                _speed = 0;
            }
        }
    }

    void Jump() {
        //If touching ground, jump
        if (isGrounded()) {
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
                                    0f, Vector2.down,1f,platformLayerMask);
        Debug.Log("Grounded: " + raycastHit.collider != null);
        return raycastHit.collider != null;
    }
}
