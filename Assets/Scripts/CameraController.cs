using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    private bool _doMovement;
    private Camera _camera;
    // perto
    private float _zoomMinBound = 20f;
    // longe
    private float _zoomMaxBound = 60f;


    public float PanSpeed = 30f;
    public float PanBorderThickness = 10f;
    public float ScrollSpeed = 5f;
    // zoom in (near)
    private float _minY = 20f;
    // zoom out (far)
    private float _maxY = 80f;
    // top screen
    private float _maxZ = -25f;
    // botton screen
    private float _minZ = -95f;
    // left screen
    private float _minX = -5f;
    // right screen
    private float _maxX = 72f;

    [Header("Touch devices")]
    public float TouchZoomSpeed = .001f;
    public float TouchMoveSpeed = .001f;

    // Start is called before the first frame update
    void Start()
    {
        _doMovement = false;
        _camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (GameManager.IsMobile)
            MovingCameraTouchDevices();
        else
            MovingCameraKeyboard();
    }

    private void MovingCameraTouchDevices()
    {
        if (Input.touchCount < 1)
            return;
        //if (!TouchManager.CanMoveCamera)
        //    return;
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Moved)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                    return;
                Vector3 pos = _camera.transform.position;
                pos -= new Vector3(touch.deltaPosition.x * TouchMoveSpeed, 0f, touch.deltaPosition.y * TouchMoveSpeed);
                pos.x = Mathf.Clamp(pos.x, _minX, _maxX);
                pos.z = Mathf.Clamp(pos.z, _minZ, _maxZ);
                _camera.transform.position = pos;
            }
        }
        if (Input.touchCount == 2)
        {
            Touch tZero = Input.GetTouch(0);
            Touch tOne = Input.GetTouch(1);

            Vector2 tZeroPrevious = tZero.position - tZero.deltaPosition;
            Vector2 tOnePrevious = tOne.position - tOne.deltaPosition;

            float oldTouchDistance = Vector2.Distance(tZeroPrevious, tOnePrevious);
            float currentTouchDistance = Vector2.Distance(tZero.position, tOne.position);

            float deltaDistance = oldTouchDistance - currentTouchDistance;
            Zoom(deltaDistance, TouchZoomSpeed);
        }
    }

    private void Zoom(float deltaMagnitudeDiff, float speed)
    {
        //MoveCamera(deltaMagnitudeDiff * speed * -1);
        _camera.fieldOfView += deltaMagnitudeDiff * speed;
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView, _zoomMinBound, _zoomMaxBound);
    }

    private void MovingCameraKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _doMovement = !_doMovement;

        if (!_doMovement)
            return;

        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - PanBorderThickness)
            transform.Translate(Vector3.forward * PanSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= PanBorderThickness)
            transform.Translate(Vector3.back * PanSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - PanBorderThickness)
            transform.Translate(Vector3.right * PanSpeed * Time.deltaTime, Space.World);
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= PanBorderThickness)
            transform.Translate(Vector3.left * PanSpeed * Time.deltaTime, Space.World);

        if (transform.position.x < _minX)
            transform.position = new Vector3(_minX, transform.position.y, transform.position.z);
        if (transform.position.x > _maxX)
            transform.position = new Vector3(_maxX, transform.position.y, transform.position.z);
        if (transform.position.z < _minZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, _minZ);
        if (transform.position.z > _maxZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, _maxZ);

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        MoveCamera(scroll * 1000 * ScrollSpeed * Time.deltaTime);
    }

    private void MoveCamera(float value)
    {

        Vector3 pos = transform.position;
        pos.y -= value;
        pos.y = Mathf.Clamp(pos.y, _minY, _maxY);
        transform.position = pos;
    }
}
