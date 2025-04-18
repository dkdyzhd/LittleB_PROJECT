using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace AYO
{
    namespace InputInterface
    {
        public interface INavigateInputTarget // πÊ«‚ ≈∞
        {
            void OnNavigate(InputAction.CallbackContext context);
        }
    }
}
