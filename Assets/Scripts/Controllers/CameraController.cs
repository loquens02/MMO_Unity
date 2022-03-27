using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuaterView;
    
    [SerializeField]
    Vector3 _delta= new Vector3(0, 4.0f, -7.0f); // Camera ~ Player 거리
    
    [SerializeField]
    GameObject _player;

    void Start()
    {
        
    }
    
    //void Update()
    // Player 이동 ~ Camera 이동 순서 안 맞아서 부들부들 떨리는 문제 해결
    void LateUpdate()  
    {

        if(_mode == Define.CameraMode.QuaterView)
        {
            RaycastHit hit;
            // player 가 camera 에 쏘는 광선이 장애물에 닿을 경우, player ~ 장애물 거리의 80% 위치에 camera 이동
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall"))) 
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                Debug.DrawRay(_player.transform.position, hit.point, Color.blue);

                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else // camera ~ player 사이에 장애물이 없을 때는, 따라가기만
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }

            
        }
    }

    // 만약 나중에 QuaterView setting 하고 싶다면
    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
}
