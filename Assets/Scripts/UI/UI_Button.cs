using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @details Prefab 
 * @pre enum ������� ������ Unity Prefab ���� component ������ ��ġ�ؾ� �Ѵ�
 */
public class UI_Button : UI_Base
{

    enum Buttons
    {
        PointButton
    }
    enum Texts
    {
        PointText,
        ScoreText
    }
    enum GameObjects
    {
        TestObject,
    }
    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));


        // �� ������ �Ϸ��� enum component ������ Unity component ������ ��ġ�ؾ� �Ѵ�.
        // Prefab �����ϱ� �����Ϸ���.
        Get<Text>((int)Texts.ScoreText).text = "Score Text !";
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