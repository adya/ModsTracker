using SMT.Actions;
using SMT.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMT.Managers
{
    public class ActionsManager
    {

        private const int NO_ACTIONS = -1;
        private const int DEFAULT_CAPACITY = 100;

        private CycledList<IAction> actions;

        public int CurrentActionIndex { get; private set; }
        private int capacity;

        public int Capacity
        {
            get { return capacity; }
            set { capacity = value; actions.Extend(capacity); }
        }

        public ActionsManager(int capacity)
        {
            this.capacity = capacity;
            CurrentActionIndex = NO_ACTIONS;
            actions = new CycledList<IAction>(capacity);
        }
        public ActionsManager() : this(DEFAULT_CAPACITY) { }

        public int PerformedActionsCount { get { return actions.Count; } }

        private bool HasRevertedActions { get { return CurrentActionIndex < actions.Count - 1; } }

        public bool IsRevertAvailable {  get { return CurrentActionIndex != NO_ACTIONS; } }

        public bool IsPerformAvailable { get { return HasRevertedActions; } }

        public void PerformAction(IAction action)
        {
            if (action == null) return;
            action.Perform();
            if (HasRevertedActions)
            {
                actions.RemoveRange(CurrentActionIndex + 1, actions.Count - CurrentActionIndex);
                CurrentActionIndex = actions.Count - 1;
            }
            if (actions.Add(action))
                ++CurrentActionIndex;
        }

        public void PerformLastAction()
        {
            if (HasRevertedActions)
                actions[(++CurrentActionIndex)].Perform();
        }

        public void RevertLastAction()
        {
            if (CurrentActionIndex == NO_ACTIONS) return;
            actions[CurrentActionIndex--].Revert();
        }

        public void RevertLastActions(int numberOfActions)
        {
            if (numberOfActions < 0)
                numberOfActions = actions.Count;
            while (numberOfActions-- > 0 && CurrentActionIndex != NO_ACTIONS)
                RevertLastAction();
        }
        public string[] getHistory()
        {
            string[] history = new string[actions.Count];
            for (int i = 0; i < history.Length; i++)
            {
                history[i] = actions[i].Name;
            }
            return history;
        }

        public void Clear()
        {
            actions.Clear();
            CurrentActionIndex = NO_ACTIONS;
        }
    }
}
