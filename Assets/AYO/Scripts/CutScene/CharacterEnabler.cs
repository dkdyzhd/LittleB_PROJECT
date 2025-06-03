using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class CharacterEnabler : DirectorAction // DirectorAction 상속
    {
        [SerializeField] private GameObject characterToToggle; // 제어할 캐릭터 GameObject
        [SerializeField] private bool enableCharacter; // true면 활성화, false면 비활성화

        // SpriteRenderer와 Collider2D는 characterToToggle에서 찾아옵니다.
        private SpriteRenderer _charSprite;
        private Collider2D _charCollider;

        void Awake()
        {
            if (characterToToggle != null)
            {
                _charSprite = characterToToggle.GetComponent<SpriteRenderer>();
                _charCollider = characterToToggle.GetComponent<Collider2D>();
            }
            else
            {
                Debug.LogError("CharacterEnabler: characterToToggle is not assigned!");
            }
        }

        // DirectorAction의 추상 메소드 구현
        public override IEnumerator Execute()
        {
            if (_charSprite == null || _charCollider == null)
            {
                Debug.LogError("CharacterEnabler: Sprite or Collider not found on target character.");
                yield break; // 액션 실행 중단
            }

            _charSprite.enabled = enableCharacter;
            _charCollider.enabled = enableCharacter;

            Debug.Log($"Character {characterToToggle.name} {(enableCharacter ? "Enabled" : "Disabled")}");

            yield return null; // 이 액션은 즉시 완료된다고 가정. 필요시 WaitForSeconds 등 추가.

            CompleteAction(); // 액션 완료 알림
        }
    }
}
