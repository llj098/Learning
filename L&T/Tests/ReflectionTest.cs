using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;
using Common;
namespace Tests
{
    class ReflectionTest
    {
        public delegate void ActionProc<A0, A1>(A0 arg0, A1 arg1);
        private static void SetField<T>(ClassWithPrivateFiled obj,T value)
        {
            Type type = typeof(ClassWithPrivateFiled);
            var fi = type.GetField("_num", BindingFlags.NonPublic | BindingFlags.IgnoreCase | BindingFlags.Instance);
            fi.SetValue(obj,value);
        }

        static ActionProc<ClassWithPrivateFiled, int> Method_SetEndpoint;
        public static ActionProc<ClassWithPrivateFiled, int> GetSetEndpointMethod()
        {
            if (Method_SetEndpoint == null) {
                FieldInfo m_RightEndPoint = typeof(ClassWithPrivateFiled).GetField("_num", BindingFlags.Instance | BindingFlags.NonPublic);

                DynamicMethod method = new DynamicMethod("SetEndpoint", typeof(void), new Type[] { typeof(ClassWithPrivateFiled), typeof(int) }, typeof(ReflectionTest), true);

                ILGenerator il = method.GetILGenerator();
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Ldarg_1);
                il.Emit(OpCodes.Stfld, m_RightEndPoint);
                il.Emit(OpCodes.Ret);

                Method_SetEndpoint = (ActionProc<ClassWithPrivateFiled, int>)method.CreateDelegate(typeof(ActionProc<ClassWithPrivateFiled, int>));
            }
            return Method_SetEndpoint;
        }

        public static void Go()
        {
            ClassWithPrivateFiled c = new ClassWithPrivateFiled(10);
            var m = GetSetEndpointMethod();
            
            CodeTimer.Initialize();
            CodeTimer.Time("reflection", 100000, () =>
                {
                    SetField<int>(c, 20);
                }
            );

            CodeTimer.Time("IL", 100000, () =>
            {
                m(c, 20);
            });
            //Console.WriteLine("The old value is {0}", c.GetNum());
            //SetField<int>(c, 20);
            Console.WriteLine("The new value is {0}", c.GetNum());
        }
    }

    class ClassWithPrivateFiled
    {
        private int _num;

        public ClassWithPrivateFiled(int num)
        {
            _num = num;
        }

        public int GetNum()
        {
            return _num;
        }
    }
}
