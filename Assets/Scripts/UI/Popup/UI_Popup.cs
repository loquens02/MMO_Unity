using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    /**
     * @details order 필요 여부- true
     */
    public override void Init()
    {
        Managers.UI.SetCanvas(gameObject, sort: true);
    }

    public virtual void ClosePopup()
    {
        Managers.UI.ClosePopupUI(this);
    }
}
