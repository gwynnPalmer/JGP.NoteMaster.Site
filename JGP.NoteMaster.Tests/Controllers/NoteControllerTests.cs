namespace JGP.NoteMaster.Tests.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Api.Controllers;
    using Core;
    using Data.EntityFramework;
    using FluentAssertions;
    using FluentAssertions.Execution;
    using NUnit.Framework;
    using Services;
    using Web.Models;

    /// <summary>
    ///     Class NoteControllerTests.
    /// </summary>
    public class NoteControllerTests
    {
        /// <summary>
        ///     The context
        /// </summary>
        private NoteContext _context;

        /// <summary>
        ///     The controller
        /// </summary>
        private NoteController _controller;

        /// <summary>
        ///     The service
        /// </summary>
        private INoteService _service;

        /// <summary>
        ///     Setups the test.
        /// </summary>
        [SetUp]
        public void SetupTest()
        {
            _context = TestHelpers.BuildContext();
            _context.InitiateDatabase();
            _service = (INoteService)_context.GetService();
            _controller = new NoteController(_service);
        }

        /// <summary>
        ///     Teardowns the test.
        /// </summary>
        [TearDown]
        public void TeardownTest()
        {
            _service.Dispose();
        }

        /// <summary>
        ///     Forces the model state error.
        /// </summary>
        private void ForceModelStateError()
        {
            _controller.ModelState.AddModelError("Key", "ErrorMessage");
        }

        /// <summary>
        ///     Defines the test method HelloWorld.
        /// </summary>
        [Test]
        public async Task HelloWorld()
        {
            var response = await _controller.Hello();
            response.Should().Be("Hello World!");
        }

        /// <summary>
        ///     Defines the test method AddNote.
        /// </summary>
        [Test]
        public async Task AddNote()
        {
            var categoryCmd = TestHelpers.GetCategoryCreateCommand();
            var category = new Category(categoryCmd);

            var noteCmd = TestHelpers.GetNoteCreateCommand(categoryId: category.Id);
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

        /// <summary>
        ///     Defines the test method AddNoteToExistingCategory.
        /// </summary>
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