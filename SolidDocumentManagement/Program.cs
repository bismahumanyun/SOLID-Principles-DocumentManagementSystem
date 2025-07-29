using System;
using System.Collections.Generic;

namespace SolidDocumentManagement
{
    // SRP: Single Responsibility Principle
    public class Document
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DocumentType Type { get; set; }
    }

    public enum DocumentType { Pdf, Word, Image }

    // OCP: Open/Closed Principle
    public interface IDocumentProcessor
    {
        bool CanProcess(DocumentType type);
        void Process(Document document);
    }

    // LSP: Liskov Substitution Principle
    public class PdfProcessor : IDocumentProcessor
    {
        public bool CanProcess(DocumentType type) => type == DocumentType.Pdf;
        public void Process(Document document)
        {
            Console.WriteLine($"Processing PDF document: {document.Title}");
            document.Content = "PDF Processed: " + document.Content;
        }
    }

    public class WordProcessor : IDocumentProcessor
    {
        public bool CanProcess(DocumentType type) => type == DocumentType.Word;
        public void Process(Document document)
        {
            Console.WriteLine($"Processing Word document: {document.Title}");
            document.Content = "WORD Processed: " + document.Content;
        }
    }

    // ISP: Interface Segregation Principle
    public interface ICloudStorage
    {
        void Upload(Document doc);
    }

    public interface ILocalStorage
    {
        void SaveToDisk(Document doc);
    }

    public class AwsStorage : ICloudStorage
    {
        public void Upload(Document doc)
        {
            Console.WriteLine($"Uploading {doc.Type} document '{doc.Title}' to AWS");
        }
    }

    public class LocalDiskStorage : ILocalStorage
    {
        public void SaveToDisk(Document doc)
        {
            Console.WriteLine($"Saving {doc.Type} document '{doc.Title}' to local disk");
        }
    }

    // DIP: Dependency Inversion Principle
    public class DocumentService
    {
        private readonly IDocumentProcessor _processor;
        private readonly ICloudStorage _storage;

        public DocumentService(IDocumentProcessor processor, ICloudStorage storage)
        {
            _processor = processor;
            _storage = storage;
        }

        public void HandleDocument(Document doc)
        {
            if (_processor.CanProcess(doc.Type))
            {
                _processor.Process(doc);
                _storage.Upload(doc);
            }
            else
            {
                Console.WriteLine($"No processor available for {doc.Type}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SOLID Document Management System");
            Console.WriteLine("===============================\n");

            // Create some sample documents
            var documents = new List<Document>
            {
                new Document { Title = "Annual Report", Type = DocumentType.Pdf, Content = "Financial data..." },
                new Document { Title = "Project Proposal", Type = DocumentType.Word, Content = "Project details..." },
                new Document { Title = "Profile Picture", Type = DocumentType.Image, Content = "Image data..." }
            };

            // Create processors
            var pdfProcessor = new PdfProcessor();
            var wordProcessor = new WordProcessor();

            // Create storage
            var awsStorage = new AwsStorage();
            var localStorage = new LocalDiskStorage();

            // Demonstrate SRP: Separate DocumentService handles coordination
            var pdfService = new DocumentService(pdfProcessor, awsStorage);
            var wordService = new DocumentService(wordProcessor, awsStorage);

            // Process documents
            foreach (var doc in documents)
            {
                Console.WriteLine($"\nProcessing {doc.Type} document: {doc.Title}");

                // OCP: We can easily add new processors without changing existing code
                if (doc.Type == DocumentType.Pdf)
                {
                    pdfService.HandleDocument(doc);
                }
                else if (doc.Type == DocumentType.Word)
                {
                    wordService.HandleDocument(doc);
                }
                else
                {
                    // LSP: We could add new processors that adhere to IDocumentProcessor
                    Console.WriteLine($"No processor available for {doc.Type}");
                }

                // ISP: Different storage options available
                if (doc.Type == DocumentType.Pdf)
                {
                    localStorage.SaveToDisk(doc);
                }
            }

            Console.WriteLine("\nProcessing complete!");
        }
    }
}