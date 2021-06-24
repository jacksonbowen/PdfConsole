using System;
using System.IO;
using UglyToad.PdfPig.Content;
using UglyToad.PdfPig.Core;
using UglyToad.PdfPig.Fonts.Standard14Fonts;
using UglyToad.PdfPig.Writer;

namespace PdfConsole
{
  class PdfPigPdf
  {
		static void Main(string[] args)
		{
			PdfDocumentBuilder builder = new PdfDocumentBuilder();

			PdfPageBuilder page = builder.AddPage(PageSize.A4);

			// Fonts must be registered with the document builder prior to use to prevent duplication.
			PdfDocumentBuilder.AddedFont font = builder.AddStandard14Font(Standard14Font.Helvetica);

			string companyName = "test company name";
			string testDocumentBody = "test document body";

			page.AddText(companyName, 30, new PdfPoint(25, 700), font);

			page.AddText(testDocumentBody, 20, new PdfPoint(25, 650), font);

			byte[] documentBytes = builder.Build();

			string currentTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

			File.WriteAllBytes($@"C:\test\newPdf + {currentTimeString}.pdf", documentBytes);
		}
	}
}
