using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float Sens = 20f;

    private PlayerMotor motor;

        void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

        void Update()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveZ = Input.GetAxisRaw("Vertical");

        Vector3 MoveHorizontal = transform.right * MoveX; // (1,0,0)
        Vector3 MoveVertical = transform.forward * MoveZ; // (0,0,1)

        Vector3 _Velocity = (MoveHorizontal + MoveVertical).normalized * speed;

        motor.Move(_Velocity);



        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, yRot, 0f) * Sens;

        motor.Rotate(_rotation);

        float xRot = Input.GetAxisRaw("Mouse Y");

        Vector3 _camrotation = new Vector3(xRot, 0f, 0f) * Sens;

        motor.CamRotate(_camrotation);
    }

}
