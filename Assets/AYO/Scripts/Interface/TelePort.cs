using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class Teleport : MonoBehaviour, IInteractable
    {
        [Header("Teleport Settings")]
        [SerializeField] private Transform teleportOutput;

        [SerializeField] private float delay = 1f;// 텔레포트 출력 위치

        //private Camera mainCamera;
        [SerializeField] private ScreenFader screenFader;
        private PlayerController playerController;
        private bool isTeleporting = false;
        private float teleportStartTime = 0f;
        [SerializeField]
        private float teleportadjusted = 0.5f;
        private void Start()
        {
            //mainCamera = Camera.main;
            playerController = FindObjectOfType<PlayerController>();
            screenFader = FindObjectOfType<ScreenFader>();

            /*if (mainCamera == null)
                Debug.LogError("메인 카메라가 없습니다!");*/

            if (playerController == null)
                Debug.LogError("PlayerController가 없습니다!");

            if (screenFader == null)
                Debug.LogError("?");
        }

        private void Update()
        {
            if (isTeleporting)
            {
                float elapsedTime = Time.time - teleportStartTime;

                if (elapsedTime >= delay / 2f)
                {

                    playerController.transform.position = new Vector3(teleportOutput.position.x, teleportOutput.position.y - teleportadjusted, teleportOutput.position.z);

                }

                if (elapsedTime >= delay)
                {
                    //mainCamera.cullingMask = -1;

                    if (screenFader != null)
                    {
                        screenFader.ScreenFadeIn();
                        Debug.Log("!");
                    }

                    playerController.enabled = true;


                    isTeleporting = false;
                }

            }
        }
        public void OnInteract()
        {
            if (teleportOutput != null)
            {

                isTeleporting = true;
                teleportStartTime = Time.time;
                playerController.enabled = false;

                //mainCamera.cullingMask = 0; // Nothing
                if (screenFader != null)
                {
                    screenFader.ScreenFadeOut();
                    Debug.Log("?");
                }

            }
            else if (isTeleporting)
            {
                Debug.LogWarning("이미 텔레포트 중입니다!");
            }
            else
            {
                Debug.LogWarning("텔레포트 출력 위치 또는 카메라가 설정되지 않았습니다!");
            }
        }

    }
}