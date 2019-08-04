﻿//
// SimpleLUI Source
//
// Copyright (c) 2019 ADAM MAJCHEREK ALL RIGHTS RESERVED
//

using JEM.UnityEngine.Interface;
using SimpleLUI.Editor.API;
using SimpleLUI.Editor.Util;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SimpleLUI.JEM.Editor
{
    [SLUIBuilderObject]
    public class SLUIJEMFadeElementEditor : SLUIBuilderObject
    {
        public SLUIJEMFadeElementEditor() : base(typeof(InterfaceFadeElement)) { }

        public override void CollectObjectDefinition(Object obj)
        {
            var i = (InterfaceFadeElement) obj;
            var name = SLUILuaBuilderSyntax.CollectVar(i);
            var parentName = SLUILuaBuilderSyntax.CollectVar(i.GetComponent<RectTransform>());

            String.AppendLine($"local {name} = {parentName}:AddComponent('SLUIJEMFadeElement')");
        }

        public override void CollectObjectProperty(Object obj)
        {
            
        }
    }
}
