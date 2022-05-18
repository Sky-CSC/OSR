﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OSR_Client.RiotApp.API;
using OSR_Client.RiotApp.API.LCU;

namespace OSR_Client.RiotApp.DataProcessing
{
    internal class ChampSelectProcess
    {
        private static OSRLogger _logger = new OSRLogger("ChampionSelect");
        /// <summary>
        /// Recovery of information on the champion selection dans send it to the API
        /// </summary>
        public static void InChampSelect()
        {
            //All information necessary for display pick and ban overlay
            string champSelectContent = ApiRequest.RequestGameClientAPI(UrlRequest.lolchampselectv1session); 
            if (champSelectContent == null)
            {
                _logger.log(LoggingLevel.ERROR, "ManageChampionSelect()", "ChampSelectContent is null");
            }
            else
            {
                _logger.log(LoggingLevel.INFO, "ManageChampionSelect()", $"ChampSelectContent is {champSelectContent}");
                //send post or put att server
            }
        }
    }
}
