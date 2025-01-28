using System.Collections.Generic;
using UnityEngine;

public class Lection3Controller : MonoBehaviour
{
    [SerializeField] private string element;

    private List<string> elementsList = new List<string>();

    [ContextMenu("Додати елемент")]
    private void AddElement()
    {
        if (!string.IsNullOrEmpty(element))
        {
            elementsList.Add(element);
            Debug.Log($"Додано елемент: {element}");
        }
        else
        {
            Debug.Log("Поле 'element' порожнє. Нічого не додано");
        }
    }

    [ContextMenu("Видалити елемент")]
    private void RemoveElement()
    {
        if (elementsList.Remove(element))
        {
            Debug.Log($"Елемент '{element}' видалено зі списку");
        }
        else
        {
            Debug.Log($"Елемент '{element}' не знайдено у списку");
        }
    }

    [ContextMenu("Вивести список")]
    private void PrintList()
    {
        if (elementsList.Count == 0)
        {
            Debug.Log("Список порожній");
            return;
        }
        string result = string.Join(", ", elementsList);
        Debug.Log($"Список: {result}");
    }

    [ContextMenu("Відсортувати список")]
    private void SortList()
    {
        elementsList.Sort();
        Debug.Log("Список відсортовано");
    }

    [ContextMenu("Очистити список")]
    private void ClearList()
    {
        elementsList.Clear();
        Debug.Log("Список очищено");
    }
}