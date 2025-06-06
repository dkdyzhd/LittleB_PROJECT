﻿using AYO.InputInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

namespace AYO
{
    public class PlayerController : MonoBehaviour, INavigateInputTarget, ILeftMouseButtonTarget, IOnInventoryTarget
    {
        [Header("플레이어 HP")]
        [SerializeField] private int playerHP;

        [Header("상태 체크 및 움직임")]
        [SerializeField] private float knockbackDuration = 1.0f;
        [SerializeField] private float groundCheckWidthMultiplier = 0.9f; // 지면 체크 너비 (캐릭터 크기 대비)
        [SerializeField] private float groundCheckHeight = 0.1f;         // 지면 체크 높이
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundadjusted = 0.1f;
        [SerializeField] private float jumpForce = 5f;
        [SerializeField] private float speed = 3f;
        [SerializeField] private float shootDelay = 0.5f;
        private Vector2 v;
        private float knockbackTimer;
        private float bulletDir;
        private bool isShooting;
        private float shootingTimer;

        // 컴포넌트
        private Rigidbody2D rb;
        private Animator ani;
        private SpriteRenderer sp;
        private Collider2D[] playerInteractableCollider;

        [Header("입력매니저")]
        [SerializeField] private PlayerInputEventManager pInputManager;

        [Header("인벤토리매니저")]
        [SerializeField] private InvenManager invenManager;

        [Header("Skill")]
        [SerializeField] private SlingShotSkill skill;

        // 상태값
        private bool isGrounded;
        private bool isJumping;
        private bool reachedApex;
        private bool isKnockbacking;
        private bool canControl = true; // by 휘익 250526
        private bool isDead = false;

        public bool IsDead => isDead;

        [Header("Sprites")]
        [SerializeField] private Sprite defaultSprite;
        [SerializeField] private Sprite jumpSprite;
        [SerializeField] private Sprite MaxhighSprite;
        [SerializeField] private Sprite fallSprite1;
        [SerializeField] private Sprite fallSprite2;

        [Header("Fall Sprite Settings")]
        [SerializeField] private float fallSpriteChangeInterval = 0.1f;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sp = GetComponent<SpriteRenderer>();
            ani = GetComponent<Animator>();

            if (pInputManager != null) // 20250530_ 수정
            {
                pInputManager.NavigateTarget = this;
                pInputManager.LeftClickTarget = this;
                pInputManager.OnInventoryTarget = this;
            }
            Vector2 dir = transform.right;
            bulletDir = dir.x;
        }

        private void Update()
        {
            // 대화 중이면 이동과 스프라이트 갱신 중단
            //if (dialogueManager.IsConversationActive)
            //{
            //    rb.velocity = new Vector2(0, rb.velocity.y);
            //    return;
            //}
            if (!canControl) // by 휘익 250526
            {
                // 조작이 불가능할 때 강제로 멈추거나 특정 애니메이션을 재생할 수도 있음
                if (rb != null) rb.velocity = new Vector2(0, rb.velocity.y); // 수평 이동 정지
                if (ani != null && isGrounded) ani.SetBool("IsRun", false); // 달리기 애니메이션 정지
                return;
            }

            if (isShooting)
            {
                shootingTimer -= Time.deltaTime;

                if (shootingTimer < 0)
                {
                    isShooting = false;
                }
                return;
            }

            if (isKnockbacking)
            {
                knockbackTimer -= Time.deltaTime;

                if(knockbackTimer <= 0f)
                {
                    isKnockbacking = false;
                }
                return;
            }

            HandleJump();    
            UpdateSprite();
            MoveCharacter(v);

        }

        private void FixedUpdate()
        {
            // 대화 중이면 이동 중단
            //if (dialogueManager.IsConversationActive)
            //    return;

            CheckGrounded();  // 땅에 닿았는지 확인

            // 땅에 있을 때만 이동 처리
            //if (isGrounded)
            //{
            //    MoveCharacter(InputSystem.Singleton.MoveInput);
            //}
            if (!canControl) return; // by 휘익 250526

            if (rb.velocity.y > 0 || rb.velocity.y < 0)
            {
                if(!isGrounded)
                    ani.enabled = false;
            }
        }

