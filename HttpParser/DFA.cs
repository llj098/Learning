using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpParser
{
    public abstract class DFA<Token,Function>
    {
        protected static DFAState<Token, Function>[] States;
        protected int OldState;
        protected int EOFAction = 0;
        protected int CurrentState = 0;
        public Token CurrentToken;
        protected static bool Inited = false;
        protected static object InitObject = new object();

        public DFAResult InputToken(int action, Token token)
        {
            if (!Inited) {
                Init();
            }

            if (States.Length == 0) {
                return DFAResult.Continue;
            }

            CurrentToken = token;
            OldState = CurrentState;

            bool isElse;
            CurrentState = States[CurrentState].NextState(action, out isElse);

            if (CurrentState < 0) {
                CurrentState = 0;
                throw new DFAException("Invalid next state");
            }

            if (CurrentState > States.Length) {
                throw new DFAException("Bad DFA State");
            }

            if (!States[CurrentState].NoFunction) {
                States[CurrentState].HandleState(action, this);
            }

            if (States[CurrentState].IsEndState) {
                return DFAResult.End;
            }
            else if (States[CurrentState].IsQuitState) {
                if (isElse) {
                    //CurrentState = 0;
                    return DFAResult.ElseQuit;
                }
                return DFAResult.Quit;
            }
            else {
                return DFAResult.Continue;
            }
        }

        public static DFAState<Token,Function> AddState(DFAState<Token, Function> state)
        {
            if (States == null)
                States = new DFAState<Token, Function>[state.ID + 1];

            if (state.ID >= States.Length) {
                int newLength;

                if (state.ID < 2 * States.Length) {
                    newLength = 2 * States.Length;
                }
                else {
                    newLength = state.ID + 1;
                }

                var old = States;
                States = new DFAState<Token, Function>[newLength];
                Array.Copy(old, States, old.Length);
            }

            if (States[state.ID] != null) {
                throw new DFAException("The id has already been in the DFA!");
            }

            States[state.ID] = state;

            return state;
        }

        protected abstract void Init();
    }

    public class DFAException : Exception
    {
        public DFAException() : base() { }

        public DFAException(string msg) : base(msg) { }
    }

    public enum DFAResult
    {
        Continue = 0,
        Quit = 1,
        ElseQuit = 2,
        End = 3,
    }

	public abstract class DFAState<Token,Function>
    {
        public int ID;
        public bool IsQuitState { get; set; }
        public bool IsEndState { get; set; }
        protected IDictionary<int,int> NextStates;
		protected Function Func;
		public bool NoFunction;
        public int ElseStateId = -1;

        public void AddState(int beginAction, int endAction, int nextAction)
        {
            for (int i = beginAction; i < endAction; i++) {
                AddState(i, nextAction);
            }
        }

        public void AddState(int action, int nextAction)
        {
            int nAction;
            if (NextStates.TryGetValue(action, out nAction)) {
                throw new DFAException("Bad State, the action is already in the DFA");
            }

            NextStates[action] = nextAction;
        }


        public void AddState(int[] actions,int nextAction)
        {
            foreach (var item in actions) {
                AddState(item, nextAction);
            }
        }

        public void AddElseState(int id)
        {
            ElseStateId = id;
        }

        public int NextState(int action,out bool isElseAction)
        {
            int ret = -1;

            if (NextStates.TryGetValue(action, out ret)) {
                isElseAction = false;
            }
            else {
                isElseAction = true;
                ret = ElseStateId;
            }

            return ret;
        }

        public abstract void HandleState(int action,DFA<Token,Function> dfa);
    }
}
