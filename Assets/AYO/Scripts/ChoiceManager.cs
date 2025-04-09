using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AYO
{
    public class ChoiceManager : MonoBehaviour
    {
        [SerializeField] private TextTableLoader tableLoader;
        [SerializeField] private GameObject choiceUI;
        [SerializeField] private Text choiceText;
        [SerializeField] private Image characterImage;

        private Choice choice;
        private string text;
        private string id;

        public void ShowChoice()
        {
            // to do : ��ȣ�ۿ� Ȥ�� ���� �̺�Ʈ�� �ҷ��ͼ� �������� ���������� �ϱ�
            text = tableLoader.GetChoiceData(choice.GetChoiceID());
        }
    }
}
