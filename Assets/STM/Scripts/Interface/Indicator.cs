using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    //�÷��̾ ������ ������ �� �װ� �˷��ִ� ����� �ʿ��ϴٴ� �ǵ���� �ݿ��߽��ϴ� by ����
    //������ ��������Ʈ ������ �ٲٴ� ����� ��������� ToggleIndicator �޼��� ������ �ٲٸ� �ٸ� ����� ����� �� �ֽ��ϴ�.
    
    public class Indicator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sp;
        private bool isActivated = false;
        private Color defaultColor = Color.grey;
        private Color activateColor = Color.red;

        // Start is called before the first frame update
        void Start()
        {
            // �ڽ� ������Ʈ���� ��� SpriteRenderer�� ������ ����Ʈ�� �߰�
            sp = GetComponent<SpriteRenderer>();
            sp.color = defaultColor;
        }

        // Indicator�� ���¸� ����ϴ� �޼���
        public void ToggleIndicator(bool state)
        {
            isActivated = state;
            {
                sp.color = isActivated ? activateColor : defaultColor;
            }
        }
    }
}
