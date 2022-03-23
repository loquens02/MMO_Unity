using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] // code �� private �̰� Unity ���� �� �� �ִ� public ȿ��
    float _speed = 10.0f;
    bool _moveToDest = false;
    Vector3 _destPos;

    void Start()
    {
        // Managers(���հ���) �ȿ� IunputManager �ְ�, �� �ȿ� KeyAction �����ڰ� �ִ�.
        // ������ �Լ��� ���ڷ� ���� == ������û. key�� ������ OnKeyboard �����ش޶�.
        Managers.Input.KeyAction -= OnKeyboard; // �ڵ� �ٸ����� �ߺ���û �ص������
        Managers.Input.KeyAction += OnKeyboard;

        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

    }

   
    void Update()
    {
        //OnKeyboard(); ���� ���񽺿��� �˷��ְ� �ֱ⿡ �ʿ� ����. �̷��� �̵��� 2��� �ȴ�.
        if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;

        }
    }

    void OnKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f); //Slerp(current, aim, %)
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }

        _moveToDest = false; // Ű���� �̵��ÿ��� Raycast �̵� ����� ������� �ʰڴ�.
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if(evt != Define.MouseEvent.Click)
        {
            return; // click �ƴϸ� ����
        }

        Debug.Log("OnMouseClicked"); // ���뿡�� �� �� ����Ȯ��.

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // camera ~ click ���� ���� ����
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f); // ī�޶� ���� 1�� ����

        
        

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall"))) // Wall Layer �� ���� �ν�. ��ġ �̵� ���ؼ�.
        {
            _destPos = hit.point;   //Raycast ������ ����Ű�� ������ ��ǥ ����
            _moveToDest = true;     //�������� ���ڴ�.

            //Debug.Log($"Raycast Camera tag : {hit.collider.gameObject.tag}");
        }
    }
}
/*
** Update() - new Vector3(0.0f, 0.0f, 1.0f) ���� ���� ���� �ִ�
** transform.position+=transform.TransformDirection(Vector3.forward*Time.deltaTime*_speed); ���� ��ǥ�踦 ����� ��ȯ, ĳ���Ͱ� �ٶ󺸴� �������� �����̰�==transform.Translate

** // GameObject (Player)
        // Transform
        // PlayerController (*)

** Vector practice
// 1. ��ġ����
// 2. ���⺤��
struct MyVector
{
    public float x;
    public float y;
    public float z;

    public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } } // ��Ÿ��� ����. 3����
    public MyVector normalized { get { return new MyVector(x / magnitude, y / magnitude, z / magnitude); } } // ��������. Vector3.forward ������

    public MyVector(float x, float y, float z) { this.x = x; this.y = y; this.z = z; }
    public static MyVector operator +(MyVector a, MyVector b)
    {
        return new MyVector(a.x + b.x, a.y + b.y, a.z + b.z);
    }
    public static MyVector operator -(MyVector a, MyVector b)
    {
        return new MyVector(a.x - b.x, a.y - b.y, a.z - b.z);
    }
    public static MyVector operator *(float d, MyVector a)
    {
        return new MyVector(a.x * d, a.y * d, a.z * d);
    }
    public static MyVector operator *(MyVector a, float d)
    {
        return new MyVector(a.x * d, a.y * d, a.z * d);
    }
MyVector to = new MyVector(1.0f, 10.0f, 1.0f);
        MyVector from = new MyVector(1.0f, 5.0f, 1.0f);
        MyVector dir = to - from; // (0, 5, 0)
        dir = dir.normalized;

        MyVector newPose = dir * _speed;
}

    //float _yAngle = 0.0f;
    //_yAngle += Time.deltaTime * _speed;
    //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

** ���ϰ� rotate �ϴµ��� Quarternion �� ����. transform.rotation
    //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);
    //transform.Rotate(new Vector3(0.0f, Time.deltaTime * _speed, 0.0f));
    //Quaternion qt= transform.rotation;

** ������ ������ �� ���⸸ ��ȯ
    transform.rotation = Quaternion.LookRotation(Vector3.left); �� �ߴ��� �ʹ� �� ��� ������.
   transform.rotation= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f); //Slerp(current, aim, %)
 
** Slerp �� Translate �� �����̴�, ȣ�� �׸��� �����̴���. �ٶ󺸴� ������ ȣ�� ���� �ٲ�ϱ�.
    transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    transform.position += Vector3.forward * Time.deltaTime * _speed;  > ���� ������� ��ǥ ��ü�� �ٲ���.

** Prefab, Nested Prefab
    class Tank // Prefab
    {
        // �°� ����
        public float speed = 30.0f;
        Player player; // Nested Prefab (��ø�� Pre-fabrication)
    }

    class FastTank : Tank
    {

    }

    class Player
    {

    }

    Tank tank1 = new Tank(); // Instance
    Tank tank2 = new Tank();
    Tank tank3 = new Tank();
    Tank tank4 = new Tank();

 */