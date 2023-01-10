using Aspose.Words;
using Aspose.Words.Drawing.Charts;
using Aspose.Words.Tables;
using System.Diagnostics;

namespace CreateWordDocument
{
    class Program
    {
        static Dictionary<string, string> coverPageDict = new Dictionary<string, string>(){
            {"EntityName", "Bangladesh" },
            {"BranchAuditMonthName", "Dhaka August" },
            {"Report#", "Report 123456" },
            {"AuditPerformer", "Md. Sahidul Islam" },
            {"AuditIssuer", "Pulok Bhai" },
            {"AuditDate", "21/03/2022"} };

        static string description = @" Le Lorem Ipsum est simplement du faux texte employé dans la composition et la mise en page avant impression. Le Lorem Ipsum est le faux texte standard de l'imprimerie depuis les années 1500, quand un imprimeur anonyme assembla ensemble des morceaux de texte pour réaliser un livre spécimen de polices de texte. Il n'a pas fait que survivre cinq siècles, mais s'est aussi adapté à la bureautique informatique, sans que son contenu n'en soit modifié. Il a été popularisé dans les années 1960 grâce à la vente de feuilles Letraset contenant des passages du Lorem Ipsum, et, plus récemment, par son inclusion dans des applications de mise en page de texte, comme Aldus PageMaker.";

        static Dictionary<string, string> detailedIssueTableContent = new Dictionary<string, string>{
            { "IssueTitle", "Shezan Bhai" },
            { "IssueOwner", "Rahimin bhai" },
            { "IssueRating", "Five star" },
            { "IssueTargetDate", "23-03-2022" },
            { "IssueDescription", description }};


        static void update_texts(Document doc, DocumentBuilder builder, bool coverPage = true)
        {
            foreach (Paragraph paragraph in doc.GetChildNodes(NodeType.Paragraph, true))
            {
                var text = paragraph.GetText().Trim();
                if (coverPage && coverPageDict.ContainsKey(paragraph.GetText().Trim()))
                {
                    if (paragraph.GetText().Trim() == "Entity Name")
                        coverPage = false;
                    builder.MoveTo(paragraph);
                    builder.Writeln("");
                    paragraph.Remove();
                    builder.Write(coverPageDict[paragraph.GetText().Trim()]);
                }
            }
        }

        static void insert_chart(Document doc, DocumentBuilder builder)
        {
            //use bookmark for chart
            //Bookmark bookmark = doc.Range.Bookmarks["Chart"];
            builder.MoveToBookmark("Chart");
            Aspose.Words.Drawing.Shape shape = builder.InsertChart(ChartType.Column, 432, 252);

            // Chart property of Shape contains all chart related options.
            Chart chart = shape.Chart;

            // Clear demo data.
            chart.Series.Clear();

            // Fill data.
            chart.Series.Add("AW Series 1",
                new string[] { "Rare", "Unlikely", "Moderate", "Likely", "Almost Certain" },
                new double[] { 1.2, 0.3, 2.1, 2.9, 4.2 });
        }

        static void edit_table(Document doc, DocumentBuilder builder)
        {

            builder.MoveToCell(1, 0, 1, -1);
            builder.Font.Name = "Arial";
            builder.Font.Bold = false;
            builder.Font.Size = 10;
            builder.Write(detailedIssueTableContent["IssueTitle"]);
            builder.MoveToCell(1, 1, 1, -1);
            builder.Write(detailedIssueTableContent["IssueOwner"]);
            builder.MoveToCell(1, 4, 0, -1);
            builder.Write(detailedIssueTableContent["IssueDescription"]);
        }

        static void add_header(Document doc, DocumentBuilder builder, int sectionNumber)
        {
            Section currentSection = doc.Sections[sectionNumber];
            PageSetup pageSetup = currentSection.PageSetup;

            // --- Create header ---
            pageSetup.HeaderDistance = 20;
            builder.MoveToHeaderFooter(HeaderFooterType.HeaderPrimary);
            builder.ParagraphFormat.Alignment = ParagraphAlignment.Right;

            // Set font properties for header text.
            builder.Font.Name = "Arial";
            builder.Font.Bold = true;
            builder.Font.Size = 10;
            // Specify header title for the first page.
            builder.Write(coverPageDict["Report#"]);
            Section section = doc.Sections[1];
            Section previousSection = (Section)section.PreviousSibling;
            section.HeadersFooters.Clear();
            foreach (HeaderFooter headerFooter in previousSection.HeadersFooters)
                section.HeadersFooters.Add(headerFooter.Clone(true));
        }

        static void keepingATableFromBreakingAcrossPages(Document doc, DocumentBuilder builder)
        {
            //snippets
            //https://docs.aspose.com/words/java/keeping-tables-and-rows-from-breaking-across-pages/#keeping-a-table-from-breaking-across-page
        }
        static void insertTitleHeading(Document doc, DocumentBuilder builder)
        {
            builder.MoveToDocumentEnd();
            builder.Font.Name = "Arial";
            builder.Font.Bold = true;
            builder.Font.Size = 10;
            builder.ParagraphFormat.StyleName = "Heading 2";
            // Specify header title for the first page.
            builder.Write("New Section");
        }

        static void Main(string[] args)
        {
            Document doc = new Document(@"C:\Users\sahidul\VSProjects\WordGenerator\Temp.docx");


            DocumentBuilder builder = new DocumentBuilder(doc);
            add_header(doc, builder, 0);
            //add_header(doc, builder, 1);
            update_texts(doc, builder);
            insert_chart(doc, builder);
            edit_table(doc, builder);

            insertTitleHeading(doc, builder);
            doc.UpdateFields();
            doc.Save(@"C:\Users\sahidul\VSProjects\WordGenerator\Output.docx");
        }
    }
}