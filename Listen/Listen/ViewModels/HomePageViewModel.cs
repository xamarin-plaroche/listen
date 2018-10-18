﻿using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using Listen.Managers;
using Listen.Models.WebServices;
using Listen.Views;
using PopolLib.Services;
using ReactiveUI;
using Xamarin.Forms;

namespace Listen.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        INavigation _nav;

        public ReactiveCommand QuestionnaireCommand { get; set; }

        public ICommand InfosCommand { get; set; }


        public HomePageViewModel(INavigation nav)
        {
            _nav = nav;

            InfosCommand = new Command(async () =>
            {
                await Task.Delay(2000);
                //await _nav.PushAsync(new ParametresPage(new ParametresPageViewModel(_nav)));
            });

            QuestionnaireCommand = ReactiveCommand.CreateFromTask(async () =>
            {

                var hud = DependencyService.Get<IProgressHUD>();
                hud.Show("Chargement ...");
                var surveys = await SurveyManager.Instance.GetSurveysAsync();

                hud.Dismiss();

                if (surveys?.Count > 0)
                {
                    await _nav.PushAsync(new SurveyPage(new SurveyPageViewModel(_nav, surveys)));
                }
                else
                {
                    var dialog = DependencyService.Get<IDialogService>();
                    dialog.Show("Erreur", "Aucun formulaires disponibles.", "Oui", (_obj) => { });
                }
            });
        }
    }
}
