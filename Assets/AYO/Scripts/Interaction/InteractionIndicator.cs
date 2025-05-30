using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace AYO
{
    public class InteractionIndicator : MonoBehaviour
    {
        [SerializeField] SpriteRenderer interactionIndicator;
        [SerializeField] Collider2D somethingToInteract;
        
        // Start is called before the first frame update
        void Start()
        {
            interactionIndicator.enabled = false;
        }

        void OnTriggerEnter2D(Collider2D somethingToInteract)
        {
            if (somethingToInteract.CompareTag("Player"))
            {
                interactionIndicator.enabled = true;
            }
        }

        private void OnTriggerExit2D(Collider2D somethingToInteract)
        {
            if (somethingToInteract.CompareTag("Player"))
            {
                interactionIndicator.enabled = false;
            }
        }
    }
}
