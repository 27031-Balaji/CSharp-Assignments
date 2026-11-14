using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace SerializationAPI
{
    /// <summary>
    /// A serializer class using Reflection.Emit.
    /// </summary>
    public class EmitSerializer
    {
        /// <summary>
        /// Creates a serializer using Reflection.Emit.
        /// </summary>
        /// <param name="type">The type to be serialized.</param>
        /// <returns>The serialized function that can be reused again and again.</returns>
        public Func<object, string> CreateSerializer(Type type)
        {
            DynamicMethod method = new DynamicMethod("Serialize", typeof(string), new[] { typeof(object) });
            ILGenerator il = method.GetILGenerator();
            LocalBuilder stringBuilder = il.DeclareLocal(typeof(StringBuilder));

            il.Emit(OpCodes.Newobj, typeof(StringBuilder).GetConstructor(Type.EmptyTypes) !);
            il.Emit(OpCodes.Stloc_0);

            foreach (PropertyInfo property in type.GetProperties())
            {
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldstr, $"{property.Name}: ");
                il.Emit(OpCodes.Callvirt, typeof(StringBuilder).GetMethod(nameof(StringBuilder.Append), new[] { typeof(string) }) !);
                il.Emit(OpCodes.Pop);

                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Castclass, type);
                il.Emit(OpCodes.Callvirt, property.GetMethod!);

                if (property.PropertyType.IsValueType)
                {
                    il.Emit(OpCodes.Box, property.PropertyType);
                }

                il.Emit(OpCodes.Callvirt, typeof(StringBuilder).GetMethod(nameof(StringBuilder.Append), new[] { typeof(object) }) !);
                il.Emit(OpCodes.Pop);
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldstr, "; ");

                il.Emit(OpCodes.Callvirt, typeof(StringBuilder).GetMethod(nameof(StringBuilder.Append), new[] { typeof(string) }) !);
                il.Emit(OpCodes.Pop);
            }

            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Callvirt, typeof(StringBuilder).GetMethod(nameof(StringBuilder.ToString), Type.EmptyTypes) !);
            il.Emit(OpCodes.Ret);
            return (Func<object, string>)method.CreateDelegate(typeof(Func<object, string>));
        }
    }
}