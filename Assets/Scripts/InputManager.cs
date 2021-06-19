using UnityEngine;
using UnityEngine.UI;

public enum KeyStatus { DOWN, UP };

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public int input = 0;   //current input contained by the input manager

    [SerializeField] KeyStatus leftStatus = KeyStatus.UP;
    [SerializeField] KeyStatus rightStatus = KeyStatus.UP;

    private void Start()
    {
        #region Maintain Single intance
        if (instance == null) instance = this;
        else Destroy(gameObject);
        #endregion
    }

    public void LeftDown()
    {
        input = -1;
        leftStatus = KeyStatus.DOWN;
    }

    public void LeftUp()
    {
        leftStatus = KeyStatus.UP;

        if (rightStatus == KeyStatus.UP && leftStatus == KeyStatus.UP)
        {
            input = 0;
        }
    }

    public void RightDown()
    {
        input = 1;
        rightStatus = KeyStatus.DOWN;
    }

    public void RightUp()
    {
        rightStatus = KeyStatus.UP;

        if (rightStatus == KeyStatus.UP && leftStatus == KeyStatus.UP)
        {
            input = 0;
        }
    }
}
