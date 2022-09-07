using UnityEngine;
using System.Collections;

public class MouseCtrl : MonoBehaviour
{
    private float f_MouseX, f_MouseY;
    [SerializeField]
    private float f_RotateSpeed;
    private bool b_CursorLock;

    private void Initial()
    {
        f_MouseX = 0f;
        f_MouseY = 0f;
        f_RotateSpeed = 20f;
        b_CursorLock = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void CursorLoad()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!b_CursorLock)
            {
                Cursor.lockState = CursorLockMode.Locked;
                b_CursorLock = true;

            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                b_CursorLock = false;
            }
        }


    }

    private void MouseInputRotate()
    {
        if (b_CursorLock)
        {
            float f_InputX = Input.GetAxis("Mouse X");
            float f_InputY = Input.GetAxis("Mouse Y");
            f_MouseX += f_InputX * f_RotateSpeed * Time.deltaTime;
            f_MouseY += f_InputY * f_RotateSpeed * Time.deltaTime;
            f_MouseY = Mathf.Clamp(f_MouseY, -60, 90);

            transform.parent.rotation = Quaternion.Euler(0, f_MouseX, 0);
            transform.rotation = Quaternion.Euler(-f_MouseY, f_MouseX, 0);
        }

    }

    public void Set_Y_Recoil(float recoil)
    {
        f_MouseY += recoil;
    }

    private void Start()
    {
        Initial();
    }

    // Update is called once per frame
    void Update()
    {
        MouseInputRotate();
        CursorLoad();
    }
}
