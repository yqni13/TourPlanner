using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using TourPlanner.BL.Services;
using TourPlanner.Models;
using TourPlanner.Models.Enums;

namespace TourPlanner.BL.PDFGeneration
{
    public class TourToPDF
    {
        public void GenerateSummarizeReport()
        {      
            // Containing statistical report of average time, distance and rating of all existing tours from their regarding tour logs.
            Tour tour = new();
            List<Tour> tourList = DataBaseService.GetAllTours();
            string fileName = $"Summary_{DateTime.Now.ToString("yyyy-MM-dd")}_{tour.Name}.pdf";

            PdfWriter writer = new PdfWriter(fileName);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            Paragraph reportHeaderTourLogs = new Paragraph($"Summary for Tours")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetBold()
                    .SetFontColor(ColorConstants.BLUE);
            document.Add(reportHeaderTourLogs);

            // Add line seperator.
            LineSeparator seperatorHeaderTourLogs1 = new(new SolidLine());
            document.Add(seperatorHeaderTourLogs1);

            // Add new line to get space between data.
            document.Add(new Paragraph("\n"));

            Paragraph tourListHeader = new Paragraph("List of all Tours:")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(14)
                    .SetBold()
                    .SetFontColor(ColorConstants.ORANGE);
            document.Add(tourListHeader);

            List listingAllTours = new List()
                .SetSymbolIndent(12)
                .SetListSymbol("\u2022")
                .SetMarginLeft(30)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD));
            foreach (Tour t in tourList)
            {
                listingAllTours.Add(new ListItem($"{t.Name} (from: {t.From} - to: {t.To})"));
            }
            document.Add(listingAllTours);

            // Add new line to get space between data.
            document.Add(new Paragraph("\n"));

            Paragraph tourlogsListHeader = new Paragraph("Statistics of all Tours:")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(14)
                    .SetBold()
                    .SetFontColor(ColorConstants.ORANGE);
            document.Add(tourlogsListHeader);

