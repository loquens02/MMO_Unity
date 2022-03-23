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
            transform.position = _player.transform.position + _delta;
            transform.LookAt(_player.transform);
        }
    }

    // ���� ���߿� QuaterView setting �ϰ� �ʹٸ�
    public void SetQuaterView(Vector3 delta)
    {
        _mode = Define.CameraMode.QuaterView;
        _delta = delta;
    }
}
