/*
  	Copyright 2017 Gustavo Cabrera
    Licensed under the Apache License, Version 2.0 (the "License");
 	you may not use this file except in compliance with the License.
 	You may obtain a copy of the License at

		http://www.apache.org/licenses/LICENSE-2.0

	Unless required by applicable law or agreed to in writing, software
	distributed under the License is distributed on an "AS IS" BASIS,
	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	See the License for the specific language governing permissions and
	limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    internal static class ReflectionUtils
    {
        /// <summary>
        /// Filtra los miembros pasados por parámetros segun tengan o no el attributo especificado y los relaciona
        /// en un diccionario donde las claves son las instancias de los atributos y los valores son los miembros que las definen.
        /// </summary>
        /// <typeparam name="AttributeType">Tipo de atributo que debe contener cada miembro</typeparam>
        /// <typeparam name="MemberType">Tipo de miembro (ej. property, method, etc)</typeparam>
        /// <param name="members">Lista de miembros a filtrar y relacionar</param>
        public static IList<AttributeMemberPair<AttributeType, MemberType>> AttributeMemberList<AttributeType, MemberType>(MemberType[] members) where MemberType : MemberInfo
        {
            var result = new List<AttributeMemberPair<AttributeType, MemberType>>();

            foreach (var member in members)
            {
                var attributes = member.GetCustomAttributes(typeof(AttributeType), true);
                if (attributes.Length > 0)
                {
                    var pair = new AttributeMemberPair<AttributeType, MemberType>
                    {
                        Attribute = (AttributeType)attributes.First(),
                        MemberInfo = member
                    };
                    
                    result.Add(pair);
                }
            }

            return result;
        }
    }
}
