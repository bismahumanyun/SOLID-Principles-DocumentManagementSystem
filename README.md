# SOLID Document Management System

A practical demonstration of SOLID principles in C# .NET, implemented as a console application for document processing.
[SOLID Principles]
Features
Demonstrates all five SOLID principles in a working application
Processes different document types (PDF, Word, Image)
Multiple storage options (Cloud, Local Disk)
Easy to extend with new document types and processors

SOLID Principles Demonstrated

| Principle | Implementation Example |
|-----------|------------------------|
| \*\*Single Responsibility\*\* | Separate classes for document data, processing, and storage |
| \*\*Open/Closed\*\* | New document processors can be added without modifying existing code |
| \*\*Liskov Substitution\*\* | All processors implement `IDocumentProcessor` and are interchangeable |
| \*\*Interface Segregation\*\* | Storage providers implement only relevant interfaces |
| \*\*Dependency Inversion\*\* | High-level `DocumentService` depends on abstractions not concretions |

Getting Started

Prerequisites
Visual Studio 2019/2022
.NET 9.0 SDK

Installation
1\. Clone the repository:
```bash
git clone https://github.com/bismahumanyun/SOLID-Principles-DocumentManagementSystem.git 
Open the solution in Visual Studio
Build the solution (Ctrl+Shift+B)
Run the application (F5)

Code Structure
SolidDocumentManagement/
├── Program.cs             - Main application entry point
├── Document.cs            - Document model (SRP)
├── Processors/            - Document processors (OCP, LSP)
│   ├── PdfProcessor.cs
│   └── WordProcessor.cs
├── Storage/               - Storage implementations (ISP)
│   ├── ICloudStorage.cs
│   ├── ILocalStorage.cs
│   ├── AwsStorage.cs
│   └── LocalDiskStorage.cs
└── Services/              - High-level services (DIP
    └── DocumentService.cs