            List listingAllStatistics = new List()
                /*.SetSymbolIndent(12)
                .SetListSymbol("\u2022")*/
                .SetMarginLeft(30)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD));
            foreach (Tour t in tourList)
            {
                listingAllStatistics.Add($"{t.Name}");
                document.Add(listingAllStatistics);
                listingAllStatistics.GetChildren().Clear();

                List listingStatistics = new List()
                    .SetSymbolIndent(10)
                    .SetListSymbol("\u2022")
                    .SetMarginLeft(60)
                    .SetFontSize(10)
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD));

                listingStatistics.Add(new ListItem($"Difficulty: {AverageDifficulty(t.TourLogs)}"));
                listingStatistics.Add(new ListItem($"Rating: {AverageRating(t.TourLogs)}"));
                listingStatistics.Add(new ListItem($"Time: {AverageTime(t.TourLogs)}"));
                document.Add(listingStatistics);
            }

            // Add new line to get space between data.
            document.Add(new Paragraph("\n"));

            // Add explanation of Difficulty and Rating.
            Paragraph difficultyExplanation = new Paragraph("Difficulty: 1 - Warmup, 2 - Easy, 3 - Moderate, 4 - Hard, 5 - Expert\nRating: 1 - Worst, 2 - Bad, 3 - Weak, 4 - Improveable, 5 - Moderate, 6 - Advancement, 7 - Good, 8 - Excellent, 9 - Satisfying, 10 - Perfect")
                .SetFontSize(7)
                .SetFontColor(ColorConstants.GRAY);
            document.Add(difficultyExplanation);

            // Page numbers
            int n = pdf.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format("page" + i + " of " + n)),
                   559, 25, i, TextAlignment.RIGHT,
                   VerticalAlignment.BOTTOM, 0);
            }

            document.Close();
        }

        public void GenerateTourReport()
        {            
            // Containing all information of single tour including all regarding tour logs.            
            
            Tour tour = new();
            string fileName = $"Report_{DateTime.Now.ToString("yyyy-MM-dd")}_{tour.Name}.pdf";
            string mapImage = tour.MapPath;

            PdfWriter writer = new PdfWriter(fileName);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);


            Paragraph reportHeaderTour = new Paragraph($"Report for the TOUR: {tour.Name}")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetBold()
                    .SetFontColor(ColorConstants.CYAN);
            document.Add(reportHeaderTour);

            // Add line seperator.
            LineSeparator ls = new(new SolidLine());
            document.Add(ls);


            if (tour.Description == "")
                document.Add(new Paragraph("No description available for this tour."));
            else
                document.Add(new Paragraph(tour.Description));


            // Add new line to get space between data.
            document.Add(new Paragraph("\n"));

            // Table to display TOUR detailed data.
            Paragraph tourTableHeader = new Paragraph("Details of chosen TOUR:")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(14)
                    .SetBold()
                    .SetFontColor(ColorConstants.ORANGE);
            document.Add(tourTableHeader);
            Table tourTable = new Table(UnitValue.CreatePercentArray(2)).UseAllAvailableWidth();
            tourTable.AddHeaderCell(getHeaderCell("Starting Adress:"));
            tourTable.AddHeaderCell(tour.From.ToString());
            tourTable.SetFontSize(11).SetBackgroundColor(ColorConstants.WHITE);
            tourTable.AddCell(getHeaderCell("Destination:"));
            tourTable.AddCell(tour.To.ToString());
            tourTable.AddCell(getHeaderCell("Coordinates Start:"));
            tourTable.AddCell(tour.StartCoord.ToString());
            tourTable.AddCell(getHeaderCell("Coordinates End:"));
            tourTable.AddCell(tour.EndCoord.ToString());
            tourTable.AddCell(getHeaderCell("Route Type:"));
            tourTable.AddCell(tour.Transport.ToString());
            tourTable.AddCell(getHeaderCell("Distance:"));
            tourTable.AddCell(tour.Distance.ToString());
            tourTable.AddCell(getHeaderCell("Duration:"));
            tourTable.AddCell(tour.Duration.ToString());
            tourTable.AddCell(getHeaderCell("ID:"));
            tourTable.AddCell(tour.ID.ToString());
            document.Add(tourTable);

            // Add new line to get space between data.
            document.Add(new Paragraph("\n"));

            // Table to display TourLogs with regarding data.
            Paragraph tourlogsTableHeader = new Paragraph("List of TourLogs:")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN))
                    .SetFontSize(14)
                    .SetBold()
                    .SetFontColor(ColorConstants.ORANGE);
            document.Add(tourlogsTableHeader);

            foreach (TourLogs log in tour.TourLogs)
            {
                Table tourlogsTable = new Table(UnitValue.CreatePercentArray(6)).UseAllAvailableWidth();
                tourlogsTable.AddHeaderCell(getHeaderCell("Timestamp"));
                tourlogsTable.AddHeaderCell(getHeaderCell("Difficulty"));
                tourlogsTable.AddHeaderCell(getHeaderCell("Total Time"));
                tourlogsTable.AddHeaderCell(getHeaderCell("Rating"));
                tourlogsTable.AddHeaderCell(getHeaderCell("Comment"));
                tourlogsTable.AddHeaderCell(getHeaderCell("ID"));
                tourlogsTable.SetFontSize(9).SetBackgroundColor(ColorConstants.WHITE);
                tourlogsTable.AddCell(log.Timestamp.ToString());
                tourlogsTable.AddCell(log.Difficulty.ToString());
                tourlogsTable.AddCell(log.TotalTime.ToString());
                tourlogsTable.AddCell(log.Rating.ToString());
                tourlogsTable.AddCell(log.Comment);
                tourlogsTable.AddCell(log.TourID.ToString());
                document.Add(tourlogsTable);
            }

            // Add new line to get space between data.
            document.Add(new Paragraph("\n"));

            // Add explanation of Difficulty and Rating.
            Paragraph difficultyExplanation = new Paragraph("Difficulty: 1 - Warmup, 2 - Easy, 3 - Moderate, 4 - Hard, 5 - Expert\nRating: 1 - Worst, 2 - Bad, 3 - Weak, 4 - Improveable, 5 - Moderate, 6 - Advancement, 7 - Good, 8 - Excellent, 9 - Satisfying, 10 - Perfect")
                .SetFontSize(7)
                .SetFontColor(ColorConstants.GRAY);
            document.Add(difficultyExplanation);

            // Start on the next page with the upcoming data.
            document.Add(new AreaBreak());

            Paragraph reportHeaderImage = new Paragraph($"Map of chosen TOUR: {tour.Name}")
                    .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD))
                    .SetFontSize(20)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetBold()
                    .SetFontColor(ColorConstants.CYAN);
            document.Add(reportHeaderImage);

            // Add line seperator.
            LineSeparator ls2 = new(new SolidLine());
            document.Add(ls2);

            // Add new line to get space between data.
            document.Add(new Paragraph("\n"));

            // Get Image and add to document.
            ImageData imageData = ImageDataFactory.Create(mapImage);
            document.Add(new Image(imageData));

            // Page numbers
            int n = pdf.GetNumberOfPages();
            for (int i = 1; i <= n; i++)
            {
                document.ShowTextAligned(new Paragraph(String
                   .Format("page" + i + " of " + n)),
                   559, 25, i, TextAlignment.RIGHT,
                   VerticalAlignment.BOTTOM, 0);
            }

            document.Close();
        }

        private static Cell getHeaderCell(String s)
        {
            return new Cell().Add(new Paragraph(s)).SetBold().SetBackgroundColor(ColorConstants.GRAY);
        }

        private static double AverageDifficulty(List<TourLogs> tourlogs)
        {
            double number = 0;
            foreach (TourLogs logs in tourlogs)
            {
                var enumNumber = EnumCalculations.EnumDifficultyToDouble(logs.Difficulty);
                number += enumNumber;
            }
            return number / tourlogs.Count;
        }

        private static double AverageRating(List<TourLogs> tourlogs)
        {
            double number = 0;
            foreach (TourLogs logs in tourlogs)
            {
                var enumNumber = EnumCalculations.EnumRatingToDouble(logs.Rating);
                number += enumNumber;
            }
            return number / tourlogs.Count;
        }

        private static string AverageTime(List<TourLogs> tourLogs)
        {
            string convertingTime;
            double timeInSeconds = 0;
            foreach (TourLogs logs in tourLogs)
            {
                convertingTime = logs.TotalTime;
                timeInSeconds += StringTimeConverterToSeconds(convertingTime);
            }

            // Converting back to correct calculated and readable hh:mm:ss string.
            timeInSeconds = timeInSeconds / tourLogs.Count;

            return TimeSpan.FromSeconds(timeInSeconds).ToString();
        }


        private static double StringTimeConverterToSeconds(string time)
        {
            DateTime timeType = DateTime.Parse(time);
            double seconds = timeType.Second;
            double minutes = timeType.Minute * 60;
            double hours = timeType.Hour * 3600;
            return seconds + minutes + hours;
        }
    }
}
