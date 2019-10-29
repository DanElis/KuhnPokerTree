using System.Collections.Generic;

namespace KuhnPoker
{
    public class StateNode
    {
        public const string Call = "call";
        public const string Bet = "bet";
        public const string Fold = "fold";
        public const string Check = "check";
        private readonly StateNode _callNode;
        private readonly StateNode _betNode;
        private readonly StateNode _foldNode;
        private readonly StateNode _checkNode;
        private readonly int _playerNumber;
        private readonly bool _endGame;
        private readonly int _foldPlayer;
        private readonly List<string> _actions;
        public StateNode(StateNode call,StateNode bet,StateNode fold,StateNode check, int playerNumber, bool endGame, int foldPlayer)
        {
            _actions = new List<string>();
            
            this._playerNumber = playerNumber;
            this._endGame = endGame;
            this._foldPlayer = foldPlayer;
            if (call != null)
            {
                _actions.Add(Call);
                _callNode = call;
            }

            if (bet != null)
            {
                _actions.Add(Bet);
                _betNode = bet;
            }
            
            if (fold != null)
            {
                _actions.Add(Fold);
                _foldNode = fold;
            }
            
            if (check != null)
            {
                _actions.Add(Check);
                _checkNode = check;
            }
           
        }

        public bool IsEndGame()
        {
            return _endGame;
        }

        public int FoldPlayer()
        {
            return _foldPlayer;
        }

        public List<string> GetAction()
        {
            return _actions;
        }

        public StateNode GetNode(string nameAction)
        {
            StateNode retNode = null;
            switch (nameAction)
            {
                case Bet:
                    retNode = _betNode;
                    break;
                case Call:
                    retNode = _callNode;
                    break;
                case Fold:
                    retNode = _foldNode;
                    break;
                case Check:
                    retNode = _checkNode;
                    break;
            }

            return retNode;
        }

        public int GetPlayerNumber()
        {
            return _playerNumber;
        }
    }
}