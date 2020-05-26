﻿using System.Windows;
using System.Windows.Media;
using System;
using DiagramControls;
using DataBaseContext.Diagrams;
using DataBaseContext;
using GoodInfo;
using System.Windows.Controls;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System.Threading.Tasks;

namespace BillCollector
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml. This's user's part!
	/// </summary>
	public partial class MainWindow : Window
	{
		//SOLVE: update button
		//SOLVE: add list of bills
		public MainWindow()
		{
			InitializeComponent();

			LoadStatisticButton_Click(LoadBillsButton, null);
		}

		private void LoadStatisticButton_Click(object sender, RoutedEventArgs e)
		{
			var statUri = new Uri(@"Pages\StatisticPage.xaml", UriKind.Relative);
			PageFrame.Source = statUri;
		}

		private void LoadBillsButton_Click(object sender, RoutedEventArgs e)
		{
			var billsUri = new Uri(@"Pages\BillsPage.xaml", UriKind.Relative);
			
			PageFrame.Source = billsUri;
		}
	}
}
