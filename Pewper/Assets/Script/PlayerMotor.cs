using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{

    [SerializeField]
    private Camera Freecam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float camrotationX = 0f;
    private float currentCamX = 0f;
    private Vector3 jetpackForce = Vector3.zero;

    [SerializeField]
    private float camLock = 90f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void Move (Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void CamRotate(float _camrotationX)
    {
        camrotationX = _camrotationX;
    }

    public void ApplyJetForce (Vector3 _jetpackForce)
    {
        jetpackForce = _jetpackForce;
    }


    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
        
        if(jetpackForce != Vector3.zero)
        {
            rb.AddForce(jetpackForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if (Freecam!=null)
        {
            currentCamX -= camrotationX;
            currentCamX = Mathf.Clamp(currentCamX, -camLock, camLock);

            Freecam.transform.localEulerAngles = new Vector3(currentCamX, 0f, 0f);
        }
    }



}
