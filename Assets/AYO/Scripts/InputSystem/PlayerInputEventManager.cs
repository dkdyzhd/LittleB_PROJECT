using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using AYO.InputInterface;

namespace AYO
{
    public class PlayerInputEventManager : MonoBehaviour
    {
        public INavigateInputTarget NavigateTarget { set; get; }
        public ILeftMouseButtonTarget LeftClickTarget { set; get; }

        public void OnNavigate(InputAction.CallbackContext context)
        {
            if(NavigateTarget != null)
            {
                NavigateTarget.OnNavigate(context);
            }
        }
        public void OnLeftClick(InputAction.CallbackContext context)
        {
            if (LeftClickTarget != null)
            {
                LeftClickTarget.OnLeftClick(context);
            }
        }
    }
}

// 방향 키 입력

// 플레이어, 선택지, 메뉴
