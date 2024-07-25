using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ObjDrag : MonoBehaviour
{
    private Camera cam;
    private Vector3 dragOffset; // Ŭ��������, object�� �߾���ǥ�Ͱ� Ŭ���� ��ǥ������ ����
    Rigidbody2D rb;
    CapsuleCollider2D col;
    Compare compare;

    void Start()
    {
        //ī�޶� �̷��� ���� �������ִ� ���� = ã�Ƴ��� ��� ���� ����(�ڵ尡 ��������)
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
        compare = GameObject.Find("ScaleController").GetComponent<Compare>();
        
    }


    void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();


        rb.bodyType = RigidbodyType2D.Static;
        col.enabled = false;
    }

    void OnMouseDrag()
    {
        transform.position = GetMousePos() + dragOffset;
    }

    private void OnMouseUp()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        col.enabled = true;
        StartCoroutine(ExecuteAfterTime(0.5f));
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        // ������ �ð� ���� ���
        yield return new WaitForSeconds(time);

        // �ð� ��� �� ������ �Լ� ȣ��
        compare.CompareWeight();
    }

    Vector3 GetMousePos()
    {
        var mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        // ���콺�� ��ġ�� ��������
        mousePos.z = 0;
        return mousePos; // ���콺 ��ġ�� ��ȯ 
    }
}
