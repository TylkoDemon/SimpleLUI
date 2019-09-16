﻿//
// JEM For Unity Source
//
// Copyright (c) 2019 ADAM MAJCHEREK ALL RIGHTS RESERVED
//

using System;
using System.Collections;
using UnityEngine;

namespace JEM.UnityEngine.Extension.Internal
{
    /// <inheritdoc />
    /// <summary />
    internal class JEMExtensionGameObjectScript : MonoBehaviour
    {
        /// <summary/>
        internal IEnumerator InternalSetActiveEasy(GameObject go, bool activeState, Action onDone)
        {
            if (go.activeSelf == activeState)
            {
                onDone?.Invoke();
                yield break;
            }

            go.SetActive(activeState);
            yield return new WaitForEndOfFrame();
            onDone?.Invoke();
        }
    }
}