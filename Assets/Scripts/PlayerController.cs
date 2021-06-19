using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;

    [Space]

    [SerializeField] float reloadTime;

    private int taps;
    private Touch lastTap;
    private float tapsDeltaTime;
    private float timer;

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

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                if (taps == 0) { StartCoroutine(CountDown(tapsDeltaTime)); }
                taps = taps + 1;
                lastTap = touch;
            }
        }
    }

    private IEnumerator<WaitForSeconds> CountDown(float time)
    {
        yield return new WaitForSeconds(time);

        tapsEnded();

        taps = 0;
    }

    private void tapsEnded()
    {
        if (taps == 1)
        {
            AddJump(lastTap.position);
        }
        else if (taps == 2)
        {
            gameObject.SetActive(false);
            Invoke("ReloadLevel", reloadTime);
        }
    }

    private void AddJump(Vector3 touchPos)
    {
        //Project a ray from the camera
        Ray ray = cam.ScreenPointToRay(touchPos);

        //Get the ray hit info
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);

        //Check the hit info
        if (hitInfo.collider.CompareTag("Player") && taps == 1)
        {
            //Should be in FixedUpdate() but one frame physics interactions don't show wierd behaviour in Update()
            hitInfo.collider.attachedRigidbody.AddForce(Vector3.up * jumpForce);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.right * moveSpeed * input);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
