// Copyright 2014 Konstantin Alexandroff
//   
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
//  
//      http://www.apache.org/licenses/LICENSE-2.0 
//  
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.

namespace Kostassoid.Liar.Tools
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;

	internal static class ObjectExtensions
	{
		static readonly MethodInfo CloneMethod = typeof (Object).GetMethod("MemberwiseClone",
			BindingFlags.NonPublic | BindingFlags.Instance);

		public static bool IsPrimitive(this Type type)
		{
			if (type == typeof (String)) return true;
			return (type.IsValueType & type.IsPrimitive);
		}

		public static Object Copy(this Object originalObject)
		{
			return InternalCopy(originalObject, new Dictionary<Object, Object>(new ReferenceEqualityComparer()));
		}

		static Object InternalCopy(Object originalObject, IDictionary<Object, Object> visited)
		{
			if (originalObject == null) return null;
			var typeToReflect = originalObject.GetType();
			if (IsPrimitive(typeToReflect)) return originalObject;
			if (visited.ContainsKey(originalObject)) return visited[originalObject];
			if (typeof (Delegate).IsAssignableFrom(typeToReflect)) return null;
			var cloneObject = CloneMethod.Invoke(originalObject, null);
			if (typeToReflect.IsArray)
			{
				var arrayType = typeToReflect.GetElementType();
				if (IsPrimitive(arrayType) == false)
				{
					var clonedArray = (Array) cloneObject;
					clonedArray.ForEach(
						(array, indices) => array.SetValue(InternalCopy(clonedArray.GetValue(indices), visited), indices));
				}
			}
			visited.Add(originalObject, cloneObject);
			CopyFields(originalObject, visited, cloneObject, typeToReflect);
			RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect);
			return cloneObject;
		}

		static void RecursiveCopyBaseTypePrivateFields(object originalObject, IDictionary<object, object> visited,
			object cloneObject, Type typeToReflect)
		{
			if (typeToReflect.BaseType != null)
			{
				RecursiveCopyBaseTypePrivateFields(originalObject, visited, cloneObject, typeToReflect.BaseType);
				CopyFields(originalObject, visited, cloneObject, typeToReflect.BaseType,
					BindingFlags.Instance | BindingFlags.NonPublic, info => info.IsPrivate);
			}
		}

		static void CopyFields(object originalObject, IDictionary<object, object> visited, object cloneObject,
			Type typeToReflect,
			BindingFlags bindingFlags =
				BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.FlattenHierarchy,
			Func<FieldInfo, bool> filter = null)
		{
			foreach (FieldInfo fieldInfo in typeToReflect.GetFields(bindingFlags))
			{
				if (filter != null && filter(fieldInfo) == false) continue;
				if (IsPrimitive(fieldInfo.FieldType)) continue;
				var originalFieldValue = fieldInfo.GetValue(originalObject);
				var clonedFieldValue = InternalCopy(originalFieldValue, visited);
				fieldInfo.SetValue(cloneObject, clonedFieldValue);
			}
		}

		public static T Copy<T>(this T original)
		{
			return (T) Copy((Object) original);
		}
	}
}