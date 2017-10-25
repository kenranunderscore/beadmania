namespace beadmania.UI.UnitTests.ViewModels
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using beadmania.UI.Services;
    using beadmania.UI.ViewModels;
    using Logic.IO;
    using Logic.Repositories;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class MainViewModelTest
    {
        [Test]
        public void Grid_is_shown_by_default()
        {
            var fileSystemService = new Mock<IFileSystemService>().Object;
            var dialogService = new Mock<IDialogService>().Object;
            var paletteRepository = new Mock<IPaletteRepository>().Object;
            MainViewModel vm = new MainViewModel(fileSystemService, paletteRepository, dialogService);
            Assert.IsTrue(vm.ShowGrid);
        }

        [Test]
        public void Can_toggle_grid_visibility()
        {
            var fileSystemService = new Mock<IFileSystemService>().Object;
            var dialogService = new Mock<IDialogService>().Object;
            var paletteRepository = new Mock<IPaletteRepository>().Object;
            MainViewModel vm = new MainViewModel(fileSystemService, paletteRepository, dialogService);
            vm.ShowGrid = false;
            Assert.IsFalse(vm.ShowGrid);
        }

        //[Test]
        //public void Changing_image_path_reloads_image_if_path_exists()
        //{
        //    string path = "foo.bar";
        //    using (var bmp = new Bitmap(2, 2))
        //    using (var stream = new MemoryStream())
        //    {
        //        bmp.Save(stream, ImageFormat.Bmp);
        //        var fileSystemServiceMock = new Mock<IFileSystemService>();
        //        fileSystemServiceMock.Setup(_ => _.FileExists(path)).Returns(true);
        //        fileSystemServiceMock.Setup(_ => _.OpenFile(path)).Returns(stream);
        //        var dialogService = new Mock<IDialogService>().Object;
        //        var paletteRepository = new Mock<IPaletteRepository>().Object;
        //        MainViewModel vm = new MainViewModel(fileSystemServiceMock.Object, paletteRepository, dialogService);
        //        vm.ImagePath = path;
        //        fileSystemServiceMock.Verify(_ => _.OpenFile(path), Times.Once());
        //        Assert.IsNotNull(vm.Pattern);
        //    }
        //}

        [Test]
        public void Setting_image_path_to_nonexistent_file_does_not_load_image()
        {
            var fileSystemServiceMock = new Mock<IFileSystemService>();
            fileSystemServiceMock.Setup(_ => _.FileExists(It.IsAny<string>())).Returns(false);
            var dialogService = new Mock<IDialogService>().Object;
            var paletteRepository = new Mock<IPaletteRepository>().Object;
            MainViewModel vm = new MainViewModel(fileSystemServiceMock.Object, paletteRepository, dialogService);
            vm.ImagePath = "some path";
            fileSystemServiceMock.Verify(_ => _.OpenFile(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void Opening_image_triggers_open_file_dialog()
        {
            var fileSystemService = new Mock<IFileSystemService>().Object;
            var dialogServiceMock = new Mock<IDialogService>();
            var paletteRepository = new Mock<IPaletteRepository>().Object;
            MainViewModel vm = new MainViewModel(fileSystemService, paletteRepository, dialogServiceMock.Object);
            vm.OpenImageCmd.Execute(null);
            dialogServiceMock.Verify(_ => _.ChooseFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Opening_image_sets_image_path()
        {
            var fileSystemService = new Mock<IFileSystemService>();
            var dialogServiceMock = new Mock<IDialogService>();
            var paletteRepository = new Mock<IPaletteRepository>().Object;
            dialogServiceMock.Setup(_ => _.ChooseFile(It.IsAny<string>(), It.IsAny<string>())).Returns("foo");
            MainViewModel vm = new MainViewModel(fileSystemService.Object, paletteRepository, dialogServiceMock.Object);
            vm.OpenImageCmd.Execute(null);
            Assert.AreEqual("foo", vm.ImagePath);
        }
    }
}