﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class Teleport_Trigger : MonoBehaviour
    {
        [Header("Teleport Settings")]
        [SerializeField] private Transform teleportOutput;

        [SerializeField] private int delay = 1;// 텔레포트 출력 위치
        [SerializeField] Collider2D portalTrigger;

        private Camera mainCamera;
        private PlayerController playerController;
        private bool isTeleporting = false;
        private float teleportStartTime = 0f;
        [SerializeField]
        private float teleportadjusted = 0.5f;
        private void Start()
        {
            mainCamera = Camera.main;
            playerController = FindObjectOfType<PlayerController>();

            if (mainCamera == null)
                Debug.LogError("메인 카메라가 없습니다!");

            if (playerController == null)
                Debug.LogError("PlayerController가 없습니다!");
        }

        private void Update()
        {
            if (isTeleporting)
            {
                float elapsedTime = Time.time - teleportStartTime;

                if (elapsedTime >= delay)
                {

                    playerController.transform.position = new Vector3(teleportOutput.position.x, teleportOutput.position.y - teleportadjusted, teleportOutput.position.z);


                   mainCamera.cullingMask = -1; 

                 
                    playerController.enabled = true;

           
                    isTeleporting = false;
                }
            }
        }
        void OnTriggerEnter2D(Collider2D portalTrigger)
        {
            if (portalTrigger.CompareTag("Player"))
            {
                if (teleportOutput != null)
                {

                    isTeleporting = true;
                    teleportStartTime = Time.time;
                    playerController.enabled = false;

                    mainCamera.cullingMask = 0; // Nothing
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
}