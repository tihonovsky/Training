using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class JoyStickHeandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _joystickBackGround;
    [SerializeField] private Image _joystickHandle;
    [SerializeField] private Image _joystickArea;
    
    public Color _active;
    public Color _isActiveHandle;
    public Color _isActiveBackGround;
    
    private Vector2 _joystickBackGroundStartPosition;
    private bool _joystickIsActive = false;
     
    protected Vector2 _inputVector;
    
    private void Start()
    {
        ClickEffect();
       
        _joystickBackGroundStartPosition = _joystickBackGround.rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 joystickPosition;
        
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackGround.rectTransform, eventData.position, eventData.pressEventCamera, out joystickPosition))
        {
            _inputVector.x = (joystickPosition.x * 2 / _joystickBackGround.rectTransform.sizeDelta.x);
            _inputVector.y = (joystickPosition.y * 2 / _joystickBackGround.rectTransform.sizeDelta.y);
            
            _inputVector = new Vector2(joystickPosition.x, joystickPosition.y);
            
            _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;
            
            _joystickHandle.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystickBackGround.rectTransform.sizeDelta.x / 2), _inputVector.y * (_joystickBackGround.rectTransform.sizeDelta.y / 2));
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        ClickEffect();

        Vector2 joystickBackGroundPosition;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform, eventData.position, eventData.pressEventCamera, out joystickBackGroundPosition))
        {
            _joystickBackGround.rectTransform.anchoredPosition = new Vector2(joystickBackGroundPosition.x, joystickBackGroundPosition.y);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickBackGround.rectTransform.anchoredPosition = _joystickBackGroundStartPosition;
        ClickEffect();
        _inputVector = Vector2.zero;
        _joystickHandle.rectTransform.anchoredPosition = Vector2.zero;
    }
    private void ClickEffect()
    {
        if (!_joystickIsActive)
        {
            _joystickBackGround.color = _active;
            _joystickHandle.color = _active;
            _joystickIsActive = true;
        }

        else
        {
            _joystickBackGround.color = _isActiveBackGround;
            _joystickHandle.color = _isActiveHandle;
            _joystickIsActive = false;
        }
    }
}
