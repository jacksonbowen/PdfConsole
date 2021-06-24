using System;
using System.Diagnostics;
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
		//static void Main(string[] args)
		//{
		//	GlobalFontSettings.FontResolver = new FontResolver();

		//	var document = new PdfDocument();
		//	var page = document.AddPage();
		//	var gfx = XGraphics.FromPdfPage(page);
		//	var font = new XFont("OpenSans", 20, XFontStyle.Bold);
		//	string currentTimeString = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

		//	gfx.DrawString(
		//		"Hello World!",
		//		font,
		//		XBrushes.Black,
		//		new XRect(20, 20, page.Width, page.Height),
		//		XStringFormats.Center);

		//	string filename = "c:\\test\\testpdf.pdf";

		//	document.Save(filename);

		//}
		public class LayoutHelper
		{
			private readonly PdfDocument _document;
			private readonly XUnit _topPosition;
			private readonly XUnit _bottomMargin;
			private XUnit _currentPosition;

			public LayoutHelper(PdfDocument document, XUnit topPosition, XUnit bottomMargin)
			{
				_document = document;
				_topPosition = topPosition;
				_bottomMargin = bottomMargin;
				// Set a value outside the page - a new page will be created on the first request.
				_currentPosition = bottomMargin + 10000;
			}

			public XUnit GetLinePosition(XUnit requestedHeight)
			{
				return GetLinePosition(requestedHeight, -1f);
			}

			public XUnit GetLinePosition(XUnit requestedHeight, XUnit requiredHeight)
			{
				XUnit required = requiredHeight == -1f ? requestedHeight : requiredHeight;
				if (_currentPosition + required > _bottomMargin)
					CreatePage();
				XUnit result = _currentPosition;
				_currentPosition += requestedHeight;

				return result;
			}

			public XGraphics Gfx { get; private set; }

			public PdfPage Page { get; private set; }

			void CreatePage()
			{
				Page = _document.AddPage();
				Page.Size = PageSize.A4;
				Gfx = XGraphics.FromPdfPage(Page);
				_currentPosition = _topPosition;
			}
		}

		static void Main(string[] args)
		{
			PdfDocument document = new PdfDocument();

			// Sample uses DIN A4, page height is 29.7 cm. We use margins of 2.5 cm.
			LayoutHelper helper = new LayoutHelper(
				document,
				XUnit.FromCentimeter(2.5),
				XUnit.FromCentimeter(29.7 - 2.5));
			XUnit left = XUnit.FromCentimeter(2.5);

			// Random generator with seed value, so created document will always be the same.
			Random rand = new Random(42);

			const int headerFontSize = 20;
			const int normalFontSize = 10;

			XFont fontHeader = new XFont("Verdana", headerFontSize, XFontStyle.BoldItalic);
			XFont fontNormal = new XFont("Verdana", normalFontSize, XFontStyle.Regular);

			const int totalLines = 666;
			bool washeader = false;

			for (int line = 0; line < totalLines; ++line)
			{
				bool isHeader = line == 0 || !washeader && line < totalLines - 1 && rand.Next(15) == 0;
				washeader = isHeader;
				// We do not want a single header at the bottom of the page, so if we have a header we require space for header and a normal text line.
				XUnit top = helper.GetLinePosition(
					isHeader ? headerFontSize + 5 : normalFontSize + 2,
					isHeader ? headerFontSize + 5 + normalFontSize : normalFontSize);

				helper.Gfx.DrawString(
					isHeader
						? "Sed massa libero, semper a nisi nec"
						: "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
					isHeader ? fontHeader : fontNormal,
					XBrushes.Black,
					left,
					top,
					XStringFormats.TopLeft);
			}

			// Save the document... 
			const string filename = "c:\\test\\MultiplePages.pdf";
			document.Save(filename);
			// ...and start a viewer.
			//}
		}
	}
}
