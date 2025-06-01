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

        public interface ISpaceInputTarget  // Space
        {

        }

        public interface ILeftMouseButtonTarget // ��Ŭ��
        {
            void OnLeftClick(InputAction.CallbackContext context);
        }

        public interface IOnInventoryTarget // i Ű
        {
            void OnInventory(InputAction.CallbackContext context);
        }
    }
}
