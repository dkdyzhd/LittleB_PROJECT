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
                    Debug.Log("완성 아이템 전환 가능!");
                    return true;
                }
            }
            Debug.Log("조합 아이템이 부족합니다!");
            return false;
        }

        public void ConvertToItem(RecipeData recipe)
        {
            if (CanConvert(recipe))
            {
                foreach(RecipeData.Ingredient ingredient in recipe.ingredients)
                {
                    invenManager.RemoveItem(ingredient.item, ingredient.quantity);
                    Debug.Log("재료 차감!");
                }
                invenManager.AddItem(recipe.outputItem);
                Debug.Log("완성 아이템 추가 완료!");
            }
        }
    }
}
