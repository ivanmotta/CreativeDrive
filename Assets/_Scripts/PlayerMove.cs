using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 50f;
    private Rigidbody rb;
    private Vector3 moveDir;

    private float horizInput;
    private float vertInput;

    public float minX = -60f;
    public float maxX = 60f;
    public float sensitivityX = 10f;
    public float sensitivityY = 25f;
    private float rotationY = 0f;
    private float rotationX = 0f;

    public Camera cam;

    private Quaternion lastRotation;
    private Quaternion lastCamRotation;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");

        rotationY += Input.GetAxis("Mouse X") * sensitivityX;
        rotationX += Input.GetAxis("Mouse Y") * sensitivityY;
        rotationX = Mathf.Clamp(rotationX, minX, maxX);

        transform.eulerAngles = new Vector3(0, rotationY, 0);
        cam.transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);

        moveDir = (horizInput * cam.transform.right + vertInput * cam.transform.forward).normalized;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * walkSpeed * Time.deltaTime;
    }

    public void RecordPosition()
    {
        lastRotation = transform.rotation;
        lastCamRotation = cam.transform.rotation;
    }

    public void Recenter()
    {
        //transform.eulerAngles = new Vector3(0, rotationY, 0);
        //cam.transform.localEulerAngles = new Vector3(0, rotationY, 0);
        transform.rotation = lastRotation;
        cam.transform.rotation = lastCamRotation;
    }
}