        public void OnNavigate(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            // 조작 불가능 상태면 입력 무시 by 휘익 250526
            if (!canControl)
            {
                v = Vector2.zero; // 입력 값 초기화
                return;
            }

            if (context.performed || context.canceled)
            {
                v = context.ReadValue<Vector2>();
            }
        }

        /// <summary>
        /// 좌우 이동 처리
        /// </summary>
        private void MoveCharacter(Vector2 direction)
        {
            // 넉백 상태일 때는 리턴
            if (isKnockbacking) return;

            if(isGrounded)
            {
                if (direction != Vector2.zero)
                {
                    // 만약 점프 중이 아니면 달리기 애니메이션 재생
                    if (!isJumping && ani != null) // null 체크 추가 by 휘익 250526
                    {
                        ani.SetBool("IsRun", true);
                    }
                    if (rb != null) rb.velocity = new Vector2(direction.x * speed, rb.velocity.y); // null 체크 추가 by 휘익 250526


                    // 캐릭터 좌우 반전
                    Vector3 scale = transform.localScale;
                    scale.x = (direction.x < 0) ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
                    transform.localScale = scale;

                    bulletDir = scale.x;
                }
                else
                {
                    // 이동 입력이 없으면 달리기 애니메이션 끔
                    if (ani != null) ani.SetBool("IsRun", false); // null 체크 추가
                    if (rb != null) rb.velocity = new Vector2(0, rb.velocity.y); // null 체크 추가
                }
            }          
        }

        /// <summary>
        /// 땅에 닿았는지 확인
        /// </summary>
        private void CheckGrounded()
        {
            if (rb == null) return;

            Collider2D collider = GetComponent<Collider2D>();
            if (collider == null) return; //null 체크 추가 by 휘익 250526

            Vector2 groundCheckPosition = new Vector2(
                transform.position.x,
                transform.position.y - collider.bounds.extents.y - groundadjusted
            );

            Vector2 boxSize = new Vector2(
                collider.bounds.size.x * groundCheckWidthMultiplier,
                groundCheckHeight
            );

            // 겹치는 Collider2D가 있으면 땅으로 판정
            Collider2D groundCollider = Physics2D.OverlapBox(groundCheckPosition, boxSize, 0f, groundLayer);
            isGrounded = groundCollider != null;

            // 땅에 착지하면 점프 상태 해제 + 기본 스프라이트 복귀
            if (isGrounded)
            {
                isJumping = false;
                reachedApex = false;
                if (ani != null) ani.enabled = true; // null 체크 추가 by 휘익 250526
            }
        }

