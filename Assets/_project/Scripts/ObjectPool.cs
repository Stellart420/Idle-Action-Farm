using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;   // префаб, который будем использовать для создания объектов
    [SerializeField] private int poolSize = 500;  // количество объектов в пуле

    private List<GameObject> pool;  // список объектов в пуле

    private void Awake()
    {
        pool = new List<GameObject>();

        // создаем объекты и добавляем их в пул
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(_prefab, transform);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    // метод для получения объекта из пула
    public GameObject GetObject()
    {
        // ищем первый неактивный объект в пуле и возвращаем его
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // если не нашли неактивный объект, создаем новый и добавляем его в пул
        GameObject newObj = Instantiate(_prefab, transform);
        pool.Add(newObj);
        return newObj;
    }

    // метод для возвращения объекта в пул
    public void ReturnObject(GameObject obj)
    {
        obj.transform.SetParent(transform);
        obj.SetActive(false);
    }
}