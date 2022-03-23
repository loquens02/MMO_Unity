using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"OnCollision : {collision.gameObject.name}");
    }
    private void OnTriggerEnter(Collider other)
    {
        
        Debug.Log($"OnTrigger : {other.gameObject.name}");
    }


    void Start()
    {
        
    }

    void Update()
    {
        // mouse click 
        if (Input.GetMouseButtonDown(0)){
            // ī�޶󿡼� ��� ����ü ���� Ư�� ��ü���� ���⺤��
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ī�޶� ���� 1�� ����
            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            LayerMask mask = LayerMask.GetMask("Monster") | LayerMask.GetMask("Wall");
            // Layer bit mask
            //int mask = (1 << 8) | (1 << 9);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f, mask))
            {
                //Debug.Log($"Raycast Camera : {hit.collider.gameObject.name}");
                //Debug.Log($"Raycast Camera tag : {hit.collider.gameObject.tag}");
            }
        }
    }
}

/*
 ** // ��ǥ�� 4��- Local , World, Viewport, Screen(ȭ��)
    //Debug.Log(Input.mousePosition); // Screen
    //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));
 
 ** Raycast �ϳ��� ���� ��
    if (Physics.Raycast(transform.position + Vector3.up, look, out hit, 10))
   
 ** Raycast All
    // from local space to world space
    // ������ ����� ��ȯ. ���� �ٶ󺸴� ������ ���迡���� ��������. '��'�� ���谡 �����ϵ���.
    // => 'ĳ����'�� ���� ���� �������� ���� ���� �ϵ���. 
    Vector3 look = transform.TransformDirection(Vector3.forward); 
    Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

    RaycastHit[] hits;
    hits= Physics.RaycastAll(transform.position + Vector3.up, look, 10);

    foreach(RaycastHit hit in hits)
    {
        Debug.Log($"Raycast !!{hit.collider.gameObject.name}");
    }
         
** ī�޶󿡼� ��� ����ü ���� Ư�� ��ü���� ���⺤��
    // z: ī�޶� ~ ����ü ���� ��� ������ �Ÿ�
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

    // [mouse position ~ Camera] unit Vector
    Vector3 dir = mousePos - Camera.main.transform.position;
    dir = dir.normalized;

**
    Debug.DrawRay(Camera.main.transform.position, dir, Color.red, 1.0f); // ī�޶� ���� 1�� ����. dir ũ�� 1

    RaycastHit hit;
    if (Physics.Raycast(Camera.main.transform.position, dir, out hit)) // �� ������ ���� �̵��Ÿ� ������
    {
        Debug.Log($"Raycast Camera : {hit.collider.gameObject.name}");
    }
 
 */