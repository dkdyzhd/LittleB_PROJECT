using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class ENPCColider : MonoBehaviour
    {
        [SerializeField] private ENPCStateController stateController;
        [SerializeField] private GameObject player;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                stateController.SetAIState(ENPCAIState.AI_Combat);
                stateController.ToggleAggro(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                stateController.ToggleAggro(false);
                stateController.SetLastSeenPlayerPos(player.transform.position);
            }
        }

        
    }
}
