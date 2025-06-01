using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace AYO
{
    namespace InputInterface
    {
        public interface INavigateInputTarget // 방향 키
        {
            void OnNavigate(InputAction.CallbackContext context);
        }

        public interface ISpaceInputTarget  // Space
        {

        }

        public interface ILeftMouseButtonTarget // 좌클릭
        {
            void OnLeftClick(InputAction.CallbackContext context);
        }

        public interface IOnInventoryTarget // i 키
        {
            void OnInventory(InputAction.CallbackContext context);
        }
    }
}
