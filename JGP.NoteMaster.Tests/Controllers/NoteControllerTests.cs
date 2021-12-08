using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGP.NoteMaster.Tests.Controllers
{
    using Core;
    using Data.EntityFramework;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using JGP.NoteMaster.Api.Controllers;
    using NUnit.Framework;
    using Services;
    using Web.Models;

    public class NoteControllerTests
    {
        private NoteContext _context;

        private NoteController _controller;

        private INoteService _service;

        [SetUp]
        public void SetupTest()
        {
            _context = TestHelpers.BuildContext();
            _context.InitiateDatabase();
            _service = (INoteService)_context.GetService();
            _controller = new NoteController(_service);
        }

        [TearDown]
        public void TeardownTest()
        {
            _service.Dispose();
        }

        private void ForceModelStateError()
        {
            _controller.ModelState.AddModelError("Key", "ErrorMessage");
        }

        [Test]
        public async Task HelloWorld()
        {
            var response = await _controller.Hello();
            response.Should().Be("Hello World!");
        }

        [Test]
        public async Task AddNote()
        {
            var categoryCmd = TestHelpers.GetCategoryCreateCommand();
            var category = new Category(categoryCmd);

            var noteCmd = TestHelpers.GetNoteCreateCommand(categoryId:category.Id);
            var note = new Note(noteCmd);
            note.Category = category;
            var model = new NoteModel(note);

            var response = await _controller.CreateNoteAsync(model);

            using (new AssertionScope())
            {
                response.IsSuccess.Should().BeTrue();
                _context.Categories.Count().Should().Be(1);
                _context.Notes.Count().Should().Be(1);
            }
        }

        [Test]
        public async Task AddNoteToExistingCategory()
        {
            var categoryCmd = TestHelpers.GetCategoryCreateCommand();
            var category = new Category(categoryCmd);

            var noteCmd = TestHelpers.GetNoteCreateCommand(categoryId: category.Id);
            var note = new Note(noteCmd);
            note.Category = category;
            var model = new NoteModel(note);

            var response = await _controller.CreateNoteAsync(model);


            var newNoteCmd = TestHelpers.GetNoteCreateCommand(categoryId: category.Id);
            var newNote = new Note(newNoteCmd);
            note.Category = category;
            var newModel = new NoteModel(newNote);

            var newResponse = await _controller.CreateNoteAsync(newModel);

            using (new AssertionScope())
            {
                response.IsSuccess.Should().BeTrue();
                _context.Categories.Count().Should().Be(1);
                _context.Notes.Count().Should().Be(2);
            }
        }

    }
}
