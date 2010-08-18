using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Reflection.Emit;

namespace SocketTest
{
    class SocketPool
    {
        private Queue<Socket> _socketsQueue;
        private object _syncRoot;

        public SocketPool(int capacity)
        {
            _socketsQueue = new Queue<Socket>(capacity);
            _syncRoot = new object();
        }

        public Socket FetchOne()
        {
            Socket ret = null;
            lock (_syncRoot)
                ret = _socketsQueue.Dequeue();

            return ret;
        }

        public void Collect(Socket socket)
        {
            var m = GetEraAction();
            m(socket, null);
            socket.Shutdown(SocketShutdown.Both);

            lock (_syncRoot) {
                _socketsQueue.Enqueue(socket);
            }
        }

        public delegate void ActionP<A0, A1>(A0 a0, A1 a1);
        private ActionP<Socket, object> EraAction;
        public ActionP<Socket,object> GetEraAction()
        {
            if (EraAction == null) {
                string fieldName = "m_RightEndPoint";
                var field = typeof(Socket).GetField(fieldName);

                DynamicMethod dynamicMethod = new DynamicMethod("SetEndPoint",
                    typeof(void),
                    new Type[] { typeof(Socket), typeof(object) },
                    true);

                var ilGenerator = dynamicMethod.GetILGenerator();
                ilGenerator.Emit(OpCodes.Ldarg_0);
                ilGenerator.Emit(OpCodes.Ldarg_1);
                ilGenerator.Emit(OpCodes.Stfld, field);
                ilGenerator.Emit(OpCodes.Ret);

                EraAction = (ActionP<Socket, Object>)dynamicMethod.CreateDelegate(
                        typeof(ActionP<Socket, object>)
                    );
            }

            return EraAction;
        }
    }
}
