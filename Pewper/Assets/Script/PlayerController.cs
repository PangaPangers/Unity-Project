using UnityEngine;
[RequireComponent(typeof(ConfigurableJoint))]
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float Sens = 20f;

    [SerializeField]
    private float jetpackForce = 1000f;

    [Header("Joint Options: ")]
    [SerializeField]
    private JointDriveMode jointMode = JointDriveMode.Position;
    [SerializeField]
    private float jointSpring = 20f;
    [SerializeField]
    private float jointMaxForce = 50f;


    private PlayerMotor motor;
    private ConfigurableJoint joint;

        void Start()
    {
        motor = GetComponent<PlayerMotor>();
        joint = GetComponent<ConfigurableJoint>();

        SetJointSettings(jointSpring);
    }

        void Update()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveZ = Input.GetAxisRaw("Vertical");

        Vector3 MoveHorizontal = transform.right * MoveX; // (1,0,0)
        Vector3 MoveVertical = transform.forward * MoveZ; // (0,0,1)

        Vector3 _Velocity = (MoveHorizontal + MoveVertical).normalized * speed;

        // Apply Movement
        motor.Move(_Velocity);


        float yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, yRot, 0f) * Sens;

        // Apply Rotation
        motor.Rotate(_rotation);

        float xRot = Input.GetAxisRaw("Mouse Y");

        float _camrotationX = xRot * Sens;

        motor.CamRotate(_camrotationX);

        Vector3 _jetpackForce = Vector3.zero;

        
        if (Input.GetButton("Jump"))
        {
            _jetpackForce = Vector3.up * jetpackForce;
            SetJointSettings(0f);
        }
        else
        {
            SetJointSettings(jointSpring);
        }

        // Apply JetpackForce
        motor.ApplyJetForce(_jetpackForce);

    }

    private void SetJointSettings(float _jointSpring)
    {
        joint.yDrive = new JointDrive
        {
            mode = jointMode,
            positionSpring = _jointSpring,
            maximumForce = jointMaxForce
        };
    }

}
