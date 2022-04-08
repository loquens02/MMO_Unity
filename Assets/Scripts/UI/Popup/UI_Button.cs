using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * <summary>Prefab </summary>
 * <remarks>enum ������� ������ Unity Prefab ���� component ������ ��ġ�ؾ� �Ѵ�</remarks>
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


        // �� ������ �Ϸ��� enum component ������ Unity component ������ ��ġ�ؾ� �Ѵ�.
        // Prefab �����ϱ� �����Ϸ���.
        //Get<Text>((int)Texts.ScoreText).text = "Score Text !"; �갡 scoreText �� ������ �ּ�

        // Extension - method channing by this
        GetButton((int)Buttons.PointButton).gameObject.BindEvent(OnButtonClicked);

        GameObject go = Get<Image>((int)Images.ItemIcon).gameObject; //�ϴ� �̹��� ���� GameObject. Event�� �����Ϸ� �ϱ⶧��
        BindEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }


    int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        //Debug.Log("click button !");
        _score++;
        Get<Text>((int)Texts.ScoreText).text = $"����: {_score}��";
    }
}

/**
 ** Button �� Unity �󿡼� ������ �ٿ��ִ� ���
 public class UI_Button{
 [SerializeField]
    Text _text;
 public void OnButtonClicked(){
    _text.text = $"����: {_score}";

** BindEvent
    UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
    evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; });
 */ 