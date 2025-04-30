using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class InteractionItem : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemData itemData;
        [SerializeField] private InvenManager invenManager;


        public void OnInteract()
        {
            invenManager.AddItem(itemData);
            Destroy(transform.root.gameObject);
        }

    }
}
