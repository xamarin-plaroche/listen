﻿using System;
using System.Collections.Generic;
using Listen.ViewModels.ViewCell;
using PopolLib.VisualElements.FastListView;
using Xamarin.Forms;

namespace Listen.Views.ViewCells
{
    public partial class NoSurveyViewCell : FastViewCell
    {
        public NoSurveyViewCell()
        {
            BindingContext = new NoSurveyViewCellViewModel();
            InitializeComponent();
        }
    }
}
