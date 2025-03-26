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
            Debug.Log("CombinedLever�� OnInteract �޼��� ȣ���!");
            isLeverOn = !isLeverOn;
            FlipSprite();

            if (isActivated)
            {
                foreach (GameObject obj in linkedObjects)
                {
                    ActivateIndicator(obj); // Indicator Ŭ�������� ������ by ���� (25.03.04)
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

        //Indicator�� ���Ե� ToggleIndicator �޼��带 ���� �÷��̾ ���� ������ �۵���Ű�� �ε������� ������Ʈ�� �̹��� ������ �ٲ� (25.03.04)
        private void ActivateIndicator(GameObject obj)
        {
            Indicator indicatorScript = obj.GetComponent<Indicator>();
            if (indicatorScript != null)
            {
                indicatorScript.ToggleIndicator(isLeverOn);
                Debug.Log($"Indicator Ȱ��ȭ��: {obj.name}");
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

                Debug.Log(isActivated ? "BlackLever�� ���� ���·� �����!" : "BlackLever�� ���� ���·� �����!");
            }
        }

        public bool IsOnV2  // �̸��� ���ļ� �ӽ÷� �̸��� �ٲ���ϴ�
        {
            get { return isLeverOn; }
        }

        private void FlipSprite()
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }
}

