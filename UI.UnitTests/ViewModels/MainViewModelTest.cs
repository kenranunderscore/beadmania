namespace beadmania.UI.UnitTests.ViewModels
{
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using beadmania.UI.Services;
    using beadmania.UI.ViewModels;
    using Logic.IO;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class MainViewModelTest
    {
        [Test]
        public void Grid_is_shown_by_default()
        {
            var ioService = new Mock<IFileSystemService>().Object;
            var dialogService = new Mock<IDialogService>().Object;
            MainViewModel vm = new MainViewModel(ioService, dialogService);
            Assert.IsTrue(vm.ShowGrid);
        }

        [Test]
        public void Can_toggle_grid_visibility()
        {
            var ioService = new Mock<IFileSystemService>().Object;
            var dialogService = new Mock<IDialogService>().Object;
            MainViewModel vm = new MainViewModel(ioService, dialogService);
            vm.ShowGrid = false;
            Assert.IsFalse(vm.ShowGrid);
        }

        [Test]
        public void Changing_image_path_reloads_image_if_path_exists()
        {
            string path = "foo.bar";
            using (var bmp = new Bitmap(2, 2))
            using (var stream = new MemoryStream())
            {
                bmp.Save(stream, ImageFormat.Bmp);
                var ioServiceMock = new Mock<IFileSystemService>();
                ioServiceMock.Setup(_ => _.FileExists(path)).Returns(true);
                ioServiceMock.Setup(_ => _.OpenFile(path)).Returns(stream);
                var dialogService = new Mock<IDialogService>().Object;
                MainViewModel vm = new MainViewModel(ioServiceMock.Object, dialogService);
                vm.ImagePath = path;
                ioServiceMock.Verify(_ => _.OpenFile(path), Times.Once());
                Assert.IsNotNull(vm.Pattern);
            }
        }

        [Test]
        public void Setting_image_path_to_nonexistent_file_does_not_load_image()
        {
            var ioServiceMock = new Mock<IFileSystemService>();
            ioServiceMock.Setup(_ => _.FileExists(It.IsAny<string>())).Returns(false);
            var dialogService = new Mock<IDialogService>().Object;
            MainViewModel vm = new MainViewModel(ioServiceMock.Object, dialogService);
            vm.ImagePath = "some path";
            ioServiceMock.Verify(_ => _.OpenFile(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void Opening_image_triggers_open_file_dialog()
        {
            var ioServiceMock = new Mock<IFileSystemService>();
            var dialogServiceMock = new Mock<IDialogService>();
            MainViewModel vm = new MainViewModel(ioServiceMock.Object, dialogServiceMock.Object);
            vm.OpenImageCmd.Execute(null);
            dialogServiceMock.Verify(_ => _.ChooseFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Opening_image_sets_image_path()
        {
            var ioServiceMock = new Mock<IFileSystemService>();
            var dialogServiceMock = new Mock<IDialogService>();
            dialogServiceMock.Setup(_ => _.ChooseFile(It.IsAny<string>(), It.IsAny<string>())).Returns("foo");
            MainViewModel vm = new MainViewModel(ioServiceMock.Object, dialogServiceMock.Object);
            vm.OpenImageCmd.Execute(null);
            Assert.AreEqual("foo", vm.ImagePath);
        }
    }
}