using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] // code 는 private 이고 Unity 에서 볼 수 있는 public 효과
    float _speed = 10.0f;
    bool _moveToDest = false;
    Vector3 _destPos;

    void Start()
    {
        // Managers(통합관리) 안에 IunputManager 있고, 그 안에 KeyAction 전파자가 있다.
        // 걔한테 함수를 인자로 전달 == 구독신청. key가 눌리면 OnKeyboard 실행해달라.
        Managers.Input.KeyAction -= OnKeyboard; // 코드 다른데서 중복신청 해뒀을까봐
        Managers.Input.KeyAction += OnKeyboard;

        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

    }

   
    void Update()
    {
        //OnKeyboard(); 구독 서비스에게 알려주고 있기에 필요 없다. 이러면 이동이 2배로 된다.
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

        _moveToDest = false; // 키보드 이동시에는 Raycast 이동 방식은 사용하지 않겠다.
    }

    void OnMouseClicked(Define.MouseEvent evt)
    {
        if(evt != Define.MouseEvent.Click)
        {
            return; // click 아니면 무시
        }

        Debug.Log("OnMouseClicked"); // 이쯤에서 한 번 실행확인.

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // camera ~ click 지점 방향 벡터
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f); // 카메라 광선 1초 유지

        
        

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall"))) // Wall Layer 만 광선 인식. 위치 이동 위해서.
        {
            _destPos = hit.point;   //Raycast 광선이 가리키는 목적지 좌표 정보
            _moveToDest = true;     //목적지로 가겠다.

            //Debug.Log($"Raycast Camera tag : {hit.collider.gameObject.tag}");
        }
    }
}
/*
** Update() - new Vector3(0.0f, 0.0f, 1.0f) 자주 쓰니 예약어가 있다
** transform.position+=transform.TransformDirection(Vector3.forward*Time.deltaTime*_speed); 로컬 좌표계를 월드로 변환, 캐릭터가 바라보는 방향으로 움직이게==transform.Translate

** // GameObject (Player)
        // Transform
        // PlayerController (*)

** Vector practice
// 1. 위치벡터
// 2. 방향벡터
struct MyVector
{
    public float x;
    public float y;
    public float z;

    public float magnitude { get { return Mathf.Sqrt(x * x + y * y + z * z); } } // 피타고라스 정리. 3차원
    public MyVector normalized { get { return new MyVector(x / magnitude, y / magnitude, z / magnitude); } } // 단위벡터. Vector3.forward 같은거

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

** 여하간 rotate 하는데는 Quarternion 만 쓴다. transform.rotation
    //transform.eulerAngles = new Vector3(0.0f, _yAngle, 0.0f);
    //transform.Rotate(new Vector3(0.0f, Time.deltaTime * _speed, 0.0f));
    //Quaternion qt= transform.rotation;

** 누르는 쪽으로 몸 방향만 전환
    transform.rotation = Quaternion.LookRotation(Vector3.left); 만 했더니 너무 각 잡고 돌더라.
   transform.rotation= Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f); //Slerp(current, aim, %)
 
** Slerp 에 Translate 로 움직이니, 호를 그리며 움직이더라. 바라보는 방향이 호를 따라 바뀌니까.
    transform.Translate(Vector3.forward * Time.deltaTime * _speed);
    transform.position += Vector3.forward * Time.deltaTime * _speed;  > 방향 관계없이 좌표 자체를 바꾸자.

** Prefab, Nested Prefab
    class Tank // Prefab
    {
        // 온갖 정보
        public float speed = 30.0f;
        Player player; // Nested Prefab (중첩된 Pre-fabrication)
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