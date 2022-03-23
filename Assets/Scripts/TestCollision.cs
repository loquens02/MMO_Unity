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
            // 카메라에서 쏘는 절두체 내에 특정 객체로의 방향벡터
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 카메라 광선 1초 유지
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
 ** // 좌표계 4개- Local , World, Viewport, Screen(화면)
    //Debug.Log(Input.mousePosition); // Screen
    //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));
 
 ** Raycast 하나만 잡을 때
    if (Physics.Raycast(transform.position + Vector3.up, look, out hit, 10))
   
 ** Raycast All
    // from local space to world space
    // 로컬을 월드로 변환. 내가 바라보는 방향을 세계에서도 먹히도록. '나'를 세계가 인지하도록.
    // => '캐릭터'가 보는 방향 기준으로 방향 설정 하도록. 
    Vector3 look = transform.TransformDirection(Vector3.forward); 
    Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

    RaycastHit[] hits;
    hits= Physics.RaycastAll(transform.position + Vector3.up, look, 10);

    foreach(RaycastHit hit in hits)
    {
        Debug.Log($"Raycast !!{hit.collider.gameObject.name}");
    }
         
** 카메라에서 쏘는 절두체 내에 특정 객체로의 방향벡터
    // z: 카메라 ~ 절두체 내부 어딘가 까지의 거리
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

    // [mouse position ~ Camera] unit Vector
    Vector3 dir = mousePos - Camera.main.transform.position;
    dir = dir.normalized;

**
    Debug.DrawRay(Camera.main.transform.position, dir, Color.red, 1.0f); // 카메라 광선 1초 유지. dir 크기 1

    RaycastHit hit;
    if (Physics.Raycast(Camera.main.transform.position, dir, out hit)) // 값 없으면 광선 이동거리 무제한
    {
        Debug.Log($"Raycast Camera : {hit.collider.gameObject.name}");
    }
 
 */