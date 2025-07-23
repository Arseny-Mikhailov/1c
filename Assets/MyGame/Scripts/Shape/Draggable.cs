using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Zenject;

[RequireComponent(typeof(Rigidbody2D))]
public class Draggable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector3 _startPos;
    private Vector3 _offset;
    
    private Rigidbody2D _rb;
    
    [Inject] private readonly Camera _cam;
    
    private bool _isDragging;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _startPos = transform.position;
        _offset = transform.position - _cam.ScreenToWorldPoint(eventData.position);
        _isDragging = true;
        _rb.simulated = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_isDragging) return;
        Vector3 newPos = _cam.ScreenToWorldPoint(eventData.position) + _offset;
        newPos.z = 0;
        transform.position = newPos;
    }

    public void OnPointerUp(PointerEventData eventData) 
    {
        _isDragging = false;
        _rb.simulated = true;
        
        // Здесь будет проверка попадания в слот
        // Если попадает, то запускаем анимацию перемещения в слот
        // Если не попадает, то возвращаемся к начальному положению
        transform.DOMove(_startPos, 0.5f).SetEase(Ease.OutQuad);
    }
}

public class Shape : Draggable
{
    
}