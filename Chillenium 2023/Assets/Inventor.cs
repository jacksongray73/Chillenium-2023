using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventor : MonoBehaviour {
    [SerializeField] private float _maxSpeed, _acceleration, _jumpHeight;
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] public string controlStyle;
    [SerializeField] Robot robot;
    private float _speed = 0;
    private bool _left, _right, _jump, _interact, _canDoubleJump;
    private string _lastInput = "";
    private Dictionary<string, bool> _inputs;
    public string command;

    
    // Start is called before the first frame update
    void Start() {
        
    }

    void Awake() {
        command = "follow";

    }

    // Update is called once per frame
    void Update() {
        //Map inputs
        _inputs = new Dictionary<string, bool>();
        if (controlStyle == "one-handed") {
            _inputs.Add("Right", Input.GetKey(KeyCode.D));
            _inputs.Add("Left", Input.GetKey(KeyCode.A));
            _inputs.Add("Jump", Input.GetKeyDown(KeyCode.Space));
            _inputs.Add("Action1", Input.GetKey(KeyCode.W));
            _inputs.Add("Command", Input.GetKeyDown(KeyCode.S));
            _inputs.Add("RightDown", Input.GetKeyDown(KeyCode.D));
            _inputs.Add("LeftDown", Input.GetKeyDown(KeyCode.A));
        }
        else if (controlStyle == "two-handed") {
            _inputs.Add("Right", Input.GetKey(KeyCode.D));
            _inputs.Add("Left", Input.GetKey(KeyCode.A));
            _inputs.Add("Jump", Input.GetKeyDown(KeyCode.J));
            _inputs.Add("Action1", Input.GetKey(KeyCode.K));
            _inputs.Add("Command", Input.GetKeyDown(KeyCode.L));
            _inputs.Add("RightDown", Input.GetKeyDown(KeyCode.D));
            _inputs.Add("LeftDown", Input.GetKeyDown(KeyCode.A));
        }
        //else if (controlStyle == "controller") {
        //    _inputs.Add("Right", Input.GetKey(KeyCode.D));
        //    _inputs.Add("Left", Input.GetKey(KeyCode.A));
        //    _inputs.Add("Jump", PlayerControls.);
        //    _inputs.Add("Action1", Input.GetKey(KeyCode.W));
        //    _inputs.Add("Command", Input.GetKeyDown(KeyCode.S));
        //    _inputs.Add("RightDown", Input.GetKeyDown(KeyCode.D));
        //    _inputs.Add("LeftDown", Input.GetKeyDown(KeyCode.A));
        //}


        //Detect inputs
        DetectInputs();
        //Move
        if (_right ^ _left) {
            //Move and accelerate
            Move();
            if (command.Equals("follow")) {
                //Robot moves
                robot.Move();
            }
        }
        else {
            //Decrease speed
            Slow();
            if (command.Equals("follow")) {
                //Robot slows
                robot.Slow();
            }
        }
        //Jump
        if (_jump) {
            Jump();
            if (command.Equals("follow")){
                //Robot jumps
                robot.Jump();
            }
        }
        
        //Interact
        if (_interact) {

        }
    }

    void DetectInputs() {
        //Detect last L/R input
        if (_inputs["RightDown"]) {
            if (_lastInput.Equals("left")) {
                _speed = 0;
                robot.speed = 0;
            }
            _lastInput = "right";
            robot.lastInput = "right";
        }
        else if (_inputs["LeftDown"]) {
            if (_lastInput.Equals("right")) {
                _speed = 0;
                robot.speed = 0;
                
            }
            _lastInput = "left";
            robot.lastInput = "left";
            
        }
        //Move right
        if (_inputs["Right"]) {
            _right = true;
        }
        else {
            _right = false;
        }
        //Move left
        if (_inputs["Left"]) {
            _left = true;
        }
        else {
            _left = false;
        }
        //Jump
        if (_inputs["Jump"]) {
            _jump = true;
        }
        else {
            _jump = false;
        }
        //Interact
        if (_inputs["Action1"]) {
            _interact = true;
        }
        else {
            _interact = false;
        }
        //Change command
        if (_inputs["Command"]) {
            if (command.Equals("follow")) {
                command = "stay";
            }
            else {
                command = "follow";
            }
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
                                    0f, Vector2.down,.01f,platformLayerMask);
        return raycastHit.collider != null;
    }
}
