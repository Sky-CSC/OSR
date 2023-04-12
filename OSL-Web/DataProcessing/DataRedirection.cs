﻿using Newtonsoft.Json;
using OSL_Common.System.Logging;
using OSL_Common.System.Sockets;

namespace OSL_Web.DataProcessing
{
    public class DataRedirection
    {
        private static Logger _logger = new("DataRedirection");

        private static bool gameFlowPhaseStatus = false;
        private static bool champSelectStatus = false;
        private static bool inGameStatus = false;
        private static bool endGameStatus = false;

        public static void RedirectData(string content)
        {
            try
            {
                GameFlowPhaseStatus dataJsonRecive = JsonConvert.DeserializeObject<GameFlowPhaseStatus>(content);
                if (dataJsonRecive.Phase != null && dataJsonRecive.Status != null && dataJsonRecive.Date != null)
                {
                    _logger.log(LoggingLevel.INFO, "RedirectData()", "Read Json GameFlowPhaseStatus");
                    if (dataJsonRecive.Phase.Equals("ChampSelect"))
                    {
                        if (dataJsonRecive.Status.Equals("Running"))
                        {
                            champSelectStatus = true; //We are in champ select
                        }
                        else
                        {
                            champSelectStatus = false; //Champ select finished
                        }
                        gameFlowPhaseStatus = true; //Prepare for read data for specific game phase
                    }
                    else if (dataJsonRecive.Phase.Equals("InGame"))
                    {
                        if (dataJsonRecive.Status.Equals("Running"))
                        {
                            inGameStatus = true; //We are in game
                            InGame.firstCallPerks = true; //To create a list of perks once
                        }
                        else
                        {
                            inGameStatus = false; //In game finished
                        }
                        gameFlowPhaseStatus = true; //Prepare for read data for specific game phase
                    }
                    else if (dataJsonRecive.Phase.Equals("EndGame"))
                    {
                        if (dataJsonRecive.Status.Equals("Running"))
                        {
                            endGameStatus = true; //We are in end game
                        }
                        else
                        {
                            endGameStatus = false; //End game finished
                        }
                        gameFlowPhaseStatus = true; //Prepare for read data for specific game phase
                    }
                }
                else
                {
                    gameFlowPhaseStatus = false; //Data recive is not a GameFlowPhaseStatus
                    _logger.log(LoggingLevel.INFO, "RedirectData()", "Not a GameFlowPhaseStatus");
                }
            }
            catch
            {
                gameFlowPhaseStatus = false;
                _logger.log(LoggingLevel.INFO, "RedirectData()", $"Not a GameFlowPhaseStatus");
            }
            if (!gameFlowPhaseStatus) //If we recive data different from GameFlowPhaseStatus
            {
                try
                {
                    if (champSelectStatus)
                    {
                        _logger.log(LoggingLevel.INFO, "RedirectData()", $"Send this data to champ select");
                        ChampSelect.ReadData(content);
                    }
                    else if (inGameStatus)
                    {
                        _logger.log(LoggingLevel.INFO, "RedirectData()", $"Send this data to in game");
                        InGame.ReadData(content);
                    }
                    else if (endGameStatus)
                    {
                        _logger.log(LoggingLevel.INFO, "RedirectData()", $"Send this data to end game");
                        EndGame.ReadData(content);
                    }
                }
                catch (Exception e)
                {
                    _logger.log(LoggingLevel.ERROR, "RedirectData()", $"Error Read Json {e.Message}");
                }
            }
        }



        /// <summary>
        /// Game Flow Phase Status
        /// </summary>
        public class GameFlowPhaseStatus
        {
            //public Int64 IdGame { get; set; }
            public string? Phase { get; set; }
            public string? Status { get; set; }
            public string? Date { get; set; }
        }
    }
}