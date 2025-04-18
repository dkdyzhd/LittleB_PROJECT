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

// ���� Ű �Է�

// �÷��̾�, ������, �޴�
