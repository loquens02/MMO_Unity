using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    public enum Scene
    {
        Unknown,
        Login,
        Lobby,
        Game,
    }
    //public enum Sound
    //{
    //    Bgm,
    //    Effect,
    //    MaxCount,
    //}
    public enum UIEvent
    {
        Click,
        Drag,
    }
    public enum MouseEvent
    {
        Press, // 눌려있는 상태
        Click,
    }
    public enum CameraMode
    {
        QuaterView,
    }
}
