using System.Collections.Generic;

namespace KuhnPoker
{
    public class StateNode
    {
        public const string Call = "call";
        public const string Bet = "bet";
        public const string Fold = "fold";
        public const string Check = "check";
        public const string Root = "root";
        public const int NumberPlayers = 2;
        public const int FirstPlayer = 0;
        private readonly List<StateNode> _children;
        private readonly int _playerNumber;
        private readonly string _nameNode;


        public StateNode(string nameNode, int playerNumber, List<StateNode> children)
        {
            this._children = children;
            this._playerNumber = playerNumber;
            this._nameNode = nameNode;
        }

        public string GetNameNode()
        {
            return _nameNode;
        }

        public List<StateNode> GetChildren()
        {
            return _children;
        }
        public int GetPlayerNumber()
        {
            return _playerNumber;
        }
    }
}