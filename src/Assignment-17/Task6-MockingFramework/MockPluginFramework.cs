using System.Reflection;
using System.Reflection.Emit;
using MockingFramework.Interface;

namespace MockingFramework.Class
{
    /// <summary>
    /// Framework used to create a mock plugin.
    /// </summary>
    public class MockPluginFramework
    {
        /// <summary>
        /// Creates a mock plugin for testing.
        /// </summary>
        /// <returns>An <see cref="IPlugin"/> mock object.</returns>
        public IPlugin CreateMockPlugin()
        {
            AssemblyName assemblyName = new AssemblyName("MockAssembly");
            AssemblyBuilder assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MockModule");
            TypeBuilder typeBuilder = moduleBuilder.DefineType("MockPlugin", TypeAttributes.Public);
            typeBuilder.AddInterfaceImplementation(typeof(IPlugin));

            this.DefinePluginNameProperty(typeBuilder);
            this.DefineEncryptMethod(typeBuilder);
            this.DefineDecryptMethod(typeBuilder);

            Type mockType = typeBuilder.CreateType() !;
            IPlugin mockInterface = (IPlugin)Activator.CreateInstance(mockType) !;
            return mockInterface;
        }

        /// <summary>
        /// Defines the plugin name of the mock plugin.
        /// </summary>
        /// <param name="typeBuilder">The type of the mock plugin used to define and create instances of classes.</param>
        private void DefinePluginNameProperty(TypeBuilder typeBuilder)
        {
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty("PluginName", PropertyAttributes.None, typeof(string), null);
            MethodInfo interfaceGetter = typeof(IPlugin)
                .GetProperty(nameof(IPlugin.PluginName)) !
                .GetGetMethod() !;

            MethodBuilder getter = typeBuilder.DefineMethod(
                    "get_name",
                    MethodAttributes.Public | MethodAttributes.Virtual,
                    typeof(string),
                    Type.EmptyTypes);

            ILGenerator il = getter.GetILGenerator();
            il.Emit(OpCodes.Ldstr, "Mock Plugin");
            il.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(getter);
            typeBuilder.DefineMethodOverride(getter, interfaceGetter);
        }

        /// <summary>
        /// Defines the encryption method in the interface for the mock plugin.
        /// </summary>
        /// <param name="typeBuilder">The type of the mock plugin used to define and create instances of classes.</param>
        private void DefineEncryptMethod(TypeBuilder typeBuilder)
        {
            MethodInfo interfaceMethod = typeof(IPlugin).GetMethod(nameof(IPlugin.Encrypt)) !;
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                    interfaceMethod.Name,
                    MethodAttributes.Public | MethodAttributes.Virtual,
                    typeof(string),
                    new[] { typeof(string) });

            ILGenerator il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldstr, "Mock Encrypted");
            il.Emit(OpCodes.Ret);

            typeBuilder.DefineMethodOverride(methodBuilder, interfaceMethod);
        }

        /// <summary>
        /// Defines the decryption method in the interface for the mock plugin.
        /// </summary>
        /// <param name="typeBuilder">The type of the mock plugin used to define and create instances of classes.</param>
        private void DefineDecryptMethod(TypeBuilder typeBuilder)
        {
            MethodInfo interfaceMethod = typeof(IPlugin).GetMethod(nameof(IPlugin.Decrypt)) !;
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(
                    interfaceMethod.Name,
                    MethodAttributes.Public | MethodAttributes.Virtual,
                    typeof(string),
                    new[] { typeof(string) });

            ILGenerator il = methodBuilder.GetILGenerator();
            il.Emit(OpCodes.Ldstr, "Mock Decrypted");
            il.Emit(OpCodes.Ret);

            typeBuilder.DefineMethodOverride(methodBuilder, interfaceMethod);
        }
    }
}