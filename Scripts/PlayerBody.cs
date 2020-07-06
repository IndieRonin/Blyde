using Godot;
using System;

public class PlayerBody : KinematicBody
{
    //The current speed of the player
    float speed;
    //How fast the player moves when crouched
    float crouchMoveSpeed = 3;
    //How fast the player moves when walking 
    float walkSpeed = 7;
    //How fast the player goes into a crouching position
    float crouchSpeed = 26;
    float accelleration = 20;
    float gravity = 9.8f;
    float jump = 5;

    float defualtHeight = 1.5f;
    float crouchHeight = .5f;


    float mouseSensitivity = 0.05f;

    Vector3 direction;
    Vector3 velocity;
    Vector3 fall;
    //The spatiol object repereseting the head of the player
    Spatial head;
    CollisionShape playerCapsule;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
        head = GetNode<Spatial>("Head");
        playerCapsule = GetNode<CollisionShape>("CollisionShape");
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion eventMouseMotion)
        {
            //RotateY(Mathf.Deg2Rad(-@event.relative.x * mouseSensitivity))
            RotateY(Mathf.Deg2Rad(-eventMouseMotion.Relative.x * mouseSensitivity));
            head.RotateX(Mathf.Deg2Rad(-eventMouseMotion.Relative.y * mouseSensitivity));
            //head.Rotation.x = Mathf.Clamp(head.Rotation.x, Mathf.Deg2Rad(-90), Mathf.Deg2Rad(90));
            head.Rotation = new Vector3(Mathf.Clamp(head.Rotation.x, Mathf.Deg2Rad(-90), Mathf.Deg2Rad(90)), head.Rotation.y, head.Rotation.z);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //Set the players speed to the walk speed by defualt
        speed = walkSpeed;

        direction = Vector3.Zero;

        if (!IsOnFloor())
        {
            fall.y -= gravity * delta;
        }
        if (Input.IsActionJustPressed("Jump"))
        {
            fall.y = jump;
        }
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            Input.SetMouseMode(Input.MouseMode.Visible);
        }

        if (Input.IsActionJustPressed("Crouch"))
        {
            CapsuleShape playerColShape = new CapsuleShape();
            playerColShape.Radius = 0.5f;
            playerColShape.Height -= crouchSpeed * delta;
            playerColShape.Height = Mathf.Clamp(playerColShape.Height, crouchHeight, defualtHeight);
            playerCapsule.Shape = playerColShape;
            speed = crouchMoveSpeed;
        }
        else
        {
            CapsuleShape playerColShape = new CapsuleShape();
            playerColShape.Radius = 0.5f;
            playerColShape.Height -= crouchSpeed * delta;
            playerColShape.Height = Mathf.Clamp(playerColShape.Height, crouchHeight, defualtHeight);
            playerCapsule.Shape = playerColShape;
        }

        if (Input.IsActionJustPressed("MoveForward"))
        {
            direction -= Transform.basis.z;
        }
        else if (Input.IsActionJustPressed("MoveBackward"))
        {
            direction += Transform.basis.z;
        }
        if (Input.IsActionJustPressed("MoveLeft"))
        {
            direction -= Transform.basis.x;
        }
        else if (Input.IsActionJustPressed("MoveRight"))
        {
            direction += Transform.basis.x;
        }

        direction = direction.Normalized();

        velocity = velocity.LinearInterpolate(direction * speed, accelleration * delta);

        velocity = MoveAndSlide(velocity, Vector3.Up);
        MoveAndSlide(fall, Vector3.Up);
    }
}
