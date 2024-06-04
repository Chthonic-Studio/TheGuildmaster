using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PanAndZoom : MonoBehaviour
{

    private CinemachineInputProvider inputProvider;
    private CinemachineVirtualCamera virtualCamera;
    private Transform cameraTransform;
    [SerializeField] private float panSpeed = 5f;
    [SerializeField] private float zoomSpeed = 3f;
    [SerializeField] private float zoomInMax = 4f;
    [SerializeField] private float zoomOutMax = 12f;

    private void Awake()
    {
        inputProvider = GetComponent<CinemachineInputProvider>();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = virtualCamera.VirtualCameraGameObject.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = inputProvider.GetAxisValue(0); 
        float y = inputProvider.GetAxisValue(1);
        float z = inputProvider.GetAxisValue(2);
        if (x != 0 || y != 0)
        {
            PanScreen(x, y);
        }

        ZoomScreen(z);
    }

    public void ZoomScreen (float increment)
    {
        // Adjust OrthographicSize based on zoom input
        virtualCamera.m_Lens.OrthographicSize -= increment * zoomSpeed * Time.deltaTime;

        // Clamp the OrthographicSize to be within a certain range
        virtualCamera.m_Lens.OrthographicSize = Mathf.Clamp(virtualCamera.m_Lens.OrthographicSize, zoomInMax, zoomOutMax);    
    }

    public Vector2 PanDirection(float x, float y)
    {
        Vector2 direction = Vector2.zero;

        if (y >= Screen.height * 0.95f)
        {
            direction.y += 1;
        }
        else if (y <= Screen.height * 0.05f)
        {
            direction.y = -1;
        }
        if (x >= Screen.width * 0.95f)
        {
            direction.x += 1;
        }
        else if (x <= Screen.width * 0.05f)
        {
            direction.x = -1;
        }

        return direction;
    }

    public void PanScreen(float x, float y)
    {
        Vector2 direction;

        // Get keyboard input
        Vector2 keyboardInput = GetKeyboardInput();

        // If there's keyboard input, use that. Otherwise, use mouse input.
        if (keyboardInput != Vector2.zero)
        {
            direction = keyboardInput;
        }
        else
        {
            direction = PanDirection(x, y);
        }

        // Move the camera
        cameraTransform.position = Vector3.Lerp
            (cameraTransform.position, cameraTransform.position + (Vector3)direction * panSpeed, Time.deltaTime);
    }
    public Vector2 GetKeyboardInput()
    {
        // Get input from WASD or arrow keys
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Normalize the input to ensure diagonal movement isn't faster
        input = input.normalized;

        return input;
    }
}
