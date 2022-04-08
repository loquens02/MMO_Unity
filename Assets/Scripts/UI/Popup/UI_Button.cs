using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * <summary>Prefab </summary>
 * <remarks>enum 구성요소 순서와 Unity Prefab 내부 component 순서가 일치해야 한다</remarks>
 */
public class UI_Button : UI_Popup
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
    enum Images
    {
        ItemIcon,
    }
    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));


        // 이 방법대로 하려면 enum component 순서와 Unity component 순서가 일치해야 한다.
        // Prefab 단위니까 가능하려나.
        //Get<Text>((int)Texts.ScoreText).text = "Score Text !"; 얘가 scoreText 를 덮어씌우니 주석

        // Extension - method channing by this
        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        GameObject go = Get<Image>((int)Images.ItemIcon).gameObject; //일단 이미지 말고 GameObject. Event를 연동하려 하기때문
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }


    int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        //Debug.Log("click button !");
        _score++;
        Get<Text>((int)Texts.ScoreText).text = $"점수: {_score}점";
    }
}

/**
 ** Button 을 Unity 상에서 손으로 붙여주는 방식
 public class UI_Button{
 [SerializeField]
    Text _text;
 public void OnButtonClicked(){
    _text.text = $"점수: {_score}";

** BindEvent
    UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
    evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });
 */ 