using System;
using System.Diagnostics;
using System.IO;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Utils;

namespace PdfConsoleCore
{
	class Program
	{
		static void Main(string[] args)
		{
			GlobalFontSettings.FontResolver = new FontResolver();

			var document = new PdfDocument();
			var page = document.AddPage();
			var gfx = XGraphics.FromPdfPage(page);
			var font = new XFont("OpenSans", 20, XFontStyle.Bold);
			string currentTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

			gfx.DrawString(
				"Hello World!",
				font,
				XBrushes.Black,
				new XRect(20, 20, page.Width, page.Height),
				XStringFormats.Center);

			string filename = "c:\\test\\testpdf.pdf";

			document.Save(filename);

		}
	}
}
