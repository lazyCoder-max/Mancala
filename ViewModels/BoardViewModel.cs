using MancalaAssessment.Model;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MancalaAssessment.ViewModels
{
    public class BoardViewModel : ObservableObject
    {
        #region Fields
        private Player[] _Player = new Player[2];
        private PitModel[] pitModel = new PitModel[12];
        private int player = 0;
        private Visibility _GameStatus = Visibility.Collapsed;
        #endregion

        #region Properties
        public Player[] Player { get=>_Player; set=>SetProperty(ref _Player,value); }
        public PitModel[] Pits { get=>pitModel; set=> SetProperty(ref pitModel, value); }
        public int PlayerTurn { get=>player; set=>SetProperty(ref player,value); }
        public Visibility GameStatus { get=>_GameStatus; set=>SetProperty(ref _GameStatus, value); }

        /// <summary>
        /// Check if all pits are empty for the selected player
        /// </summary>
        public Visibility IsPitsEmpty
        {
            get
            {
                var result = Pits.Where(x=> x.Player.Name == Player[PlayerTurn].Name).All<PitModel>(y => y.IsEmpty);
                if(result)
                    return Visibility.Visible;
                return Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Counts and returns the winner
        /// </summary>
        /// <returns></returns>
        public Player GetWinner()
        {
           var result =  Player[0]?.ScoreBoard.TotalStone > Player[1]?.ScoreBoard.TotalStone ? Player[0] : Player[1];
            return result;
        }
        #endregion

        #region Methods

        public BoardViewModel()
        {
            InitiallizePlayer();
        }

        /// <summary>
        /// +------+------+--<<<<<-Player 2----+------+------+------+
        /// 2      |11    |10    |9     |8     |7     |6     |      1
        ///        |      |      |      |      |      |      |
        /// S      |      |      |      |      |      |      |      S
        /// C   0  +------+------+------+------+------+------+   0  C
        /// O      |0     |1     |2     |3     |4     |5     |      O
        /// R      |      |      |      |      |      |      |      R
        /// E      |      |      |      |      |      |      |      E
        /// +------+------+------+-Player 1->>>>>-----+------+------+
        /// </summary>
        /// There are 12 Holes ( Pit or Cup)
        /// 
        /// 
        /// ALGORITHM TO FIND THE OPPONENT PIT = TOTAL INDEX OF PIT - FORLOOP INDEX
        /// <param name="isSecondPlayer"></param>
        public void InitialisePit()
        {
            for (int i = 0; i <= 11; i++)
            {
                var pit = new PitModel()
                {
                    Name = $"Pit{i + 1}",
                    InitialStone = 4,
                    TotalStone = 4,
                    OpponentPitIndex = 11-i,
                    PitIndex = i,
                    IsLastPit = i==6? true: i==0? true:false,
                    Player = i <=5 ? Player[0]: Player[1]
                    };
                Pits[i] = pit;
            }
        }

        /// <summary>
        /// Initiallise Player for the first time
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        public void InitiallizePlayer(string player1="player1", string player2="player2")
        {
            var plyr1 = new Player()
            {
                Name = player1,
                ScoreBoard = new ScoreBoardModel() { TotalStone=0}
            };
            var plyr2 = new Player()
            {
                Name = player2,
                ScoreBoard = new ScoreBoardModel() { TotalStone = 0 }
            };
            Player[0] = plyr1;
            Player[1] = plyr2;
            InitialisePit();
        }

        /// <summary>
        /// Collect stones from selected pit
        /// </summary>
        public void CollectStone()
        {
            Player[PlayerTurn].SelectedPit.TotalStone = 0;
            Pits[Player[PlayerTurn].SelectedPit.PitIndex] = Player[PlayerTurn].SelectedPit;
        }


        /// <summary>
        /// gets oponent players pit
        /// </summary>
        /// <param name="selectedPit"></param>
        /// <returns></returns>
        public PitModel GetOponentPit(PitModel selectedPit)
        {
            return Pits[selectedPit.OpponentPitIndex];
        }

        public async Task ThrowStone()
        {
            Pits = await Player[PlayerTurn].ThrowSeed(Pits);
            GameStatus = IsPitsEmpty;
            ChangePlayer();
        }
        public void RestartGame()
        {
            Player[0].Name = "";
            Player[0].HasAnotherChance = false;
            Player[0].ScoreBoard = new ScoreBoardModel() { TotalStone = 0 };
            Player[1].Name = "";
            Player[1].HasAnotherChance = false;
            Player[1].ScoreBoard = new ScoreBoardModel() { TotalStone = 0 };
            for(int i=0;i<=11;i++)
            {
                var pit = new PitModel()
                {
                    Name = $"Pit{i + 1}",
                    InitialStone = 4,
                    TotalStone = 4,
                    OpponentPitIndex = 11 - i,
                    PitIndex = i,
                    IsLastPit = i == 6 ? true : i == 0 ? true : false,
                    Player = i <= 5 ? Player[0] : Player[1]
                };
                Pits[i] = pit;
            }
            PlayerTurn = 0;
        }
        public void ChangePlayer()
        {
            if (!Player[PlayerTurn].HasAnotherChance)
            {
                if (PlayerTurn == 0)
                    PlayerTurn = 1;
                else
                    PlayerTurn = 0;
            }
            else
            {
                Player[PlayerTurn].HasAnotherChance = false;
            }
        }
        #endregion
    }
}
