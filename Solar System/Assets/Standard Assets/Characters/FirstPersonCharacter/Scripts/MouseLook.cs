using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MouseLook : MonoBehaviour
{
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;
    public bool lockCursor = true;

    private Quaternion m_CameraTargetRot;
    private bool m_cursorIsLocked = true;
    private new Transform camera;
    private bool fast = false;
    private Vector3 topDown = Vector3.zero;
    private GameObject[] bodies;
    private int nextBody = 0;
    private bool startMoving = false;

    private void Start() {
        camera = this.transform;
        Time.timeScale = 1f;
        topDown = camera.position;
        
        bodies = GameObject.FindGameObjectsWithTag("body");

        m_CameraTargetRot = camera.localRotation;
    }


    private void Update() {
        if (Input.GetKeyDown(KeyCode.S))
            ChangeTimeScale();

        if (Input.GetKeyDown(KeyCode.Tab)) {
            ChangeBody();
        }

        if (startMoving) {
            UpdatePos();
        }

        float yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
        float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;

        m_CameraTargetRot *= Quaternion.Euler (-xRot, yRot, 0f);

        if(smooth)
        {
            camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                smoothTime * Time.deltaTime);
        }
        else
        {
            camera.localRotation = m_CameraTargetRot;
        }

        UpdateCursorLock();
    }

    private void ChangeTimeScale() {
        if (fast) {
            Time.timeScale = 1f;
        } else {
            Time.timeScale = 10f;
        }
        fast = !fast;
    }

    private void ChangeBody() {
        if (nextBody >= bodies.Length) {
            camera.position = topDown;
            startMoving = false;
            nextBody = 0;
        } else {
            if (nextBody == 0) {
                startMoving = true;
            }
            
            nextBody++;
        }
    }

    private void UpdatePos() {
        int current = nextBody - 1;
        Vector3 bodyPos = bodies[current].transform.position;
        this.transform.position = new Vector3(bodyPos.x, bodyPos.y + 100, bodyPos.z);
    }

    public void SetCursorLock(bool value)
    {
        lockCursor = value;
        if(!lockCursor)
        {//we force unlock the cursor if the user disable the cursor locking helper
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void UpdateCursorLock()
    {
        //if the user set "lockCursor" we check & properly lock the cursos
        if (lockCursor)
            InternalLockUpdate();
    }

    private void InternalLockUpdate()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            m_cursorIsLocked = false;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (!m_cursorIsLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

