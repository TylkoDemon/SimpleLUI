﻿//
// JEM For Unity Source
//
// Copyright (c) 2019 ADAM MAJCHEREK ALL RIGHTS RESERVED
//

using UnityEngine;

namespace JEM.UnityEngine.Interface
{
    /// <inheritdoc />
    /// <summary>
    ///     Base element of every locale based interface element.
    /// </summary>
    public abstract class JEMInterfaceLocaleElement : MonoBehaviour
    {
        private void OnEnable()
        {
            // Always refresh locale on enable...
            RefreshLocale();
        }

        /// <summary>
        ///     Refresh text of this element from locale.
        /// </summary>
        public abstract void RefreshLocale();

        /// <summary>
        ///     Refresh all active locale elements in scene.
        /// </summary>
        public static void RefreshAll()
        {
            var elements = FindObjectsOfType<JEMInterfaceLocaleElement>();
            foreach (var e in elements)
            {
                e.RefreshLocale();
            }
        }
    }
}
