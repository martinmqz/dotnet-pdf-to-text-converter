using System;
using System.IO;
using UglyToad.PdfPig;

namespace PdfToTextConverter
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.WriteLine("=== PDF to Text Converter ===");

      if (args.Length != 2)
      {
        Console.WriteLine("Usage: PdfToTextConverter <InputPdfPath> <OutputTextPath>");
        return;
      }

      string inputPdf = args[0];
      string outputTxt = args[1];

      if (!File.Exists(inputPdf))
      {
        Console.WriteLine($"Error: File does not exist: {inputPdf}");
        return;
      }

      try
      {
        string extractedText = ExtractTextFromPdf(inputPdf);

        File.WriteAllText(outputTxt, extractedText);

        Console.WriteLine($"✅ Text extracted and saved to: {outputTxt}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"❌ Error: {ex.Message}");
      }
    }

    static string ExtractTextFromPdf(string pdfPath)
    {
      using (PdfDocument document = PdfDocument.Open(pdfPath))
      {
        var text = new System.Text.StringBuilder();

        foreach (var page in document.GetPages())
        {
          text.AppendLine(page.Text);
        }

        return text.ToString();
      }
    }
  }
}
