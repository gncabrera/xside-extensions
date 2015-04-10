using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Extensions
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Retorna las propiedades y sus atributos para poder utilizarlos relacionados uno a uno.
        /// Si una property tiene más de un attribute del tipo especificado, utilizará el primero.
        /// </summary>
        /// <typeparam name="Type">Tipo en el cual se inspeccionarán las properties que 
        /// contengan el attribute AttributeType.</typeparam>
        /// <typeparam name="AttributeType">Tipo de attribute de las properties que serán seleccionadas.</typeparam>
        /// <returns>Diccionario que tiene como claves las instancias de los atributos de cada
        /// property; y como valor la property con la que se corresponde.</returns>
        public static IList<AttributeMemberPair<AttributeType, PropertyInfo>> InspectProperties<AttributeType>(this Type type)
        {
            var properties = type.GetProperties();
            return ReflectionUtils.AttributeMemberList<AttributeType, PropertyInfo>(properties);
        }

        /// <summary>
        /// Retorna los metodos y sus atributos para poder utilizarlos relacionados uno a uno.
        /// Si un metodo tiene más de un attribute del tipo especificado, utilizará el primero.
        /// </summary>
        /// <typeparam name="Type">Tipo en el cual se inspeccionarán los metodos que 
        /// contengan el attribute AttributeType.</typeparam>
        /// <typeparam name="AttributeType">Tipo de attribute de los metodos que serán seleccionados.</typeparam>
        /// <returns>Diccionario que tiene como claves las instancias de los atributos de cada
        /// metodo; y como valor el metodo con el que se corresponde.</returns>
        public static IList<AttributeMemberPair<AttributeType, MethodInfo>> InspectMethods<AttributeType>(this Type type)
        {
            var methods = type.GetMethods();
            return ReflectionUtils.AttributeMemberList<AttributeType, MethodInfo>(methods);
        }

        /// <summary>
        /// Retorna la primer property que tenga el atributo especificado.
        /// </summary>
        /// <typeparam name="AttributeType">Tipo de Attribute a buscar</typeparam>
        /// <param name="type">Tipo en el cual se buscará el property</param>
        /// <exception cref="PropertyNotFound">Si no se encuentra ninguna property con ese Attribute.</exception>
        public static PropertyInfo GetAnnotatedProperty<AttributeType>(this Type type)
        {
            try
            {
                var properties = type.GetProperties();
                return properties.First(property => Attribute.IsDefined(property, typeof(AttributeType)));
            }
            catch (InvalidOperationException e)
            {
                throw new PropertyNotFoundException("No se pudo encontrar ninguna property en el tipo \"" + type.Name + "\" anotada con \"" + typeof(AttributeType).Name + "\"", e);
            }
        }

        /// <summary>
        /// Retorna el tipo de attribute específico, asociado al método.
        /// </summary>
        /// <typeparam name="AttributeType">Tipo de attribute a buscar</typeparam>
        /// <param name="type">Tipo en el cual se buscará el método y su attribute</param>
        /// <param name="methodName">Nombre del método donde se buscará el attribute</param>
        /// <returns>Attribute del método</returns>
        /// <exception cref="MethodNotFoundException">Cuando no se encuentra el método.</exception>
        /// <exception cref="AttributeNotFoundException">Cuando no se encuentra el attribute en el método.</exception>
        public static AttributeType GetMethodAttribute<AttributeType>(this Type type, string methodName) where AttributeType : Attribute
        {
            var method = type.GetMethod(methodName);
            if (method == null)
            {
                throw new MethodNotFoundException("No se pudo encontrar el método " + methodName + " en la clase " + type.Name + ".");
            }
            var attribute = method.GetCustomAttribute<AttributeType>();
            if (attribute == null)
            {
                throw new AttributeNotFoundException("El attribute " + typeof(AttributeType).Name + " no pudo encontrarse para el método " + methodName + " de la clase " + type.Name + ".");
            }
            return attribute;
        }

        /// <summary>
        /// Busca los attribute que anotan al tipo a nivel class y retorna el primero del tipo de attribute especificado
        /// </summary>
        /// <typeparam name="AttributeType">Tipo de Attribute a buscar</typeparam>
        /// <param name="type">Tipo en el cual se buscará el property</param>
        public static AttributeType GetAttribute<AttributeType>(this Type type) where AttributeType : Attribute
        {
            var attr = type.GetCustomAttributes(typeof(AttributeType)).FirstOrDefault() as AttributeType;
            if (attr == null)
                throw new AttributeNotFoundException("El attribute " + typeof(AttributeType).Name + " no pudo encontrarse en la clase " + type.Name + ".");
            return attr;
        }

        public static AttributeType GetAttribute<AttributeType, TOut, TIn>(this Expression<Func<TOut, TIn>> expression) where AttributeType : Attribute
        {
            var attrType = typeof(AttributeType);
            var property = expression.GetProperty();
            return property.GetCustomAttribute(attrType) as AttributeType;
        }


        public static AttributeType GetClassAttribute<AttributeType, TOut, TIn>(this Expression<Func<TOut, TIn>> expression) where AttributeType : Attribute
        {
            var attrType = typeof(AttributeType);
            var property = expression.GetProperty();
            return property.ReflectedType.GetCustomAttribute<AttributeType>() as AttributeType;
        }

        /// <summary>
        /// Devuelve un PropertyInfo a partir del retorno de una expression con un parametro de entrada
        /// </summary>
        /// <typeparam name="TOut">El tipo out de la expression</typeparam>
        /// <typeparam name="TIn">El tipo in de la expression</typeparam>
        /// <param name="expression">La expression a partir de la cual se buscara el PropertyInfo</param>
        /// <returns>El PropertyInfo al que esta referenciando la expression</returns>
        public static MemberInfo GetProperty<TOut, TIn>(this Expression<Func<TOut, TIn>> expression)
        {
            MemberExpression Exp;

            //this line is necessary, because sometimes the expression comes as Convert(originalexpression)
            if (expression.Body is UnaryExpression)
            {
                UnaryExpression UnExp = (UnaryExpression)expression.Body;
                if (UnExp.Operand is MemberExpression)
                {
                    Exp = (MemberExpression)UnExp.Operand;
                }
                else if (UnExp.Operand is MethodCallExpression)
                {
                    return ((MethodCallExpression)UnExp.Operand).Method;
                }
                else
                    throw new ArgumentException();
            }
            else if (expression.Body is MemberExpression)
            {
                Exp = (MemberExpression)expression.Body;
            }
            else if (expression.Body is MethodCallExpression)
            {
                return ((MethodCallExpression)expression.Body).Method;
            }
            else
            {
                throw new ArgumentException();
            }

            return Exp.Member;
        }

        public static string GetNameWithoutGenericArity(this Type type)
        {
            string name = type.Name;
            int index = name.IndexOf('`');
            return index == -1 ? name : name.Substring(0, index);
        }

        /// <summary>
        /// Retorna true si el tipo puede ser asignado a una variable de tipo "SuperType"
        /// </summary>
        public static bool SubtypeOf<SuperType>(this Type type)
        {
            return typeof(SuperType).IsAssignableFrom(type);
        }
    }
}
