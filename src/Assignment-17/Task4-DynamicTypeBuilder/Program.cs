using System.Reflection;
using System.Reflection.Emit;

namespace DynamicTypeBuilder
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The method that executes when the application is run.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            AssemblyName assemblyName = new AssemblyName("EmployeeAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("EmployeeModule");

            TypeBuilder typeBuilder = moduleBuilder.DefineType("Employee", TypeAttributes.Public);
            FieldBuilder nameField = typeBuilder.DefineField("_name", typeof(string), FieldAttributes.Private);
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty("Name", PropertyAttributes.None, typeof(string), null);

            MethodBuilder getMethodBuilder = typeBuilder.DefineMethod("get_Name", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, typeof(string), Type.EmptyTypes);
            ILGenerator getIL = getMethodBuilder.GetILGenerator();
            getIL.Emit(OpCodes.Ldarg_0);
            getIL.Emit(OpCodes.Ldfld, nameField);
            getIL.Emit(OpCodes.Ret);

            MethodBuilder setMethodBuilder = typeBuilder.DefineMethod("set_Name", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new Type[] { typeof(string) });
            ILGenerator setIL = setMethodBuilder.GetILGenerator();
            setIL.Emit(OpCodes.Ldarg_0);
            setIL.Emit(OpCodes.Ldarg_1);
            setIL.Emit(OpCodes.Stfld, nameField);
            setIL.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getMethodBuilder);
            propertyBuilder.SetSetMethod(setMethodBuilder);

            MethodBuilder displayMethod = typeBuilder.DefineMethod("Display", MethodAttributes.Public, typeof(void), Type.EmptyTypes);
            ILGenerator methodIL = displayMethod.GetILGenerator();
            methodIL.Emit(OpCodes.Ldstr, "Employee Display Method Invoked");
            methodIL.Emit(OpCodes.Call, typeof(Console).GetMethod(nameof(Console.WriteLine), new Type[] { typeof(string) }) !);
            methodIL.Emit(OpCodes.Ret);

            Type employeeType = typeBuilder.CreateType() !;
            Console.WriteLine($"Assembly: {assemblyBuilder.GetName().Name}");
            Console.WriteLine($"Module: {moduleBuilder.ScopeName}");
            Console.WriteLine($"Type: {employeeType.Name}");

            object employee = Activator.CreateInstance(employeeType) !;

            PropertyInfo property = employeeType.GetProperty("Name") !;
            property.SetValue(employee, "Dinesh");
            Console.WriteLine($"Property Value: {property.GetValue(employee)}");
            MethodInfo method = employeeType.GetMethod("Display") !;

            method.Invoke(employee, null);
            Console.ReadKey();
        }
    }
}