﻿using Newtonsoft.Json;
using OSL_Server.DataReciveClient.Processing.ChampSelect;

namespace OSL_Server.DataReciveClient
{
    public class ReciveFromClient
    {
        private static OSLLogger _logger = new OSLLogger("ReciveFromClient");

        private static bool gameFlowPhaseStatus = false;
        private static bool champSelectStatus = false;
        public static string ReadData(string content)
        {
            try
            {
                GameFlowPhaseStatus dataJsonRecive = JsonConvert.DeserializeObject<GameFlowPhaseStatus>(content);
                _logger.log(LoggingLevel.INFO, "ReadData()", $"DeserializeObject {content}");
                if (dataJsonRecive.Phase != null)
                {
                    gameFlowPhaseStatus = true;
                    if (dataJsonRecive.Phase.Equals("ChampSelect"))
                    {
                        if (dataJsonRecive.Status.Equals("Running"))
                        {
                            champSelectStatus = true;
                        }
                        else
                        {
                            champSelectStatus = false;
                        }
                    }
                    else if (dataJsonRecive.Phase.Equals("InGame"))
                    {

                    }
                }
                else
                {
                    gameFlowPhaseStatus = false;
                }
                _logger.log(LoggingLevel.INFO, "ReadData()", "Read Json GameFlowPhaseStatus");
            }
            catch (Exception e)
            {
                gameFlowPhaseStatus = false;
                _logger.log(LoggingLevel.ERROR, "ReadData()", $"Error Read Json GameFlowPhaseStatus {e.Message}");
            }
            if (!gameFlowPhaseStatus)
            {
                try
                {
                    //dynamic dataJsonRecive = JsonConvert.DeserializeObject(content);
                    if (champSelectStatus)
                    {
                        ChampSelectInfo.InChampSelect(content);
                    }
                }
                catch (Exception e)
                {
                    _logger.log(LoggingLevel.ERROR, "ReadData()", $"Error Read Json {e.Message}");
                }
            }
            return "Bonjour";
        }
    }
    public class GameFlowPhaseStatus
    {
        //public Int64 IdGame { get; set; }
        public string? Phase { get; set; }
        public string? Status { get; set; }
        public string? Date { get; set; }
    }
















    //Server recive from client : 
    //Waiting a Game
    //
    //Champ Select
    //Recive start champ select
    //Prepare to recive data
    //Recive data
    //Display on overlay information of data recive
    //When a champ is pick make request to a API
    //Stats pick
    //Stats ban
    //Stats win
    //Recive end champ select
    //End of champ select

    //Waiting Game Start
    //Display rune of summoners
    //Line vs line
    //Make stats of champ pick by summoners
    //Stats of Champ
    //Line vs line

    //In Game

    //End Game





    /// <summary>
    /// Recovery of information on the champion selection dans send it to the API
    /// </summary>
    //public static void InChampSelect()
    //{
    //    //All information necessary for display pick and ban overlay
    //    string champSelectContentPrevious = "";
    //    string champSelectContent;
    //    while ((champSelectContent = ApiRequest.RequestGameClientAPI(UrlRequest.lolchampselectv1session)) != null)
    //    {
    //        _logger.log(LoggingLevel.INFO, "ManageChampionSelect()", $"ChampSelectContent is {champSelectContent}");
    //        if (!champSelectContent.Equals(champSelectContentPrevious))
    //        {
    //            _logger.log(LoggingLevel.INFO, "ManageChampionSelect()", "Send ChampSelectContent");
    //            champSelectContentPrevious = champSelectContent;
    //            //Send to Server ChampSelectContent
    //        }
    //        else
    //        {
    //            _logger.log(LoggingLevel.WARN, "ManageChampionSelect()", "No modification of ChampSelectContent");
    //        }
    //        Thread.Sleep(1000);
    //    }
    //}
}
