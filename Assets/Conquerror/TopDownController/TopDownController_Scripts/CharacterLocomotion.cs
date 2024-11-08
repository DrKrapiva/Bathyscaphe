using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    [Tooltip("Assign animator if you would like. We are using 2d blendtree")]
    [SerializeField] Animator animator;
    //this script will make you character move correctly regardless of how camera is setup
    [Tooltip("Character controller is a built in component in unity. Feel free to use rigidbody or changing transform directly")]
    [SerializeField] CharacterController characterController;
    [Tooltip("how fast the player walks")]
    private float walkSpeed = 3f;
    private float rotationSpeed = 10f; // Скорость поворота
    [Tooltip("if you would like separate visual from player assign something else here")]
    [SerializeField] Transform characterVisual;//if you would like separate visual from player assign something else here
    [Tooltip("Turn this off if you want to separate movement and aiming")]
    [SerializeField] bool lookToMovementDirection = true;//turn this off if you want to separate movement and aiming
    [Tooltip("Feel free to assign other joysticks here")]
    [SerializeField] FixedJoystick moveJoystick;//assign joystick here
    [Tooltip("Self explanatory. After this magnitude player will move ")]
    [SerializeField] float movementThreshold = 0;//0.1f;// self explanatory. After this magnitude player will move 
    [Header("Animation variables")]
    [Tooltip("This will turn rotation towards the joystick direction")]
    [SerializeField] bool canStrafe = false;
    [Tooltip("Animation variables for blendtrees")]
    [SerializeField] string forwardAnimationVar = "Forward";
    [Tooltip("Animation variables for blendtrees")]
    [SerializeField] string strafeAnimationVar = "Strafe";
    float mag; // maginutde
    Transform camTransform;
    Vector3 fwd,right; //camera fwd,right
    Vector3 input,move;//input for animations
    Vector3 cameraForward;
    float forward,strafe;//we will use them in animation variables
    private float originalWalkSpeed; // Поле для хранения исходной скорости

    // Переменные для хранения предыдущих значений джойстика
    private float previousJoystickHorizontal = 0f;
    private float previousJoystickVertical = 0f;

    public static CharacterLocomotion Instance
    {
        get
        {
            return _instance;
        }
    }
    void Awake()
    {
        _instance = this;

        if (characterController == null){
            characterController = GetComponent<CharacterController>();
            //getting the characterController component
        }
        if(characterVisual == null){
            characterVisual = transform;
        }
        camTransform = Camera.main.transform;

        
    }
    static private CharacterLocomotion _instance;
    void Start(){

        walkSpeed = DictionaryHero.Instance.Speed(PlayerPrefs.GetString("NowHero"));
        originalWalkSpeed = walkSpeed; // Сохраняем исходную скорость
        //Debug.Log(walkSpeed + " walkSpeed");

        characterController.detectCollisions = false; //we don't want character controller to detect collisions
        RecalculateCamera(Camera.main);//we should know where camera is looking at. Call this method each time camera angle changes
        //also consider caching the camera
    }
    void Update(){

        // Текущие значения джойстика
        float joystickHorizontal = moveJoystick.Horizontal;
        float joystickVertical = moveJoystick.Vertical;

        // Проверка на изменение значений джойстика
        if (joystickHorizontal != previousJoystickHorizontal || joystickVertical != previousJoystickVertical)
        {
            //Debug.Log($"[CharacterLocomotion] Joystick Horizontal: {joystickHorizontal}, Joystick Vertical: {joystickVertical}");

            // Обновляем предыдущие значения
            previousJoystickHorizontal = joystickHorizontal;
            previousJoystickVertical = joystickVertical;
        }


        //Движение при помощи WASD
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0)
        {
            Vector3 movement = new Vector3(horizontal, 0, vertical).normalized;
            characterController.Move(movement * walkSpeed * Time.deltaTime);

            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        //


        //mag = Mathf.Clamp01(new Vector2(moveJoystick.Horizontal, moveJoystick.Vertical).sqrMagnitude);
        // Используем точное значение величины ввода джойстика
        mag = new Vector2(joystickHorizontal, joystickVertical).magnitude;

        if (canStrafe){
            lookToMovementDirection = false;
            //I turn it off because player needs to strafe to it's forward.
            //use strafe when you look at certain object(target) for instance
        }
        //getting the magnitude
        if (mag >= movementThreshold) 
        {
            MovementAndRotation();
        }
        else{
            characterController.Move(new Vector3(0,-9.8f,0));//gravity when idle
        }
        if(animator != null){
            if(canStrafe){
                RelativeAnimations();
            }
            else{
                animator.SetFloat(forwardAnimationVar,mag);
            }
        }
    }
   
    void RelativeAnimations(){
        if (camTransform != null)
        {
            cameraForward = Vector3.Scale(camTransform.up, new Vector3(1, 0, 1)).normalized; //camera forwad
            move = moveJoystick.Vertical * cameraForward + moveJoystick.Horizontal * camTransform.right;//relative 
            //vector to camera forward and right
        }
        else
        {
            move = moveJoystick.Vertical * Vector3.forward + moveJoystick.Horizontal * Vector3.right;
            //if there is no camera transform(for any reason then we use joystick forward and right)
        }
        if (move.magnitude > 1)
        {
            move.Normalize();//normalizing here
        }
        MoveAnims(move);
    }
    void MoveAnims(Vector3 move){
        this.input = move;
        Vector3 localMove = transform.InverseTransformDirection(input);//inversing local move from the input
        strafe = localMove.x;//x is right input relative to camera 
        forward = localMove.z;//z is forward joystick input relative to camera
        animator.SetFloat(forwardAnimationVar, forward*2f, 0.01f, Time.deltaTime);//setting animator floats
        animator.SetFloat(strafeAnimationVar, strafe*2f, 0.01f, Time.deltaTime);
    }
    void RecalculateCamera(Camera _cam){
            Camera cam = _cam;
            camTransform = cam.transform;
            fwd = cam.transform.forward; //camera forward
            fwd.y = 0;
            fwd = Vector3.Normalize(fwd);
            right = Quaternion.Euler(new Vector3(0, 90, 0)) * fwd; //camera right
    }
    void MovementAndRotation(){

        // Расчёт направления на основе отклонения джойстика
        Vector3 direction = new Vector3(moveJoystick.Horizontal, 0, moveJoystick.Vertical);//joystick direction
       // Vector3 rightMovement = right * walkSpeed * Time.deltaTime * moveJoystick.Horizontal;//getting right movement out of joystick(relative to camera)
        
       // Vector3 upMovement = fwd * walkSpeed * Time.deltaTime * moveJoystick.Vertical; //getting up movement out of joystick(relative to camera)
        Vector3 rightMovement = right * walkSpeed * mag * Time.deltaTime * moveJoystick.Horizontal;
        Vector3 upMovement = fwd * walkSpeed * mag * Time.deltaTime * moveJoystick.Vertical;
    
        Vector3 heading = Vector3.Normalize(rightMovement + upMovement); //final movement vector

        // Применение гравитации и движения с учётом скорости
        heading.y = -9.8f;//gravity while moving
        //characterController.Move(heading * walkSpeed*Time.deltaTime);//move
        characterController.Move(heading * walkSpeed * mag * Time.deltaTime);

        if (lookToMovementDirection)
        {
            characterVisual.forward = new Vector3(heading.x,characterVisual.forward.y,heading.z);
            //look to movement direction
            //gameObject.transform.LookAt(direction);
            Quaternion rotation = Quaternion.LookRotation(direction);

            // Применяем этот кватернион для мгновенного поворота объекта
            //transform.rotation = rotation;
            //Применяем этот кватернион для постепенного поворота объекта
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        Debug.Log($"[CharacterLocomotion] Current Speed: {walkSpeed * mag}, Joystick Magnitude: {mag}");
    }
    public void StartCoroutineChangeWalkSpeed(float persent, float time)
    {
        Debug.Log("walkSpeed" + walkSpeed);
        StartCoroutine(ChangeWalkSpeed(persent, time));
    }
    public void StopCoroutineChangeWalkSpeed()
    {
        StopCoroutine("ChangeWalkSpeed");
        walkSpeed = originalWalkSpeed; // Восстанавливаем исходную скорость
        Debug.Log("walkSpeed" + walkSpeed);
    }
    IEnumerator ChangeWalkSpeed(float percent, float time)
    {
        walkSpeed = originalWalkSpeed * (1 + percent);
        //Debug.Log("walkSpeed" + walkSpeed);
        yield return new WaitForSeconds(time);
        walkSpeed = originalWalkSpeed; // Восстанавливаем исходную скорость
    }
}
