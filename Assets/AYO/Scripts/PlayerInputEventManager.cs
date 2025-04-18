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

        public void OnNavigate(InputAction.CallbackContext context)
        {
            NavigateTarget.OnNavigate(context);
        }
    }
}

// 방향 키 입력

// 플레이어, 선택지, 메뉴
