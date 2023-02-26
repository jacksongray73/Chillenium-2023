using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventor : MonoBehaviour {
    [SerializeField] private float _maxSpeed, _acceleration, _jumpHeight;
    [SerializeField] private LayerMask platformLayerMask, doorLayerMask;
    [SerializeField] public string controlStyle;
    [SerializeField] Robot robot;
    private float _speed = 0;
    private bool _left, _right, _jump, _interact, _canDoubleJump;
    private string _lastInput = "";
    public Dictionary<string, bool> inputs;
    public string command;

    
    // Start is called before the first frame update
    void Start() {
        Physics2D.IgnoreCollision(robot.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void Awake() {
        command = "follow";

    }

    // Update is called once per frame
    void Update() {

        //Map inputs
        inputs = new Dictionary<string, bool>();
        if (controlStyle == "one-handed") {
            inputs.Add("Right", Input.GetKey(KeyCode.D));
            inputs.Add("Left", Input.GetKey(KeyCode.A));
            inputs.Add("Jump", Input.GetKeyDown(KeyCode.Space));
            inputs.Add("Interact", Input.GetKeyDown(KeyCode.W));
            inputs.Add("Command", Input.GetKeyDown(KeyCode.S));
            inputs.Add("RightDown", Input.GetKeyDown(KeyCode.D));
            inputs.Add("LeftDown", Input.GetKeyDown(KeyCode.A));
        }
        else if (controlStyle == "two-handed") {
            inputs.Add("Right", Input.GetKey(KeyCode.D));
            inputs.Add("Left", Input.GetKey(KeyCode.A));
            inputs.Add("Jump", Input.GetKeyDown(KeyCode.J));
            inputs.Add("Interact", Input.GetKeyDown(KeyCode.K));
            inputs.Add("Command", Input.GetKeyDown(KeyCode.L));
            inputs.Add("RightDown", Input.GetKeyDown(KeyCode.D));
            inputs.Add("LeftDown", Input.GetKeyDown(KeyCode.A));
        }
        //else if (controlStyle == "controller") {
        //    inputs.Add("Right", Input.GetKey(KeyCode.D));
        //    inputs.Add("Left", Input.GetKey(KeyCode.A));
        //    inputs.Add("Jump", PlayerControls.);
        //    inputs.Add("Interact", Input.GetKey(KeyCode.W));
        //    inputs.Add("Command", Input.GetKeyDown(KeyCode.S));
        //    inputs.Add("RightDown", Input.GetKeyDown(KeyCode.D));
        //    inputs.Add("LeftDown", Input.GetKeyDown(KeyCode.A));
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
        //if (_jump) {
        //    Jump();
        //    if (command.Equals("follow")){
        //        //Robot jumps
        //        robot.Jump();
        //    }
        //}
        
        //Interact
        if (_interact) {
            if (command.Equals("follow")) {
                robot.Interact();
            }
        }
    }

    void DetectInputs() {
        //Detect last L/R input
        if (inputs["RightDown"]) {
            if (_lastInput.Equals("left")) {
                _speed = 0;
                robot.speed = 0;
            }
            _lastInput = "right";
            robot.lastInput = "right";
        }
        else if (inputs["LeftDown"]) {
            if (_lastInput.Equals("right")) {
                _speed = 0;
                robot.speed = 0;
                
            }
            _lastInput = "left";
            robot.lastInput = "left";
            
        }
        //Move right
        if (inputs["Right"]) {
            _right = true;
        }
        else {
            _right = false;
        }
        //Move left
        if (inputs["Left"]) {
            _left = true;
        }
        else {
            _left = false;
        }
        //Jump
        if (inputs["Jump"]) {
            _jump = true;
        }
        else {
            _jump = false;
        }
        //Interact
        if (inputs["Interact"]) {
            _interact = true;
        }
        else {
            _interact = false;
        }
        //Change command
        if (inputs["Command"]) {
            if (command.Equals("follow")) {
                command = "stay";
                robot.GetComponent<Animator>().SetBool("Locked", true);
            }
            else {
                command = "follow";
                robot.GetComponent<Animator>().SetBool("Locked", false);
            }
        }
    }

    void Move(){
        Vector3 position = transform.position;
        float direction = 0;
        if (inputs["Right"]) {
            GetComponent<Animator>().SetBool("Walking", true);
            GetComponent<SpriteRenderer>().flipX = false;
            direction = 1;
        }
        else if (inputs["Left"]) {
            GetComponent<Animator>().SetBool("Walking", true);
            GetComponent<SpriteRenderer>().flipX = true;
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

            if (_speed == 0) {
                GetComponent<Animator>().SetBool("Walking", false);
            }

        }
    }

    void Jump() {
        //If touching ground, jump
        if (IsGrounded()) {
            _canDoubleJump = true;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * _jumpHeight;
        }

        //If not touching groud, check for double jump
        else if (_canDoubleJump) {
            _canDoubleJump = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.up * _jumpHeight;
        }
    }

    bool IsGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(GetComponent<BoxCollider2D>().bounds.center,
                                    GetComponent<BoxCollider2D>().bounds.size,
                                    0f, Vector2.down,.01f,platformLayerMask);
        return raycastHit.collider != null;
    }
}