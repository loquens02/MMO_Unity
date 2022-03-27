using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    
    enum Buttons
    {
        PointButton
    }
    enum Texts
    {
        PointText,
        ScoreText
    }
    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));


        // 이 방법대로 하려면 enum component 순서와 Unity component 순서가 일치해야 한다.
        // Prefab 단위니까 가능하려나.
        Get<Text>((int)Texts.ScoreText).text = "Score Text !";
    }

    void Bind<T>(Type type) where T : UnityEngine.Object
    {
        String[] names = Enum.GetNames(type); // enum 속 요소들
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; // 요소 목록
        _objects.Add(typeof(T), objects); // key: 어떤 객체 유형을 Bind 할건지. value: 요소 목록

        // enum 요소 이름에 대응하는 개체 찾기
        for(int i=0; i<names.Length; i++)
        {
            objects[i] = Util.FindChild<T>(gameObject, names[i], true); // gameObject: 아무것도 안 불러도 되는 최상위 개체
        }
    }

    T Get<T>(int idx) where T: UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if(_objects.TryGetValue(typeof(T), out objects) == false) //꺼내는 데 실패하면 종료
        {
            return null;
        }

        // 성공하면 인덱스 위치의 무언가(Object) 를 T 타입으로 형변환해서 반환
        return objects[idx] as T;
    }

    int _score = 0;
    public void OnButtonClick()
    {
        _score++;
        Debug.Log("click button !");

        
    }
}

/**
 ** Button 을 Unity 상에서 손으로 붙여주는 방식
 public class UI_Button{
 [SerializeField]
    Text _text;
 public void OnButtonClick(){
    _text.text = $"점수: {_score}";
 */ 