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


        // �� ������ �Ϸ��� enum component ������ Unity component ������ ��ġ�ؾ� �Ѵ�.
        // Prefab �����ϱ� �����Ϸ���.
        Get<Text>((int)Texts.ScoreText).text = "Score Text !";
    }

    void Bind<T>(Type type) where T : UnityEngine.Object
    {
        String[] names = Enum.GetNames(type); // enum �� ��ҵ�
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; // ��� ���
        _objects.Add(typeof(T), objects); // key: � ��ü ������ Bind �Ұ���. value: ��� ���

        // enum ��� �̸��� �����ϴ� ��ü ã��
        for(int i=0; i<names.Length; i++)
        {
            objects[i] = Util.FindChild<T>(gameObject, names[i], true); // gameObject: �ƹ��͵� �� �ҷ��� �Ǵ� �ֻ��� ��ü
        }
    }

    T Get<T>(int idx) where T: UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if(_objects.TryGetValue(typeof(T), out objects) == false) //������ �� �����ϸ� ����
        {
            return null;
        }

        // �����ϸ� �ε��� ��ġ�� ����(Object) �� T Ÿ������ ����ȯ�ؼ� ��ȯ
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
 ** Button �� Unity �󿡼� ������ �ٿ��ִ� ���
 public class UI_Button{
 [SerializeField]
    Text _text;
 public void OnButtonClick(){
    _text.text = $"����: {_score}";
 */ 