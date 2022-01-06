/***********************************************************************************************
 *Input Manager for player input controls
 *Can work with both keyboard controls and mouse clicks on ui button
 ************************************************************************************************/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : GenericSingleton<InputManager>
{
    GameManager GM;
    // Need to make sure multiple press donot happen
    bool CanPress;
    // Start is called before the first frame update
   public void Init()
    {
        GM = GameManager.Instance;
        EventManager.AddListener(CName.newShape, PressKey);
    }

    // UI Button Click Player input
    public void PlayerUIInput(int value)
    {
        if (CanPress)
        {
            EventManager.TriggerEvent(CName.click);
            GM.currt_input_value = value;
            GM.ValidateInput();
            CanPress = false;
        }
    }

    // Arrow Keys Player input
    public void PlayerInputUpdate()
    {
        if (CanPress)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                GM.currt_input_value = -1;
                GM.ValidateInput();
                CanPress = false;
                EventManager.TriggerEvent(CName.click);

            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GM.currt_input_value = 1;
                GM.ValidateInput();
                CanPress = false;
                EventManager.TriggerEvent(CName.click);

            }

        }
    }

    // Need to make sure multiple press donot happen and also updates score if no key is pressed
    public void PressKey()
    {
        if(CanPress)
        {
            GM.Score -= 100;
            EventManager.TriggerEvent(CName.wrong);
        }
        else
        CanPress = true;

    }
}
