using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaAssessment.Model
{
    public class ScoreBoardModel: ObservableObject
    {
        #region Fields
        private int _TotalStone = 0;
        #endregion

        #region Properties
        public int TotalStone { get => _TotalStone; set => SetProperty(ref _TotalStone,value); }
        #endregion

        #region Methods
        #endregion
    }
}
