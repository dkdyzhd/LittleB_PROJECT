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
                invenManager.AddItem(recipe.outputItem);
                Debug.Log("�ϼ� ������ �߰� �Ϸ�!");
            }
        }
    }
}
