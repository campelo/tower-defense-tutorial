using UnityEngine;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    public static bool CanMoveCamera = true;
    private bool _isPointerOverGameObject = false;

    void Start()
    {
        _isPointerOverGameObject = false;
    }

    void Update()
    {
        if (!GameManager.IsMobile)
            return;
        if (Input.touchCount < 1)
            return;
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            Ray raycast = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit raycastHit;

            if (touch.phase == TouchPhase.Began)
            {
                _isPointerOverGameObject = EventSystem.current.IsPointerOverGameObject(touch.fingerId);
            }

            if (touch.phase == TouchPhase.Moved)
            {
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (_isPointerOverGameObject)
                    return;
                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.CompareTag(nameof(Node)))
                    {
                        raycastHit.transform.gameObject.SendMessage(nameof(Node.InteractWithNode));
                    }
                }
            }

            if (touch.phase == TouchPhase.Canceled)
            {
            }
        }
    }
}
