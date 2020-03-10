using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IEndDragHandler, IDragHandler//, IPointerDownHandler, IPointerUpHandler
{
    public Transform Stick; // 조이스틱
    public Vector2 JoyVec;  // 조이스틱의 방향

    private Vector2 StickFirstPos;
    private Vector2 currentPos;
    private float Radius;           // 조이스틱 배경의 가로 길이의 반

    void Start()
    {
        if (Stick == null)
            Stick = transform.GetChild(0);

        JoyVec = Vector2.zero;

        // 포지션 초기화
        StickFirstPos = currentPos = Stick.transform.position;

        Radius = GetComponent<RectTransform>().sizeDelta.x * 0.45f;

        // 캔버스 크기에대한 반지름 조절.
        float Can = transform.parent.GetComponent<RectTransform>().localScale.x;
        Radius *= Can;
    }
    
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        // < 드래그 중 >
        currentPos.x = Input.mousePosition.x;
        
        JoyVec = (currentPos - StickFirstPos).normalized;
        
        float Dis = Mathf.Abs(currentPos.x - StickFirstPos.x);

        if (Dis > Radius)
            currentPos.x = StickFirstPos.x + JoyVec.x * Radius;
        Stick.transform.position = currentPos;
    }
    
    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        // < 드래그 끝 >
        Stick.transform.position = StickFirstPos;
        JoyVec = Vector2.zero;
    }

    /*void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        // < 터치 시작 >
        currentPos.x = Input.mousePosition.x;
        JoyVec = (currentPos - StickFirstPos).normalized;
        Stick.transform.position = currentPos;
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        // < 터치 뗌 >
        Stick.transform.position = StickFirstPos;
        JoyVec = Vector2.zero;
    }*/
}
