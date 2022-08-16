using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MancalaAssessment.Model
{
    public class Player: ObservableObject
    {
        #region Fields
        private string _Name = "";
        private ScoreBoardModel _ScoreBoard = new ScoreBoardModel();
        private PitModel _selectedPit;
        private bool hasAnotherChance = false;
        #endregion

        #region Properties
        public string Name { get=>_Name; set=>SetProperty(ref _Name,value); }
        public ScoreBoardModel ScoreBoard { get=>_ScoreBoard; set=>SetProperty(ref _ScoreBoard, value); }
        public PitModel SelectedPit { get=>_selectedPit; set=>SetProperty(ref _selectedPit,value ); }
        public bool HasAnotherChance { get=>hasAnotherChance; set=>SetProperty(ref hasAnotherChance,value); }
        #endregion

        #region Methods
        public Player()
        {
            
        }


        /// <summary>
        /// Start throwing seed to the other pits
        /// </summary>
        /// <param name="pits"></param>
        public async Task<PitModel[]> ThrowSeed(PitModel[] pits)
        {
            var lastStone = SelectedPit.PitIndex + SelectedPit.TotalStone;
            int counter = 0;
            int indexer = SelectedPit.PitIndex + 1;
            for (int i=indexer;i<= lastStone; i++)
            {
                await Task.Delay(500);
                SelectedPit.TotalStone -= 1;
                pits[SelectedPit.PitIndex] = SelectedPit;
                if (i >= 12)
                {

                    // Top to bottom 

                    // islast pit
                    var isCaptured = CaptureStone(pits, pits[counter], SelectedPit.TotalStone);
                    if (pits[counter].IsLastPit)
                    {
                        if (pits[counter].IsLastDropped == false)
                        {
                            ThrowToScoreBoard(1);
                            if (SelectedPit.TotalStone == 0 && (pits[counter].IsLastPit))
                            {
                                // player will have another chance
                                HasAnotherChance = true;
                            }
                            else
                            {
                                pits[counter].IsLastDropped = true;
                                counter -= 1;
                            }
                        }
                        else
                        {
                            pits[counter].IsLastDropped = false;
                            pits[counter].TotalStone += 1;
                        }
                    }
                    else
                    {
                        if (!isCaptured)
                            pits[counter].TotalStone += 1;
                    }
                    counter++;
                }
                else
                {
                    // bottom to top
                    var isCaptured = CaptureStone(pits, pits[i], SelectedPit.TotalStone);
                    if (pits[i].IsLastPit)
                    {
                        if (pits[i].IsLastDropped==false)
                        {
                            ThrowToScoreBoard(1);
                            if (SelectedPit.TotalStone == 0 && (pits[i].IsLastPit))
                            {
                                // player will have another chance
                                HasAnotherChance = true;
                            }
                            else
                            {
                                pits[i].IsLastDropped = true;
                                i -= 1;
                            }
                            
                        }
                        else
                        {
                            pits[i].IsLastDropped = false;
                            pits[i].TotalStone += 1;
                            i += 1;
                        }
                    }
                    else
                    {
                        if (!isCaptured)
                            pits[i].TotalStone += 1;
                    }
                    counter = 0;
                }
                
            }
            return pits;
        }
        /// <summary>
        /// gets oponent players pit
        /// </summary>
        /// <param name="selectedPit"></param>
        /// <returns></returns>
        public PitModel GetOponentPit(PitModel selectedPit, PitModel[] pits)
        {
            return pits[selectedPit.OpponentPitIndex];
        }
        /// <summary>
        /// Capture Oponent Stones when the last stone pit is empty
        /// </summary>
        public bool CaptureStone(PitModel[] pits, PitModel currentPit, int remainingStones)
        {
            if(currentPit.IsEmpty && remainingStones==0)
            {
                var oponentPit = GetOponentPit(currentPit, pits);
                // oponent has stones in the pit
                if (!oponentPit.IsEmpty)
                {
                    // collect the oponent stones
                    ThrowToScoreBoard(oponentPit.TotalStone+1);
                    oponentPit.TotalStone = 0;
                    pits[oponentPit.PitIndex] = oponentPit;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds a stone to scoreboard
        /// </summary>
        /// <param name="totalStone"></param>
        public void ThrowToScoreBoard(int totalStone)
        {
            ScoreBoard.TotalStone += totalStone;
        }
        #endregion

    }
}
