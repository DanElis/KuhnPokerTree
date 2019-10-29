using System.Collections.Generic;

namespace KuhnPoker
{
    public class Tree
    {
        private readonly StateNode _root;
        private StateNode _currentNode;
        public Tree()
        {
            var callNodeL3 = new StateNode(null, null, null, null, 2, true, -1);
            var foldNodeL3 = new StateNode(null, null, null, null, 2, true, 2);
            var betNodeL2 = new StateNode(callNodeL3, null, foldNodeL3, null, 1, false, -1);
            var checkNodeL2 = new StateNode(null, null, null, null, 1, true, -1);
            var checkNodeL1 = new StateNode(null, betNodeL2, null, checkNodeL2, 2, false, -1);
            
            var foldNodeR2 = new StateNode(null, null, null, null, 1, true, 1 );
            var callNodeR2 = new StateNode(null, null, null, null, 1, true, -1);
            var betNodeR1 = new StateNode(callNodeR2, null, foldNodeR2, null, 2, false, -1);

            _root = new StateNode(null, betNodeR1, null, checkNodeL1, 1, false, -1);
            _currentNode = _root;
        }

        public List<string> GetActions()
        {
            return _currentNode.GetAction();
        }

        public int PerfomAction(string nameAction)
        {
            var ret = -1;
            var node = _currentNode.GetNode(nameAction);
            if (node != null)
            {
                _currentNode = node;
                ret = 0;
            }

            return ret;
        }

        public bool IsEndGame()
        {
            return _currentNode.IsEndGame();
        }

        public int GetPlayerNumber()
        {
            return _currentNode.GetPlayerNumber();
        }

        public int FoldPlayer()
        {
            return _currentNode.FoldPlayer();
        }

        public void NewGame()
        {
            _currentNode = _root;
        }
    }
}