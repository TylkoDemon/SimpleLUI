﻿//
// SimpleLUI Source
//
// Copyright (c) 2019 ADAM MAJCHEREK ALL RIGHTS RESERVED
//

using SimpleLUI.API.Util;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SimpleLUI.Editor
{
    internal static class SLUIEngineToScriptConverter
    {
        internal static void Convert(Canvas c, string f, bool prettyPrint)
        {
            EditorUtility.DisplayProgressBar($"Converting UI to LUA ({c.name})", "Clearing.", 5f);

            // clear cache
            varCache.Clear();

            var fullDir = Path.GetDirectoryName(f);
            var fullRes = $"{fullDir}\\auto_res";
            //fullRes = fullRes.Remove(0, Environment.CurrentDirectory.Length + 1);
            var res = $"auto_res";
            
            if (Directory.Exists(fullRes))
            {
                Directory.Delete(fullRes, true);
            }

            Directory.CreateDirectory(fullRes);

            EditorUtility.DisplayProgressBar($"Converting UI to LUA ({c.name})", "Preparing.", 15f);

            var builder = new SLUILuaBuilder(prettyPrint, res, fullRes);

            // write allowed namespaces
            foreach (var lib in SLUIManager.AllowedNamespaces)
            {
                builder.Import(lib.Key, lib.Value);
            }

            builder.Space();
            builder.String.AppendLine("-- Auto Generated by SLUI 'Unity's UI to Lua Converter'");
            builder.String.AppendLine($"-- Time {DateTime.Now:G}");

            // collect supported components
            var allComponents = c.GetComponentsInChildren<Component>(true);
            var supportedComponents = new List<Component>();
            foreach (var d in allComponents)
            {
                if (d == null || d.transform == null || d.transform.parent == null)
                    continue; // ignore root canvas
                var t = d.GetType();
                if (t == typeof(SLUIUnityEventHelper) || t == typeof(CanvasRenderer))
                    continue;

                if (builder.CheckForSupport(t))
                    supportedComponents.Add(d);
                else
                {
                    Debug.LogWarning($"Type {t} is not supported by the {nameof(SLUILuaBuilder)} and will be ignored!", d);
                }
            }

            EditorUtility.DisplayProgressBar($"Converting UI to LUA ({c.name})", "Generating. (1/5)", 45f);

            // write all objects
            builder.Space("![Definition]");
            foreach (var d in supportedComponents)
            {
                builder.AppendDefinition(d);
            }

            EditorUtility.DisplayProgressBar($"Converting UI to LUA ({c.name})", "Generating. (2/5)", 60f);

            builder.Space("![DefinitionExtras]");
            foreach (var d in supportedComponents)
            {
                builder.AppendDefinitionExtras(d);
            }

            EditorUtility.DisplayProgressBar($"Converting UI to LUA ({c.name})", "Generating. (3/5)", 75f);

            builder.Space("![Property]");
            foreach (var d in supportedComponents)
            {
                builder.AppendProperty(d);
            }

            EditorUtility.DisplayProgressBar($"Converting UI to LUA ({c.name})", "Generating. (4/5)", 85f);

            builder.Space("![Extras]");
            foreach (var d in supportedComponents)
            {
                builder.AppendExtras(d);
            }

            EditorUtility.DisplayProgressBar($"Converting UI to LUA ({c.name})", "Finishing. (5/5)", 100f);

            // save the file
            File.WriteAllText(f, builder.ToString());
            Debug.Log($"LUA script saved at " + f + $" (total of {supportedComponents.Count}/{allComponents.Length})");
            EditorUtility.ClearProgressBar();
        }

        private static void CheckAndAddVar(string var, UnityEngine.Object o)
        {
            if (varCache.Contains(var))
            {
                throw new InvalidOperationException($"var of name {var} already exists. Please, rename {o.name}.");
            }

            varCache.Add(var);
        }

        private static List<string> varCache { get; } = new List<string>();
    }
}
