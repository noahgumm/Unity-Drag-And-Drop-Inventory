/*
 * Noah Gumm
 * 06/19/2025
 * 
 * Description: Simple base item class for the inventory system. Includes a name, id, and GameObject prefab
 */
using UnityEngine;

namespace InventoryNamespace
{
    public class Item
    {
        private int _id;
        private string _name;
        public int Id { get { return _id; } }
        public string Name { get { return _name; } }
        public GameObject Prefab { get; private set; }

        public Item(int id, string name)
        {
            _id = id;
            _name = name;
        }

        // Loads item prefabs based on their path
        // Objects must be located within the Resources folder
        private GameObject LoadPrefabByPathName(string path)
        {
            // Builds the path: "BuildPrefabs/Wall/Wall"
            GameObject prefab = Resources.Load<GameObject>(path);

            if (prefab == null)
            {
                Debug.LogError($"Prefab not found at path: Resources/{path}");
            }

            return prefab;
        }

        // Initilizes the object
        // Should be called immediately after creating an item
        // Loading resources can't be done in a constructor
        public void InitObject()
        {
            Prefab = LoadPrefabByPathName($"{_name}");
        }

    }
}
