﻿using DataBaseContext;
using GoodInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using DataBaseContext.Diagrams;
using System.Windows;

namespace AdditionalControls
{
	/// <summary>
	/// Логика взаимодействия для PieDiagram.xaml
	/// </summary>
	public partial class PieDiagram : UserControl
	{
		//TODO: make templated
		public DateTime Initial { get; }
		public DateTime? Final { get; }

		private readonly List<PiePiece> piePieces = new List<PiePiece>();
		private const int FullAngle = 360;

		public Scopes<GoodType, Expence.ExpenceSelection> Scopes { get; }
		public SolidColorBrush[] UsersBrushes { get; }

		public PieDiagram(Scopes<GoodType, Expence.ExpenceSelection> scopes, SolidColorBrush[] brushes)
		{
			if (scopes.Count() < brushes.Length)
				throw new ArgumentException($"Amount of {nameof(brushes)} must be not less then amount of members in enum {scopes.EnumType.Name}");

			if (scopes is null)
				throw new ArgumentNullException($"{nameof(scopes)} was null!");

			InitializeComponent();
			UsersBrushes = brushes;
			Scopes = scopes;

			if (Scopes.IsEmpty)
			{
				MessageBox.Show("There's no data for this period");
				return;
			}

			InitializeLegend();
			InitializePiePieces();
		}

		private void InitializePiePieces()
		{
			var generalVol = Scopes.TotalSum;
			var genAngle = 0.0;
			for (int i = 0; i < Scopes.Count(); i++)
			{
				if (Scopes[i].Sum != 0)
				{
					var angle = Convert.ToDouble((Scopes[i].Sum * FullAngle) / generalVol);
					var piePiece = new PiePiece(i, angle, UsersBrushes[i]);
					piePiece.MouseIn += PiePiece_MouseIn;
					piePiece.MouseOut += PiePiece_MouseOut;
					piePiece.Rotate(genAngle);
					genAngle += angle;
					piePieces.Add(piePiece);
					PiecesGrid.Children.Add(piePiece);
				}
			}
		}

		private void PiePiece_MouseOut(PiePiece sender)
		{
			piePieceHeaderTextBlock.Text = "General info";
			piePieceInfoTextBlock.Text = "";
			Scopes.OutputData((output) => piePieceInfoTextBlock.Text += output);

		}

		private void PiePiece_MouseIn(PiePiece sender)
		{
			int num = sender.Num;
			piePieceInfoTextBlock.Text = "";
			piePieceHeaderTextBlock.Text = $"{Scopes.EnumStringValues[num]} ()";
			Scopes[num].OutputData((output) => piePieceInfoTextBlock.Text += output);

		}

		private void InitializeLegend()
		{
			for (int i = 0; i < Scopes.EnumStringValues.Count; i++)
			{
				legend.Children.Add(new PieLegendItem(UsersBrushes[i], Scopes.EnumStringValues[i]));
			}
		}
	}
}
