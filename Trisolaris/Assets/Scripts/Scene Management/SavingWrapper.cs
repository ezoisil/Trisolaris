using System;
using System.Collections;
using System.Collections.Generic;
using Trisolaris.Saving;
using UnityEngine;

namespace Trisolaris.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Save()
        {
            // call to the saving system
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            // call to the saving system
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}
