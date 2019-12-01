﻿//
// JEM For Unity Source
//
// Copyright (c) 2019 ADAM MAJCHEREK ALL RIGHTS RESERVED
//

// TODO: JEMBetterEditor need major refactor work.

using System;
using UnityEngine;

namespace JEM.UnityEditor
{
#pragma warning disable 1591
    public struct JEMBetterEditorEvents<T>
    {
        public Action<T> OnPreDraw;
        public Func<T, T> OnDraw;
        public Action<T> OnPostDraw;
        public Action OnAdd;
        public Action<T> OnRemove;
        public Action<Rect> OnDrawMenu;

        public JEMBetterEditorEvents(Func<T, T> onDraw)
        {
            OnPreDraw = null;
            OnDraw = onDraw;
            OnPostDraw = null;
            OnAdd = null;
            OnRemove = null;
            OnDrawMenu = null;
        }
    }
#pragma warning restore 1591
}