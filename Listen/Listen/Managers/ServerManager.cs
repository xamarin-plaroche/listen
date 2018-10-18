﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Listen.Models.WebServices;
using Xamarin.Forms;

namespace Listen.Managers
{
    public class ServerManager 
    {
        private static readonly Lazy<ServerManager> lazy = new Lazy<ServerManager>(() => new ServerManager());

        public ServerManager()
        {
        }

        public static ServerManager Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public async Task<List<Survey>> GetSurveysAsync()
        {
            string token = "";
            var user = await UserManager.Instance.GetUser();
            token = user?.Token;
            // -- On  checke si Token valide
            var infos = await TokenManager.Instance.GetInfoAsync(token);
            if (infos == null)
            {
                var newtoken = await TokenManager.Instance.RefreshTokenAsync(user?.RefreshToken);
                token = newtoken?.AccessToken;
            }
            var list = await SurveyWS.Instance.GetSurveysAsync(token);
            await SurveyManager.Instance.AddOrUpdateAsync(list);
            return list;
        }
    }
}
