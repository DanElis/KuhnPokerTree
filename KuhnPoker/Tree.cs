using System.Collections.Generic;

namespace KuhnPoker
{
    public class Tree
    {
        private readonly StateNode _root;
        private StateNode _currentNode;
        private readonly int _maxDepth;

        public Tree(int maxDepth)
        {
            this._maxDepth = maxDepth;
            _root = BuildTree();
            _currentNode = _root;
        }

        public void PerfomAction(string nameAction)
        {
            var children = _currentNode.GetChildren();
            foreach (var child in children)
            {
                if (child.GetNameNode() == nameAction)
                {
                    _currentNode = child;
                }
            }
        }

        public List<string> GetNamesActions()
        {
            var namesActions = new List<string>();
            var children = _currentNode.GetChildren();
            if (children.Count == 0)
                return namesActions;
            foreach (var child in children)
            {
                namesActions.Add(child.GetNameNode());
            }

            return namesActions;
        }

        public void NewGame()
        {
            _currentNode = _root;
        }

        private StateNode BuildTree()
        {
            var node = BuildRootNode(StateNode.FirstPlayer, 0);
            return node;
        }
        private List<StateNode> BuildChildren(IReadOnlyCollection<string> namesChildren, int playerNumber, int depthBet)
        {
            var children = new List<StateNode>();
            if (namesChildren.Count == 0)
            {
                return children;
            }
            
            foreach (var name in namesChildren)
            {
                /*Console.WriteLine(depthBet);
                Console.WriteLine(name);*/
                StateNode node;
                switch (name)
                {
                    case StateNode.Call:
                        node = BuildCallNode(playerNumber,depthBet);
                        children.Add(node);
                        break;
                    case StateNode.Bet:
                        node = BuildBetNode(playerNumber,depthBet);
                        children.Add(node);
                        break;
                    case StateNode.Fold:
                        node = BuildFoldNode(playerNumber,depthBet);
                        children.Add(node);
                        break;
                    case StateNode.Check:
                        node = BuildCheckNode(playerNumber,depthBet);
                        children.Add(node);
                        break;
                    case StateNode.Root:
                        node = BuildRootNode(playerNumber,depthBet);
                        children.Add(node);
                        break;
                }
            }

            return children;
        }
        
        private StateNode BuildCallNode(int playerNumber, int depthBet)
        {
            var children = BuildChildren(new List<string>(), NextPlayerNum(playerNumber),depthBet);
            var callNode = new StateNode(StateNode.Call, playerNumber,children);
            return callNode;
        }
        private StateNode BuildBetNode(int playerNumber, int depthBet)
        {
            depthBet += 1;
            var namesChildren = new List<string> {StateNode.Call, StateNode.Fold};
            if (_maxDepth - depthBet > 0)
            {
                namesChildren.Add(StateNode.Bet);
            }
            var children = BuildChildren(namesChildren, NextPlayerNum(playerNumber),depthBet);
            var betNode = new StateNode(StateNode.Bet, playerNumber, children);
            return betNode;
        }
    
        private StateNode BuildFoldNode(int playerNumber, int depthBet)
        {
            var children = BuildChildren(new List<string>(), NextPlayerNum(playerNumber),depthBet);
            var foldNode = new StateNode(StateNode.Fold, playerNumber, children);
            return foldNode;
        }

        private StateNode BuildCheckNode(int playerNumber, int depthBet)
        {
            List<string> namesChildren = new List<string>();
            if (playerNumber + 1 != StateNode.NumberPlayers)
            {
                namesChildren.Add(StateNode.Bet);
                namesChildren.Add(StateNode.Check);
            }
            var children = BuildChildren(namesChildren, NextPlayerNum(playerNumber),depthBet);
            var checkNode = new StateNode(StateNode.Check, playerNumber, children);
            return checkNode;
            
        }
        
        private StateNode BuildRootNode(int playerNumber, int depthBet)
        {
            var namesChildren = new List<string> {StateNode.Bet, StateNode.Check};

            var children = BuildChildren(namesChildren, playerNumber,depthBet);
            var checkNode = new StateNode(StateNode.Check, playerNumber, children);
            return checkNode;
            
        }
        private static int NextPlayerNum(int playerNumber)
        {
            return (playerNumber + 1) % StateNode.NumberPlayers;
        }
    }
}