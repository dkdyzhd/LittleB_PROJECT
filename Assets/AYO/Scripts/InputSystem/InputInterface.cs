using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace AYO
{
    namespace InputInterface
    {
        public interface INavigateInputTarget // ���� Ű
        {
            void OnNavigate(InputAction.CallbackContext context);
        }
    }
}
