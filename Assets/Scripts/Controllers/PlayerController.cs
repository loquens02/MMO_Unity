using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] // code 는 private 이고 Unity 에서 볼 수 있는 public 효과
    float _speed = 10.0f;
    Vector3 _destPos;
    

    void Start()
    {
        // Managers(통합관리) 안에 IunputManager 있고, 그 안에 KeyAction 전파자가 있다.
        // 걔한테 함수를 인자로 전달 == 구독신청. key가 눌리면 OnKeyboard 실행해달라.
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        // UI 생성 (임시) -- 40강에서 제거
        //Managers.Resource.Instantiate("UI/UI_Button");

        
        // UI 생성 (임시)
        UI_Button ui = Managers.UI.ShowPopupUI<UI_Button>(); // script 이름과 개체 이름을 같게 하면 편하다.
        //Managers.UI.ClosePopupUI(ui); - 바로 닫는 테스트용

    }

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
    }

    PlayerState _state = PlayerState.Idle;

    public void UpdateDie()
    {
        // 아무것도 안 함
    }
    public void UpdateIdle() 
    {
        // Animation - WAIT
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }
    public void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f) // 부동소수점 오류 고려, 찍은 지점 부근에 도착
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude); // 이동거리를 [0 ~ 남은거리] 사이값 이 되도록.
            transform.position += dir.normalized * moveDist; // 얼마만큼 이동. LookAt 없으면 방향 고정해서 이동

            //transform.LookAt(_destPos); // dir 넣으면 빙글 돌면서 간다. 
            //휙휙 도는거 방지
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
        }

        // Animation - RUN
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _speed); // 현재 속도로 set

    }



    void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
        }
  
    }

    
    
    /**
     클릭, 드래그
     */
    void OnMouseClicked(Define.MouseEvent evt)
    {
        // Observer 패턴 이용중이라, '클릭 이동' 기능만 예외적으로 여기서 따로 관리.
        if(_state == PlayerState.Die)
        {
            return;
        }

        if (evt == Define.MouseEvent.Click)
        {
            Debug.Log("OnMouseClicked"); // MouseEvent 실행확인
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // camera ~ click 지점 방향 벡터
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f); // 카메라 광선 1초 유지


        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall"))) // Wall Layer 만 광선 인식. 위치 이동 위해서.
        {
            _destPos = hit.point;   //Raycast 광선이 가리키는 목적지 좌표 정보
            _state = PlayerState.Moving;     //이동 중. 목적지로 가겠다.

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

        
** delegate 에게 함수가 호출됨을 알려줬으니, Update 문에 추가로 넣어줄 필요는 없다.
    void Update() { //OnKeyboard(); 구독 서비스에게 알려주고 있기에 필요 없다. 이러면 이동이 2배로 된다. }

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

** mouseClick 시에만 발생하도록 했던 Event > mouse 모든 입력 받게끔 조건 제거
    void OnMouseClicked(Define.MouseEvent evt)
    {
        //if(evt != Define.MouseEvent.Click)
        //{
        //    return; // click 아니면 무시
        //}

** Animation Blending 구현을 위해 State 패턴으로 바꾸는 과정. 일단 키보드 이동은 잠시 제거
    void Update(){ ...
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

    void Start(){ ...
    Managers.Input.KeyAction -= OnKeyboard; // 코드 다른데서 중복신청 해뒀을까봐
    Managers.Input.KeyAction += OnKeyboard;

    // State 패턴 적용 전에 사용했던 flag. 이동 중이냐 아니냐 상태 판단. 이동 True. 정지 False
    //public class PlayerController : MonoBehaviour
    //bool _moveToDest = false; 


** state 관리는 Unity UI 에서!
    //public class PlayerController : MonoBehaviour
    //float wait_run_ratio = 0;    

    //public void UpdateIdle()
    //wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime); // 기존 값에서 0(WAIT)까지 서서히 변화하도록
    //anim.SetFloat("wait_run_ratio", wait_run_ratio);
    //anim.Play("WAIT_RUN"); // 코드 대신 UI-state machine-mecanim 에서 관리

    //public void UpdateMoving()
    //wait_run_ratio = Mathf.Lerp(wait_run_ratio, 1, 10.0f * Time.deltaTime); // 기존 값에서 1(RUN)까지 서서히 변화하도록
    //anim.SetFloat("wait_run_ratio", wait_run_ratio);
    //anim.Play("WAIT_RUN"); // 코드 대신 UI-state machine 에서 관리

** Animation Event 특정 지점에 Effect 주기
    // OnRunEvet 오타. 이 함수가 없으면 다음 에러가 난다.
    // 'unitychan' AnimationEvent 'OnRunEvent' on animation 'RUN00_F' has no receiver! Are you missing a component?   
    public class PlayerController : MonoBehaviour
    {    
    void OnRunEvent(int a)
    {
        Debug.Log($"뚜벅 뚜벅~ {a}");
    }
    void OnRunEvent(string str)
    {
        Debug.Log($"뚜벅 뚜벅~ {str}");
    }
    
 */