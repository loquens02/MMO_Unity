using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.QuaterView;
    
    [SerializeField]
    Vector3 _delta= new Vector3(0, 4.0f, -7.0f); // Camera ~ Player �Ÿ�
    
    [SerializeField]
    GameObject _player;

    void Start()
    {
        
    }
    
    //void Update()
    // Player �̵� ~ Camera �̵� ���� �� �¾Ƽ� �ε�ε� ������ ���� �ذ�
    void LateUpdate()  
    {

        if(_mode == Define.CameraMode.QuaterView)
        {
            RaycastHit hit;
            // player �� camera �� ��� ������ ��ֹ��� ���� ���, player ~ ��ֹ� �Ÿ��� 80% ��ġ�� camera �̵�
            if (Physics.Raycast(_player.transform.position, _delta, out hit, _delta.magnitude, LayerMask.GetMask("Wall"))) 
            {
                float dist = (hit.point - _player.transform.position).magnitude * 0.8f;
                Debug.DrawRay(_player.transform.position, hit.point, Color.blue);

                transform.position = _player.transform.position + _delta.normalized * dist;
            }
            else // camera ~ player ���̿� ��ֹ��� ���� ����, ���󰡱⸸
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }

            
        }
    }

    // ���� ���߿� QuaterView setting �ϰ� �ʹٸ�
    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
}
