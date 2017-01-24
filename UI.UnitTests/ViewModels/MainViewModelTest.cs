﻿using beadmania.UI.Services;
using beadmania.UI.ViewModels;
using Moq;
using NUnit.Framework;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace beadmania.UI.UnitTests.ViewModels
{
    [TestFixture]
    public class MainViewModelTest
    {
        [Test]
        public void Grid_is_shown_by_default()
        {
            var ioService = new Mock<IIOService>().Object;
            MainViewModel vm = new MainViewModel(ioService);
            Assert.IsTrue(vm.ShowGrid);
        }

        [Test]
        public void Can_toggle_grid_visibility()
        {
            var ioService = new Mock<IIOService>().Object;
            MainViewModel vm = new MainViewModel(ioService);
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
                var ioServiceMock = new Mock<IIOService>();
                ioServiceMock.Setup(_ => _.FileExists(path)).Returns(true);
                ioServiceMock.Setup(_ => _.OpenFile(path)).Returns(stream);
                MainViewModel vm = new MainViewModel(ioServiceMock.Object);
                vm.ImagePath = path;
                ioServiceMock.Verify(_ => _.OpenFile(path), Times.Once());
                Assert.IsNotNull(vm.Bitmap);
            }
        }

        [Test]
        public void Setting_image_path_to_nonexistent_file_does_not_load_image()
        {
            var ioServiceMock = new Mock<IIOService>();
            ioServiceMock.Setup(_ => _.FileExists(It.IsAny<string>())).Returns(false);
            MainViewModel vm = new MainViewModel(ioServiceMock.Object);
            vm.ImagePath = "some path";
            ioServiceMock.Verify(_ => _.OpenFile(It.IsAny<string>()), Times.Never());
        }

        [Test]
        public void Opening_image_triggers_open_file_dialog()
        {
            var ioServiceMock = new Mock<IIOService>();
            MainViewModel vm = new MainViewModel(ioServiceMock.Object);
            vm.OpenImageCmd.Execute(null);
            ioServiceMock.Verify(_ => _.ChooseFile(It.IsAny<string>(), It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Opening_image_sets_image_path()
        {
            var ioServiceMock = new Mock<IIOService>();
            ioServiceMock.Setup(_ => _.ChooseFile(It.IsAny<string>(), It.IsAny<string>())).Returns("foo");
            MainViewModel vm = new MainViewModel(ioServiceMock.Object);
            vm.OpenImageCmd.Execute(null);
            Assert.AreEqual("foo", vm.ImagePath);
        }
    }
}