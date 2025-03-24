using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class CombinedLever : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool isActivated = false;
        [SerializeField] private List<GameObject> linkedObjects = new List<GameObject>();

        private SpriteRenderer spriteRenderer;
        private Color defaultColor;
        private Color redColor = Color.red;

        private bool isLeverOn = false;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            defaultColor = spriteRenderer.color;

            if (CompareTag("RedLever"))
            {
                isActivated = true;
                spriteRenderer.color = redColor;
            }
            else if (CompareTag("BlackLever"))
            {
                isActivated = false;
                spriteRenderer.color = defaultColor;
            }

            UpdateLeverColor();
        }

        public void OnInteract()
        {
            Debug.Log("CombinedLever의 OnInteract 메서드 호출됨!");
            isLeverOn = !isLeverOn;
            FlipSprite();

            if (isActivated)
            {
                foreach (GameObject obj in linkedObjects)
                {
                    ActivateIndicator(obj); // Indicator 클래스에서 가져옴 by 휘익 (25.03.04)
                    ActivateHorizontalMove(obj);
                    ActivateVerticalMove(obj);
                    ActivateDisable(obj);
                }
            }
        }

        private void UpdateLeverColor()
        {
            spriteRenderer.color = isActivated ? redColor : defaultColor;
        }

        //Indicator에 포함된 ToggleIndicator 메서드를 실행 플레이어가 정답 레버를 작동시키면 인디케이터 오브젝트의 이미지 색상이 바뀜 (25.03.04)
        private void ActivateIndicator(GameObject obj)
        {
            Indicator indicatorScript = obj.GetComponent<Indicator>();
            if (indicatorScript != null)
            {
                indicatorScript.ToggleIndicator(isLeverOn);
                Debug.Log($"Indicator 활성화됨: {obj.name}");
            }
        }

        private void ActivateHorizontalMove(GameObject obj)
        {
            HorizontalMoveObject horMove = obj.GetComponent<HorizontalMoveObject>();
            if (horMove != null)
            {
                horMove.ToggleMovement(isLeverOn);
            }
        }

        private void ActivateVerticalMove(GameObject obj)
        {
            VerticalMoveObject verMove = obj.GetComponent<VerticalMoveObject>();
            if (verMove != null)
            {
                verMove.ToggleMovement(isLeverOn);
            }
        }

        private void ActivateDisable(GameObject obj)
        {
            Disable disableScript = obj.GetComponent<Disable>();
            if (disableScript != null)
            {
                disableScript.ToggleActiveState(isLeverOn);
            }
        }

        public void ToggleLever()
        {
            if (CompareTag("BlackLever"))
            {
                isActivated = !isActivated;
                UpdateLeverColor();

                Debug.Log(isActivated ? "BlackLever가 빨간 상태로 변경됨!" : "BlackLever가 검정 상태로 변경됨!");
            }
        }

        public bool IsOnV2  // 이름이 겹쳐서 임시로 이름을 바꿨습니다
        {
            get { return isLeverOn; }
        }

        private void FlipSprite()
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}

