using Godot;
using System;

public class PlayerBody : KinematicBody
{
    //The current speed of the player
    float speed;
    //How fast the player moves when walking 
    float walkSpeed = 7;
    //How fast the player moves when crouched
    float crouchSpeed = 3;
    //How fast the player is moving when sprinting
    float sprintSpeed = 14;
    //How quickly the player goes into a crouched stance
    float crouchingSpeed = 5;
    //How quickly the pplayer acellerates
    float accelleration = 20;
    //How har the gravity plls dow the player
    float gravity = 9.8f;
    //The force of the jump
    float jump = 5;
    //How hight the plaayer characters camera displayes when standing
    float defualtHeight = 1.5f;
    //How high the players character camera is when it is crouvhed
    float crouchHeight = .5f;
    //How sensitive the movement of the mouse is
    float mouseSensitivity = 0.05f;


    Vector3 direction;
    Vector3 velocity;
    Vector3 fall;
    //The spatiol object repereseting the head of the player
    Spatial head;
    CollisionShape playerCollShape;
    RayCast headBump;

    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Input.SetMouseMode(Input.MouseMode.Captured);
        head = GetNode<Spatial>("Head");
        playerCollShape = GetNode<CollisionShape>("CollisionShape");
        headBump = GetNode<RayCast>("HeadBump");
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
        //Set the players speed to the walk speed by defualt every frame, this works with sprinting and crouch speed
        speed = walkSpeed;
        //Bool te check if the head if the player is making contact with a ceiling surface
        bool headBumped = false;
        //Reset the direction to 0 every frame start to not kepp building speed to light speed
        direction = Vector3.Zero;
        //Check if the head ray collider is active
        if (headBump.IsColliding()) headBumped = true;
        //if we are touchin the floor then we cancel the gravity on the playe
        if (!IsOnFloor()) fall.y -= gravity * delta;
        //If we press jump we jump!
        if (Input.IsActionJustPressed("Jump") && IsOnFloor()) fall.y = jump;
        //If the sprint key is pressed we set the default speed to the new sprint speed
        if (Input.IsActionPressed("Sprint")) speed = sprintSpeed;
        //If the player hits a ceiling he does not "stick" to it for a second then fall, it makes you fall emediatly
        if (headBumped) fall.y = -2;

        if (Input.IsActionJustPressed("ui_cancel")) Input.SetMouseMode(Input.MouseMode.Visible);

        //If the player is crouching and the collision shape is a capsule then 
        if (Input.IsActionPressed("Crouch") && playerCollShape.Shape is CapsuleShape capShape)
        {
            capShape.Height -= crouchingSpeed * delta;
            speed = crouchSpeed;
        }
        else if (!headBumped)
        {
            ((CapsuleShape)playerCollShape.Shape).Height += crouchingSpeed * delta;
        }
        //Clamp the max and min height for crouching when it is being modified
        ((CapsuleShape)playerCollShape.Shape).Height = Mathf.Clamp(((CapsuleShape)playerCollShape.Shape).Height, crouchHeight, defualtHeight);
        //If the move forward is pressed them we subtract from the transform basis z of the player body 
        if (Input.IsActionPressed("MoveForward")) direction -= Transform.basis.z;
        else if (Input.IsActionPressed("MoveBackward")) direction += Transform.basis.z;
        //If the move StrafeLeft is pressed them we subtract from the transform basis x of the player body 
        if (Input.IsActionPressed("StrafeLeft")) direction -= Transform.basis.x;
        else if (Input.IsActionPressed("StrafeRight")) direction += Transform.basis.x;
        //We normalize the direction as not to have faster movement when traveling diagonaly
        direction = direction.Normalized();
        //We set the velocity to linearly interpolate to give us smooth acceleration and decceleration
        velocity = velocity.LinearInterpolate(direction * speed, accelleration * delta);
        //Set the value from the move and slide back to the velocity vlue for the horizontal movement of the body
        velocity = MoveAndSlide(velocity, Vector3.Up);
        //Move and slide the body to the value of the jump perameter
        MoveAndSlide(fall, Vector3.Up);
    }
}
