using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SmartKnowledgeHub.API.Data;
using SmartKnowledgeHub.API.Services;
using SmartKnowledgeHub.API.Models;

namespace SmartKnowledgeHub.API.Tests.Services
{
    public class DocumentServiceTest
    {

        [Fact]
        public async Task GetDocumentByIdAsync_ReturnsDocument_WhenDocumentExistes()
        {

            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase("DocumentServiceTestDB").Options;

            using var context = new AppDbContext(options);

            var document = new Document
            {
                Id = 1,
                Title = "Test Document",
                Content = "This is a test document.",
                CreatedAt = DateTime.UtcNow
            };
            context.Documents.Add(document);
            await context.SaveChangesAsync();

            var service = new DocumentService(context);

            var result = await service.GetDocumentByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test Document", result.Title);
           

        }
    }
}
