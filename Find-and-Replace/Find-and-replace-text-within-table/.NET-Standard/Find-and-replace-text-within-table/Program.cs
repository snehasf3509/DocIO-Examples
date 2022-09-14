﻿using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.IO;
using System.Text.RegularExpressions;

namespace Find_and_replace_text_within_table
{
    class Program
    {
        static void Main(string[] args)
        {
            using (FileStream fileStreamPath = new FileStream(Path.GetFullPath(@"../../../Data/Input.docx"), FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                //Load the Input document.
                using (WordDocument document = new WordDocument(fileStreamPath, FormatType.Docx))
                {
                    //Access table in Word document.
                    WTable table = document.Sections[0].Body.ChildEntities[0] as WTable;
                    //Iterate through the rows of table.
                    foreach (WTableRow row in table.Rows)
                    {
                        //Iterate through the cells of rows.
                        foreach (WTableCell cell in row.Cells)
                        {
                            //Iterates through the paragraphs of the cell.
                            foreach (Entity ent in cell.ChildEntities)
                            {
                                if (ent.EntityType == EntityType.Paragraph)
                                {
                                    WParagraph paragraph = ent as WParagraph;
                                    //Find the selection of text inside the paragraph.
                                    TextSelection[] textSelections = document.FindAll("Suppliers", false, true);
                                    for (int i = 0; i < textSelections.Length; i++)
                                    {
                                        //Replace the specified regular expression with a TextSelection in the paragraph.
                                        paragraph.Replace(new Regex("^//(.*)"), textSelections[i]);
                                    }
                                }
                            }
                        }
                    }
                    using (FileStream outputFileStream = new FileStream(Path.GetFullPath("../../../Sample.docx"), FileMode.Create, FileAccess.ReadWrite))
                    {
                        //Save the document.
                        document.Save(outputFileStream, FormatType.Docx);
                    }
                }
            }
            
        }
    }
}
