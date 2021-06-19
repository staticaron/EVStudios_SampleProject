using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    private Rigidbody rb;
    private int input;

    private Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    private void Update()
    {
        input = InputManager.instance.input;

        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = touch.position;

            //Project a ray from the camera
            Ray ray = cam.ScreenPointToRay(touchPos);

            //Get the ray hit info
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);

            //Check the hit info
            if (hitInfo.collider.CompareTag("Player"))
            {
                //Should be in FixedUpdate() but one frame physics interactions don't show wierd behaviour in Update()
                hitInfo.collider.attachedRigidbody.AddForce(Vector3.up * jumpForce);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.right * moveSpeed * input);
    }


}
