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

                InteractionItem script = GetComponentInChildren<InteractionItem>(true);
                if (script != null)
                {
                    Debug.Log("스크립트 참조 성공!");
                    invenManager.AddItem(script);
                }
                else
                {
                    Debug.LogWarning("해당 프리팹에 Script가 없음");
                }
                /*string prefabPath = $"Prefab/Item/{recipe.outputItem.dataName}";
                GameObject prefab = Resources.Load<GameObject>(prefabPath);
                if (prefab != null)
                {
                    Debug.Log($"프리팹 로드 성공: {prefab.name}");
                    // 프리팹에 붙은 특정 컴포넌트 가져오기
                    InteractionItem script = prefab.GetComponent<InteractionItem>();
                    
                    if (script != null)
                    {
                        Debug.Log("스크립트 참조 성공!");
                        invenManager.AddItem(script);
                        // script를 통해 필요한 작업 수행
                    }
                    else
                    {
                        Debug.LogWarning("해당 프리팹에 Script가 없음");
                    }
                }
                else
                {
                    Debug.LogError($"프리팹을 찾을 수 없음: {prefabPath}");
                }*/
                Debug.Log("완성 아이템 추가 완료!");
            }
        }
    }
}
