using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : GenericSingleton<InputManager>
{
    GameManager GM;
    bool CanPress;
    // Start is called before the first frame update
   public void Init()
    {
        GM = GameManager.Instance;
        EventManager.AddListener(CName.newShape, PressKey);
    }


    public void PlayerUIInput(int value)
    {
        if (CanPress)
        {
            GM.currt_input_value = value;
            GM.ValidateInput();
            CanPress = false;
        }
    }

    // Update is called once per frame
   public void PlayerInputUpdate()
    {
        if (CanPress)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                GM.currt_input_value = -1;
                GM.ValidateInput();
                CanPress = false;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                GM.currt_input_value = 1;
                GM.ValidateInput();
                CanPress = false;
            }
        }
    }


    public void PressKey()
    {
        CanPress = true;
    }
}
