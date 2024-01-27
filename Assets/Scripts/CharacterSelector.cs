using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{

    public static CharacterSelector instance;
    public CharacterScriptableObject characterData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensure the object persists between scenes
        }
        else
        {
            Debug.LogWarning("More than one CharacterSelector in scene!");
            Destroy(gameObject); // Destroy duplicate object
        }
    }

    public static CharacterScriptableObject GetData()
    {
        return instance.characterData;
    }

    public void SelectCharacter(CharacterScriptableObject chararcter)
    {
        characterData = chararcter;
    }

    public void DestroySingleton()
    {
        instance =null;
        Destroy(gameObject);
    }
}
