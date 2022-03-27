using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * @details Prefab 
 * @pre enum 구성요소 순서와 Unity Prefab 내부 component 순서가 일치해야 한다
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


        // 이 방법대로 하려면 enum component 순서와 Unity component 순서가 일치해야 한다.
        // Prefab 단위니까 가능하려나.
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
 ** Button 을 Unity 상에서 손으로 붙여주는 방식
 public class UI_Button{
 [SerializeField]
    Text _text;
 public void OnButtonClick(){
    _text.text = $"점수: {_score}";
 */ 