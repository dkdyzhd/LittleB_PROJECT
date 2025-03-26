using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    //플레이어가 정답을 맞췄을 때 그걸 알려주는 기능이 필요하다는 피드백을 반영했습니다 by 휘익
    //지금은 스프라이트 색상을 바꾸는 기능이 실행되지만 ToggleIndicator 메서드 내용을 바꾸면 다른 기능이 실행될 수 있습니다.
    
    public class Indicator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sp;
        private bool isActivated = false;
        private Color defaultColor = Color.grey;
        private Color activateColor = Color.red;

        // Start is called before the first frame update
        void Start()
        {
            // 자식 오브젝트에서 모든 SpriteRenderer를 가져와 리스트에 추가
            sp = GetComponent<SpriteRenderer>();
            sp.color = defaultColor;
        }

        // Indicator의 상태를 토글하는 메서드
        public void ToggleIndicator(bool state)
        {
            isActivated = state;
            {
                sp.color = isActivated ? activateColor : defaultColor;
            }
        }
    }
}
