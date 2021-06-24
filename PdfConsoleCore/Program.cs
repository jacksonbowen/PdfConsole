using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using PdfSharpCore;
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

			//pdf settings
			var document = new PdfDocument();
			var page = document.AddPage();
			var gfx = XGraphics.FromPdfPage(page);

			//files
			string currentTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
			string filename = "c:\\test\\testpdf.pdf";
			string image = "C:\\Users\\jacks\\Documents\\Visual Studio 2019\\Projects\\PdfConsole\\PdfConsoleCore\\Assets\\aca.png";

			//fonts
			var font = new XFont("OpenSans", 20, XFontStyle.Bold);

			const int headerFontSize = 20;
			const int normalFontSize = 10;

			XFont fontHeader = new XFont("Verdana", headerFontSize, XFontStyle.BoldItalic);
			XFont fontNormal = new XFont("Verdana", normalFontSize, XFontStyle.Regular);

			string accountNumber = "1234567890";
			string userName = "Test User";
			string address = "123 Test";
			string city = "Test";
			string state = "TS";
			string zip = "12345";

			string addressFormatted =
				$"User : {userName}" +
				$"Address: {address}" +
				$"City: {city}" +
				$"State: {state}" +
				$"Zip: {zip}";

			//var sampleText =
			//	"Facin exeraessisit la consenim iureet dignibh eu facilluptat vercil dunt autpat. " +
			//	"Ecte magna faccum dolor sequisc iliquat, quat, quipiss equipit accummy niate magna " +
			//	"facil iure eraesequis am velit, quat atis dolore dolent luptat nulla adio odipissectet " +
			//	"lan venis do essequatio conulla facillandrem zzriusci bla ad minim inis nim velit eugait " +
			//	"aut aut lor at ilit ut nulla ate te eugait alit augiamet ad magnim iurem il eu feuissi.\n" +
			//	"Guer sequis duis eu feugait luptat lum adiamet, si tate dolore mod eu facidunt adignisl in " +
			//	"henim dolorem nulla faccum vel inis dolutpatum iusto od min ex euis adio exer sed del " +
			//	"dolor ing enit veniamcon vullutat praestrud molenis ciduisim doloborem ipit nulla consequisi.\n" +
			//	"Nos adit pratetu eriurem delestie del ut lumsandreet nis exerilisit wis nos alit venit praestrud " +
			//	"dolor sum volore facidui blaor erillaortis ad ea augue corem dunt nis  iustinciduis euisi.\n" +
			//	"Ut ulputate volore min ut nulpute dolobor sequism olorperilit autatie modit wisl illuptat dolore " +
			//	"min ut in ute doloboreet ip ex et am dunt at.";

			gfx.DrawImage(XImage.FromFile(image), 30, 15);
			
			gfx.DrawString(
				$"Account : {accountNumber}",
				fontHeader,
				XBrushes.Black,
				new XRect(30, 150, page.Width, page.Height),
				XStringFormats.TopLeft);


			gfx.DrawString($"User      : {userName}",
				fontNormal,
				XBrushes.Black,
				new XRect(30, 200, page.Width, page.Height),
				XStringFormats.TopLeft);

			gfx.DrawString($"Address : {address}",
				fontNormal,
				XBrushes.Black,
				new XRect(30, 210, page.Width, page.Height),
				XStringFormats.TopLeft);

			gfx.DrawString($"City       : {city}",
				fontNormal,
				XBrushes.Black,
				new XRect(30, 220, page.Width, page.Height),
				XStringFormats.TopLeft);

			gfx.DrawString($"State     : {state}",
				fontNormal,
				XBrushes.Black,
				new XRect(30, 230, page.Width, page.Height),
				XStringFormats.TopLeft);

			gfx.DrawString($"Zip        : {zip}",
				fontNormal,
				XBrushes.Black,
				new XRect(30, 240, page.Width, page.Height),
				XStringFormats.TopLeft);

			//gfx.DrawString(sampleText,
			//	fontNormal,
			//	XBrushes.Black,
			//	new XRect(30, 250, page.Width, page.Height),
			//	XStringFormats.TopLeft);

			// Save the document... 

			document.Save(filename);
			// ...and start a viewer.
			//}
		}
	}
}
