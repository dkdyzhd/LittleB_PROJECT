using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AYO
{
    public class ItemConverter : MonoBehaviour
    {
        [SerializeField] private InvenManager invenManager;

        public bool CanConvert(RecipeData recipe)
        {
            foreach(RecipeData.Ingredient ingredient in recipe.ingredients)
            {
                if(invenManager.HasItem(ingredient.item, ingredient.quantity))
                {
                    Debug.Log("�ϼ� ������ ��ȯ ����!");
                    return true;
                }
            }
            Debug.Log("���� �������� �����մϴ�!");
            return false;
        }

        public void ConvertToItem(RecipeData recipe)
        {
            if (CanConvert(recipe))
            {
                foreach(RecipeData.Ingredient ingredient in recipe.ingredients)
                {
                    invenManager.RemoveItem(ingredient.item, ingredient.quantity);
                    Debug.Log("��� ����!");
                }

                InteractionItem script = GetComponentInChildren<InteractionItem>(true);
                if (script != null)
                {
                    Debug.Log("��ũ��Ʈ ���� ����!");
                    invenManager.AddItem(script);
                }
                else
                {
                    Debug.LogWarning("�ش� �����տ� Script�� ����");
                }
                /*string prefabPath = $"Prefab/Item/{recipe.outputItem.dataName}";
                GameObject prefab = Resources.Load<GameObject>(prefabPath);
                if (prefab != null)
                {
                    Debug.Log($"������ �ε� ����: {prefab.name}");
                    // �����տ� ���� Ư�� ������Ʈ ��������
                    InteractionItem script = prefab.GetComponent<InteractionItem>();
                    
                    if (script != null)
                    {
                        Debug.Log("��ũ��Ʈ ���� ����!");
                        invenManager.AddItem(script);
                        // script�� ���� �ʿ��� �۾� ����
                    }
                    else
                    {
                        Debug.LogWarning("�ش� �����տ� Script�� ����");
                    }
                }
                else
                {
                    Debug.LogError($"�������� ã�� �� ����: {prefabPath}");
                }*/
                Debug.Log("�ϼ� ������ �߰� �Ϸ�!");
            }
        }
    }
}
