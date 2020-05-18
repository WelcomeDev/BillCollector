﻿using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System.Threading.Tasks;

namespace DataBaseContext.OutputTools
{
	public static class ToPDFConverter
	{
		private const string Header = "Receip";
		private const string PostHeaderInfo = "Bla-bla-bla-bla-bla-bla-bla-bla-bla-bla-bla-bla-bla\nBla-bla-bla-bla-bla-bla-bla-bla-bla-bla-bla-bla";
		private const int MainFontSize = 14;
		private readonly static string Separator = new string('*', 20);
		private readonly static string DirPath = @"C:\Users\User\source\repos\BillCollector\DataBaseContext\Bills\";
		//private readonly static string DirPath = @"C:\Users\aleks\Source\Repos\BillCollector\DataBaseContext\Bills\";

		public static async Task CreateAsync(Expence expence)
		{
			var path = await Task.Run(() => Create(expence));
			expence.CreateBill(path);
		}

		//RETURNS PATHs
		/// <summary>
		/// 
		/// </summary>
		/// <param name="expence"></param>
		private static string Create(Expence expence)
		{
			var filePath = System.IO.Path.Combine(DirPath, expence.IdentityGuid.ToString() + ".pdf");

			var pdfWriter = new PdfWriter(filePath);
			var pdfDoc = new PdfDocument(pdfWriter);
			var doc = new Document(pdfDoc);
			var header = new Paragraph(Header).SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER).SetFontSize(24);
			doc.Add(header).SetFontSize(MainFontSize);
			doc.Add(new Paragraph(PostHeaderInfo));
			doc.Add(new Paragraph(Separator));

			decimal totalSum = 0;
			foreach (var item in expence.Goods)
			{
				var curTotSum = item.Key.Price * item.Value;
				totalSum += curTotSum;
				var resStr = $"{item.Value} {item.Key.Name} - {curTotSum}";
				var par = new Paragraph(resStr).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(MainFontSize);
				doc.Add(par);
			}
			doc.Add(new Paragraph(Separator));
			doc.Add(new Paragraph(totalSum.ToString()).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(20).SetLineThrough());
			doc.Add(new Paragraph(expence.Date.ToShortDateString()).SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT).SetFontSize(MainFontSize));

			doc.Close();
			return filePath;
		}

		//public static async Task ReadAsync(string path)
		//{

		//}
	}
}