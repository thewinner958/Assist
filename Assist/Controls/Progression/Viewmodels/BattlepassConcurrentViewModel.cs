﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using Assist.MVVM.ViewModel;

namespace Assist.Controls.Progression.Viewmodels
{
    internal class BattlepassConcurrentViewModel : ViewModelBase
    {
        private BitmapImage _contractRewardImage;

        public BitmapImage ContractRewardImage
        {
            get => _contractRewardImage;
            set => SetProperty(ref _contractRewardImage, value);
        }

        private string _contractRewardName = "Loading..";
        public string ContractRewardName
        {
            get => _contractRewardName;
            set => SetProperty(ref _contractRewardName, value);
        }

        public string _contractTier;

        public string ContractTier
        {
            get => _contractTier;
            set => SetProperty(ref _contractTier, value);
        }


        private int _contractTierNumber;

        public int ContractTierNumber
        {
            get => _contractTierNumber;
            set => SetProperty(ref _contractTierNumber, value);
        }

        public string _ContractTierXp;

        public string ContractTierXp
        {
            get => _ContractTierXp;
            set => SetProperty(ref _ContractTierXp, value);
        }

        private int _currentXp;

        public int CurrentXp
        {
            get => _currentXp;
            set => SetProperty(ref _currentXp, value);
        }

        private int _neededXp;

        public int NeededXp
        {
            get => _neededXp;
            set => SetProperty(ref _neededXp, value);
        }

        public double _progression;

        public double Progression
        {
            get => _progression;
            set => SetProperty(ref _progression, value);
        }

        public async void SetupControl()
        {
            
            var bpContract = await AssistApplication.AppInstance.CurrentUser.Contracts.GetContract(AssistApiController.currentBattlepassId);
            ContractTierNumber = bpContract.ProgressionLevelReached;
            var xpTier = bpContract.ProgressionLevelReached - 1;
            NeededXp = (xpTier * 750) + 2000;
            CurrentXp = bpContract.ProgressionTowardsNextLevel;
            ContractTierXp = $"{CurrentXp}XP / {NeededXp}XP";
            ContractTier = $"{Properties.Languages.Lang.Progression_Battlepass_CurrTier} {ContractTierNumber+1}";
            Progression = (((double)CurrentXp / NeededXp) * 100);

            //Get Reward Information
            var bpData = await AssistApplication.AppInstance.AssistApiController.GetBattlepassData();

            if (ContractTierNumber == 55)
            {
                var levels = bpData.chapters[bpData.chapters.Count - 1].levels;
                var itemData = levels[levels.Count - 1];
                ContractRewardImage = await App.LoadImageUrl(itemData.rewardDisplayIcon);
                ContractRewardName = itemData.rewardName;
                ContractTierXp = "";
                ContractTier = Properties.Languages.Lang.Progression_Battlepass_Completed;
                Progression = 100;
            }
            else
            {
                var contactLevel = ContractTierNumber / 5;
                var contactLevelTier = ContractTierNumber - (contactLevel * 5);
                var itemData = bpData.chapters[contactLevel].levels[contactLevelTier];
                ContractRewardImage = await App.LoadImageUrl(itemData.rewardDisplayIcon);
                ContractRewardName = itemData.rewardName;
            }

            
            
        }
    }
}
