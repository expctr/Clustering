﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClusteringLib;

namespace Кластеризация
{
    public partial class ClusterizationParameterOptionsForm : Form
    {
        public ClusterizationParameterOptionsForm()
        {
            InitializeComponent();
            CCPChLB.CheckOnClick = true;
        }

        public MainForm ParentWinForm;

        public MainModel parentModel;

        public ClusterizationParameterOptionsForm(MainForm parentWinForm, MainModel parentModel, bool ApplyEnabled) : this()
        {
            //SizeChanged += ChooseClusterizationParameterForm_SizeChanged;
            ParentWinForm = parentWinForm;
            this.parentModel = parentModel;
            //CPOptions =
            //    ParentWinForm.GetClusterizationParameterOptions();
            //OriginalColsNames = ParentWinForm.GetOriginalColsNames();
            ApplyB.Enabled = ApplyEnabled;
            //ApplyB.Click += ApplyB_Click;
            //ShowClusteringParameterWeight.Click += ShowClusteringParameterWeight_Click;
        }

        public void ShowInfo(string[] OriginalColsNames, ClusteringParameterOptions CPOptions)
        {
            GetCCPChLB().Items.Clear();
            if (OriginalColsNames == null)
            {
                return;
            }
            GetCCPChLB().Items.AddRange(OriginalColsNames);
            for (int i = 0; i < CPOptions.ChosenClusterizationParameter.Length; ++i)
            {
                GetCCPChLB().SetItemChecked(i, CPOptions.ChosenClusterizationParameter[i]);
            }
            GetNormalizeChB().Checked = CPOptions.Normalize;
        }

        public CheckedListBox GetCCPChLB()
        {
            return CCPChLB;
        }

        public CheckBox GetNormalizeChB()
        {
            return NormalizeChB;
        }

        public CheckBox GetChangeWeightChB()
        {
            return ChangeWeightChB;
        }

        public TextBox GetClusteringParameterNameTB()
        {
            return ClusteringParameterNameTB;
        }

        public TextBox GetClusteringParameterWeightTB()
        {
            return ClusteringParameterWeightTB;
        }

        public Button GetApplyB()
        {
            return ApplyB;
        }

        public int GetHeight()
        {
            return Height;
        }

        public int GetWidth()
        {
            return Width;
        }

        public Label GetClusteringParameterWeightL()
        {
            return ClusteringParameterWeightL;
        }

        public Button GetShowClusteringParameterWeight()
        {
            return ShowClusteringParameterWeight;
        }

        public Label GetClusteringParameterNameL()
        {
            return ClusteringParameterNameL;
        }
    }
}
