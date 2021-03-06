﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectUtility {

    private static Dictionary<RecycleGameObject, ObjectPool> pools = new Dictionary<RecycleGameObject, ObjectPool>();

    private static ObjectPool GetObjectPool(RecycleGameObject reference) {
        ObjectPool pool = null;
        if (pools.ContainsKey(reference)) {
            pool = pools[reference];
        }
        else {
            GameObject poolContainer = new GameObject(reference.gameObject.name + "ObjectPool");
            pool = poolContainer.AddComponent<ObjectPool>();
            pool.prefab = reference;
            pools.Add(reference, pool);
        }
        return pool;
    }

    public static GameObject Instantiate(GameObject prefab,Vector3 position) {
        GameObject instance = null;

        RecycleGameObject recycledScript = prefab.GetComponent<RecycleGameObject>();
        if (recycledScript != null) {
            ObjectPool pool = GetObjectPool(recycledScript);
            instance = pool.NextObject(position).gameObject;
        }
        else {
            instance = GameObject.Instantiate(prefab);
            instance.transform.position = position;
        }
        return instance;
    }

    public static void Destroy(GameObject gameObject) {
        RecycleGameObject recycleGameObject = gameObject.GetComponent<RecycleGameObject>();
        if (recycleGameObject != null) {
            recycleGameObject.Shutdown();
        }
        else {
            GameObject.Destroy(gameObject);
        }
    }
}
