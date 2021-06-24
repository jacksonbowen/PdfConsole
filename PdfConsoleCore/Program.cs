using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Utils;

namespace PdfConsoleCore
{
	class Program
	{
		static void Main(string[] args)
		{
			GenerateTestPdf();
		}

		static void GenerateTestPdf()
		{
			GlobalFontSettings.FontResolver = new FontResolver();

			//pdf settings
			var document = new PdfDocument();
			var page = document.AddPage();
			var gfx = XGraphics.FromPdfPage(page);
			XTextFormatter tf = new XTextFormatter(gfx);

			//files
			string currentTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
			string currentTimePretty = DateTime.Now.ToString();
			string filename = $"c:\\test\\testpdf-{currentTimeString}.pdf";
			string acaLogo = "C:\\Users\\jacks\\Documents\\Visual Studio 2019\\Projects\\PdfConsole\\PdfConsoleCore\\Assets\\aca.png";

			//fonts
			var font = new XFont("OpenSans", 20, XFontStyle.Bold);

			const int headerFontSize = 15;
			const int normalFontSize = 10;

			XFont fontHeader = new XFont("Verdana", headerFontSize, XFontStyle.BoldItalic);
			XFont fontNormal = new XFont("Verdana", normalFontSize, XFontStyle.Regular);

			//field values

			string accountNumber = "1234567890";
			string userName = "Test User";
			string address = "123 Main Street Apt #101";
			string city = "Greenville";
			string state = "SC";
			string zip = "12345";

			var addressFormatted =
				$"User : {userName}\n" +
				$"Address: {address}\n" +
				$"City: {city}\n" +
				$"State: {state}\n" +
				$"Zip: {zip}";

			var textOne =
				$"Dear {userName}, \n" +
				"\n" +
				"Ecte magna faccum dolor sequisc iliquat, quat, quipiss equipit accummy niate magna " +
				"facil iure eraesequis am velit, quat atis dolore dolent luptat nulla adio odipissectet " +
				"lan venis do essequatio conulla facillandrem zzriusci bla ad minim inis nim velit eugait " +
				"aut aut lor at ilit ut nulla ate te eugait alit augiamet ad magnim iurem il eu feuissi.\n" +
				"Guer sequis duis eu feugait luptat lum adiamet, si tate dolore mod eu facidunt adignisl in ";

			var textTwo =
				"dolor ing enit veniamcon vullutat praestrud molenis ciduisim doloborem ipit nulla consequisi.\n" +
				"Nos adit pratetu eriurem delestie del ut lumsandreet nis exerilisit wis nos alit venit praestrud " +
				"dolor sum volore facidui blaor erillaortis ad ea augue corem dunt nis  iustinciduis euisi.\n" +
				"Ut ulputate volore min ut nulpute dolobor sequism olorperilit autatie modit wisl illuptat dolore " +
				"min ut in ute doloboreet ip ex et am dunt at. \n";

			//start creating document
			gfx.DrawImage(XImage.FromFile(acaLogo), 30, 15);

			XRect rect = new XRect(30, 150, 250, 220);
			gfx.DrawRectangle(XBrushes.White, rect);
			tf.Alignment = XParagraphAlignment.Left;
			tf.DrawString(addressFormatted, fontNormal, XBrushes.Black, rect, XStringFormats.TopLeft);

			gfx.DrawString(
				$"Current Time: {currentTimePretty}",
				fontNormal,
				XBrushes.Black,
				new XRect(30, 230, page.Width, page.Height),
				XStringFormats.TopLeft);

			rect = new XRect(30, 260, 250, 180);
			gfx.DrawRectangle(XBrushes.WhiteSmoke, rect);
			tf.Alignment = XParagraphAlignment.Left;
			tf.DrawString(textOne, fontNormal, XBrushes.Black, rect, XStringFormats.TopLeft);

			rect = new XRect(310, 260, 250, 180);
			gfx.DrawRectangle(XBrushes.WhiteSmoke, rect);
			tf.Alignment = XParagraphAlignment.Left;
			tf.DrawString(textTwo, fontNormal, XBrushes.Black, rect, XStringFormats.TopLeft);



			gfx.DrawString(
				$"Account : {accountNumber}",
				fontHeader,
				XBrushes.Black,
				new XRect(-60, 150, page.Width, page.Height),
				XStringFormats.TopRight);



			// Save the document... 
			document.Save(filename);
			// ...and start a viewer.
			//}
		}
	}
}
