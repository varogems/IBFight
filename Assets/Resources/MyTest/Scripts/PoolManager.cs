using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolManager : MonoBehaviour
{


    public enum ParticleObjectPoolType
    {
        ParticleFireShooting = 0,
        ParticleFireWork1,
        ParticleFireWork2,
        ParticleFireWork3,
        Count
    };


    //!------------------------------------------------------------------------
    static List<KeyValuePair<int, GameObject>> m_listGameObjectPool;
    static Transform m_transform;
    public static PoolManager m_instance { get; private set; }

    //!------------------------------------------------------------------------
    void Awake()
    {
        if (FindObjectsOfType(this.GetType()).Length > 1)
        {
            this.gameObject.SetActive(false);
            Destroy(this.gameObject);
            return;
        }
        else
        {

            m_transform = transform;
            StartCoroutine(IEWaitGenerateLevel());

            m_instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
    }

    IEnumerator IEWaitGenerateLevel()
    {
        yield return new WaitWhile(() => GenerateLevel.m_instance == null);

        InitGameObjectPool();
    }

    public static void InitGameObjectPool()
    {
        m_listGameObjectPool = new List<KeyValuePair<int, GameObject>>();

        GameObject gameObject;
        for (int i = 0; i < (int)Human.Character.Count; i++)
        {
            //! Create Gameobject with component ObjectPool
            gameObject = new GameObject();
            gameObject.name = GenerateLevel.m_instance.GetNamePrefabByIndex(i);
            gameObject.AddComponent<ObjectPool>();
            gameObject.GetComponent<ObjectPool>().SetPrefab(GenerateLevel.m_instance.GetPrefabByIndex(i));

            //!  Add this gameobject to gameobject with name "PoolManager"
            m_listGameObjectPool.Add(new KeyValuePair<int, GameObject>(i, gameObject));
            gameObject.transform.SetParent(m_transform);
        }

        gameObject = null;

        // Debug.Log("Init InitGameObjectPool success!");
    }





    //! Spawn bullet for player
    public static void SpawnCharacter(Human.Character character, Transform _transformPlayer)
    {

        GameObject _characterObject = m_listGameObjectPool[(int)character].Value.GetComponent<ObjectPool>().GetPooledObject();
        _characterObject.GetComponent<Human>().Reset();

        Vector3 posAppear = new Vector3(_transformPlayer.position.x + _transformPlayer.localScale.x / 2,
                                        _transformPlayer.position.y + 1,
                                        _transformPlayer.position.z);

        _characterObject.transform.position = posAppear;
        _characterObject.transform.localScale = _transformPlayer.localScale;
        _characterObject = null;

    }


    public static GameObject SpawnCharacter(Human.Character character, Vector3 position)
    {

        GameObject _characterObject = m_listGameObjectPool[(int)character].Value.GetComponent<ObjectPool>().GetPooledObject();
        _characterObject.GetComponent<Human>().Reset();


        _characterObject.transform.position = position;
        _characterObject.transform.localScale = Vector3.one;
        return _characterObject;

    }

    public static bool IsDeactiveAllObjectByType()
    {
        return m_listGameObjectPool[(int)Human.Character.Player].Value.GetComponent<ObjectPool>().IsDeactiveAllObject() &&
                m_listGameObjectPool[(int)Human.Character.Homeless].Value.GetComponent<ObjectPool>().IsDeactiveAllObject() &&
                m_listGameObjectPool[(int)Human.Character.Boss].Value.GetComponent<ObjectPool>().IsDeactiveAllObject();
    }


    public static GameObject getNextActiveObject()
    {
        for (int indexType = 0; indexType < (int)Human.Character.Count; indexType++)
            if (m_listGameObjectPool[indexType].Value.activeSelf)
                return m_listGameObjectPool[indexType].Value;

        
        return null;
    }

}
