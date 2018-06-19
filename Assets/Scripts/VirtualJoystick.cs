using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image bgImage;
    private Image joystickImage;
    private Vector2 joystickInput;

    private void Start()
    {
        bgImage = GetComponent<Image>();
        joystickImage = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnDrag(PointerEventData pointer)
    {
        Vector2 position;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            bgImage.rectTransform, pointer.position, pointer.pressEventCamera, out position))
        {
            position.x = (position.x / bgImage.rectTransform.sizeDelta.x);
            position.y = (position.y / bgImage.rectTransform.sizeDelta.y);

            joystickInput = new Vector2(position.x * 2 - 1, position.y * 2 - 1);
            // helps limit values to the extent of the circle
            joystickInput = (joystickInput.magnitude > 1) ? joystickInput.normalized : joystickInput;

            // moving the joystick image
            joystickImage.rectTransform.anchoredPosition = new Vector2(
                joystickInput.x * (bgImage.rectTransform.sizeDelta.x / 2.5f),
                joystickInput.y * (bgImage.rectTransform.sizeDelta.y / 2.5f));
        }
    }

    public virtual void OnPointerUp(PointerEventData pointer)
    {
        joystickInput = Vector2.zero;
        joystickImage.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnPointerDown(PointerEventData pointer)
    {
        OnDrag(pointer);
    }

    public float HorizontalMovement()
    {
        return joystickInput.x;
    }
}
