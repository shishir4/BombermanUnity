using System;
using System.Collections.Generic;
using UnityEngine;

/**
<summary>
Add controls in dictionary if need to add additional players.
</summary>
 */
public class PlayerControls : MonoBehaviour
{
    static int numPlayers = 4;
    // List<KeyValuePair<string, KeyCode>>[] ControlList = new List<KeyValuePair<string, KeyCode>>[5];
    static Dictionary<string, KeyCode>[] ControlDict = new Dictionary<string, KeyCode>[numPlayers];
    void Awake()
    {
        Dictionary<string, KeyCode> FirstPlayerControls = new Dictionary<string, KeyCode>();
        FirstPlayerControls.Add("left", KeyCode.A);
        FirstPlayerControls.Add("right", KeyCode.D);
        FirstPlayerControls.Add("bottom", KeyCode.S);
        FirstPlayerControls.Add("up", KeyCode.W);
        FirstPlayerControls.Add("bomb", KeyCode.Space);
        FirstPlayerControls.Add("remoteBomb", KeyCode.LeftShift);


        Dictionary<string, KeyCode> SecondPlayerControls = new Dictionary<string, KeyCode>();
        SecondPlayerControls.Add("left", KeyCode.LeftArrow);
        SecondPlayerControls.Add("right", KeyCode.RightArrow);
        SecondPlayerControls.Add("bottom", KeyCode.DownArrow);
        SecondPlayerControls.Add("up", KeyCode.UpArrow);
        SecondPlayerControls.Add("bomb", KeyCode.Return);
        SecondPlayerControls.Add("remoteBomb", KeyCode.RightShift);

        Dictionary<string, KeyCode> ThirdPlayerControls = new Dictionary<string, KeyCode>();
        ThirdPlayerControls.Add("left", KeyCode.J);
        ThirdPlayerControls.Add("right", KeyCode.L);
        ThirdPlayerControls.Add("bottom", KeyCode.K);
        ThirdPlayerControls.Add("up", KeyCode.I);
        ThirdPlayerControls.Add("bomb", KeyCode.O);
        ThirdPlayerControls.Add("remoteBomb", KeyCode.P);

        Dictionary<string, KeyCode> FourthPlayerControls = new Dictionary<string, KeyCode>();
        FourthPlayerControls.Add("left", KeyCode.Keypad4);
        FourthPlayerControls.Add("right", KeyCode.Keypad6);
        FourthPlayerControls.Add("bottom", KeyCode.Keypad2);
        FourthPlayerControls.Add("up", KeyCode.Keypad8);
        FourthPlayerControls.Add("bomb", KeyCode.KeypadEnter);
        FourthPlayerControls.Add("remoteBomb", KeyCode.KeypadPlus);


        ControlDict[0] = FirstPlayerControls;
        ControlDict[1] = SecondPlayerControls;
        ControlDict[2] = ThirdPlayerControls;
        ControlDict[3] = FourthPlayerControls;
    }

    public static Dictionary<string, KeyCode> GetPlayerControls(int id)
    {
        return ControlDict[id];
    }
}