        /// <summary>
        /// 점프 처리
        /// </summary>
        private void HandleJump()
        {
            if (!canControl) return; //by 휘익 250526

            // 점프 키를 눌렀고, 땅에 닿아 있다면
            if (InputSystem.Singleton.Jump && isGrounded)
            {
                // 달리기 애니메이션 끄기
                ani.SetBool("IsRun", false);
                
                // 점프 상태로 전환
                isJumping = true;

                // 점프 스프라이트 적용
                sp.sprite = jumpSprite;

                // 점프 위력 부여
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

       
        private void UpdateSprite()
        {
         
             //Debug.Log($"isGrounded: {isGrounded}, velocityY: {rb.velocity.y}, isJumping: {isJumping}");

            // 만약 땅에서 떨어져있다면(점프 상태) => 상승/최고점/낙하 스프라이트
            if (!isGrounded)
            {
                float yVelocity = rb.velocity.y;

                // 상승 중
                if (yVelocity > 0.1f)
                {
                    sp.sprite = jumpSprite;
                }
                // 최고점
                else if (yVelocity <= 0.1f && yVelocity >= -0.1f && !reachedApex)
                {
                    reachedApex = true;
                    sp.sprite = MaxhighSprite;
                }
                // 낙하 중
                else if (yVelocity < -0.1f)
                {
                    float t = Mathf.Repeat(Time.time, fallSpriteChangeInterval * 2f);
                    sp.sprite = (t < fallSpriteChangeInterval) ? fallSprite1 : fallSprite2;
                }
            }
            else
            {
                // 땅에 닿아있다면 기본 스프라이트
                sp.sprite = defaultSprite;
            }
        }

        private void OnDrawGizmos()
        {
            if (rb == null) rb = GetComponent<Rigidbody2D>();
            if (rb == null) return;

            Collider2D collider = GetComponent<Collider2D>();

            Vector2 groundCheckPosition = new Vector2(
                transform.position.x,
                transform.position.y - collider.bounds.extents.y - groundadjusted
            );

            Vector2 boxSize = new Vector2(
                collider.bounds.size.x * groundCheckWidthMultiplier,
                groundCheckHeight
            );

            // 씬 뷰에서 지면 체크 범위 확인용
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheckPosition, boxSize);
        }
        public bool HasItem(CharacterData characterData)
        {
            // 아이템이 있는지 확인하는 코드
            return true;
        }

        public bool NotHasItem()
        {
            return false;
        }
        public void KnockBack(Vector3 v)
        {
            // 밀리고 넉백
            if (!isKnockbacking)
            {
                isKnockbacking = true;
                knockbackTimer = knockbackDuration;
                ani.SetBool("IsRun", false);
                rb.AddForce(v * 13.0f, ForceMode2D.Impulse);
                Debug.Log("KnockBack!");
            }
        }

        public void GetDamage(int amount)
        {
            if (isDead) return;
            playerHP -= amount;
            Debug.Log($"현재 HP : " + playerHP);

            if (playerHP <= 0)
            {
                Die();
            }
        }

        public void Heal(int amount)
        {
            playerHP += amount;
            playerHP = Mathf.Min(playerHP, 10);
            Debug.Log($"회복됨! 현재 HP: {playerHP}");
        }

        private void Die()
        {
            isDead = true;
            canControl = false;
            ani.SetTrigger("Dead");
            rb.velocity = Vector2.zero;
            Debug.Log("플레이어 사망!");
        }

        public void OnInventory(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Debug.Log("인벤토리토글 눌림");
                invenManager.OnInventoryBag();
            }
        }

        public void OnLeftClick(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            if(context.performed && isGrounded && !isShooting)
            {
                isShooting = true;
                ani.SetBool("IsRun", false);
                ani.SetTrigger("Shoot");
                shootingTimer = shootDelay;
            }
        }

        public void BulletShoot()
        {
            skill.Shoot(bulletDir);
        }

        // 상호작용 아이템 condition용 함수
        public bool IsObjectNear(GameObject requireObj)
        {
            //playerInteractableCollider.gameObject   => 게임오브젝트를 가져와서 비교할 것 !
            if(requireObj == null) return false;
            if(playerInteractableCollider == null) return false;

            for (int i = 0; i < playerInteractableCollider.Length; i++)
            {
                //콜라이더 == 널이면 브레이크
                if (playerInteractableCollider[i] == null) break;
                if (playerInteractableCollider[i].gameObject == requireObj)
                {
                    return true;
                }
            }
            return false;
        }

        public void SetCurrentInteractable(Collider2D[] interactableCollider)
        {
            playerInteractableCollider = interactableCollider;
        }

        public void SetControl(bool enabled) // by 휘익 250526
        {
            canControl = enabled;
            if (!enabled) // 조작 비활성화 시
            {
                // 1. 현재 이동 입력을 즉시 0으로 만듭니다.
                v = Vector2.zero;

                // 2. 물리적인 현재 이동을 멈춥니다 (수평).
                if (rb != null)
                {
                    rb.velocity = new Vector2(0, rb.velocity.y);
                }

                // 3. 달리기 애니메이션을 멈춥니다.
                if (ani != null)
                {
                    ani.SetBool("IsRun", false);
                    // 필요하다면 Animator를 비활성화하거나 Idle 상태로 강제
                    // ani.enabled = false; // 또는 ani.Play("Idle");
                }
            }
            Debug.Log($"PlayerController : Controll set to {enabled}");
        }

        
    }
}
