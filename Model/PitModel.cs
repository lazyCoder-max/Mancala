using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaAssessment.Model
{
    public class PitModel: ObservableObject
    {
        #region Fields
        private string _Name = "";
        private float _Location = 0f;
        private int _TotalStone = 0 ;
        private int _InitialStone = 4;
        private int _OpponentPitIndex = -1;
        private int _PitIndex = -1;
        private bool _IsLastPit = false;
        private bool _IsLastDropped = false;
        private Player _Player;
        #endregion

        #region Properties
        public string Name { get=> _Name; set=> SetProperty(ref _Name, value); }
        public float Location { get=>_Location; set => SetProperty(ref _Location, value); }
        public int TotalStone { get=>_TotalStone; set => SetProperty(ref _TotalStone, value); }
        public int InitialStone { get=> _InitialStone; set => SetProperty(ref _InitialStone, value); }
        /// <summary>
        /// used to get the opposite side of the pit
        /// </summary>
        public int OpponentPitIndex { get => _OpponentPitIndex; set => SetProperty(ref _OpponentPitIndex, value); }
        public bool IsLastPit { get=>_IsLastPit; set=>SetProperty(ref _IsLastPit, value); }
        public bool IsLastDropped { get => _IsLastDropped; set => SetProperty(ref _IsLastDropped, value); }
        public int PitIndex { get => _PitIndex; set => SetProperty(ref _PitIndex, value); }
        public Player Player { get=>_Player; set=>SetProperty(ref _Player,value); }

        /// <summary>
        /// Check if the PIT (CUP) is empty
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if(TotalStone==0)
                    return true;
                return false;
            }
        }

        #endregion

        #region Methods
        public PitModel()
        {
            TotalStone = InitialStone;
        }
        #endregion
    }
}
