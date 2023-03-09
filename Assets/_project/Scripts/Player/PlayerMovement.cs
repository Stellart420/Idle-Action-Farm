using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : PlayerComponent
{
    [SerializeField] private float _moveSpeed = 5f; 

    private Joystick _joystick;
    private Vector2 _joystickDirection; 
    private Rigidbody _rigidbody; 
    private Transform _mainCameraTransform; 

    public bool IsMoving { get; private set; }
    public override void Init(PlayerController controller)
    {
        base.Init(controller);
        _rigidbody = GetComponent<Rigidbody>();
        _joystick = GUIController.Instance.Joystick;
        _mainCameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        // Получаем направление движения, задаваемое джойстиком
        _joystickDirection = new Vector2(_joystick.Horizontal, _joystick.Vertical);

        // Если направление движения нулевое, то персонаж останавливается
        if (_joystickDirection == Vector2.zero)
        {
            Controller.Animation.SetIsWalking(false);
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            IsMoving = false;
        }
        else
        {
            // Иначе запускается анимация движения и персонаж перемещается с заданной скоростью
            Controller.Animation.SetIsWalking(true);
            Move();
            Rotate();
        }
    }

    private void Move()
    {
        // Получаем направление движения относительно камеры
        Vector3 direction = new Vector3(_joystickDirection.x, 0f, _joystickDirection.y);
        direction = Quaternion.Euler(0f, _mainCameraTransform.eulerAngles.y, 0f) * direction;

        // Персонаж перемещается с заданной скоростью
        _rigidbody.velocity = direction.normalized * _moveSpeed;
        IsMoving = true;
    }

    private void Rotate()
    {
        // Поворот персонажа в направлении движения
        if (_rigidbody.velocity != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }
    }
}
