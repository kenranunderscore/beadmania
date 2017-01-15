using beadmania.UI.General;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.UI.General
{
    [TestClass]
    public class BaseViewModelTest
    {
        private static readonly MethodInfo SetPropertyHandle
            = typeof(ViewModel).GetMethod("SetProperty", BindingFlags.NonPublic | BindingFlags.Instance);

        [TestMethod]
        public void SetProperty_Sets_Value_For_Reference_Types()
        {
            Bar bar = new Bar();
            TestViewModel vm = new TestViewModel
            {
                Bar = bar
            };
            bool result = InvokeSetProperty(vm, bar, new Bar(), nameof(TestViewModel.Bar));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SetProperty_Does_Nothing_If_References_Are_Identical_For_Reference_Types()
        {
            Bar bar = new Bar();
            TestViewModel vm = new TestViewModel
            {
                Bar = bar
            };
            bool result = InvokeSetProperty(vm, vm.Bar, vm.Bar, nameof(TestViewModel.Bar));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SetProperty_Does_Nothing_If_Values_Are_Identical_For_Value_Types()
        {
            TestViewModel vm = new TestViewModel
            {
                Foo = 5
            };
            bool result = InvokeSetProperty(vm, vm.Foo, vm.Foo, nameof(TestViewModel.Foo));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SetProperty_Sets_Value_For_Value_Types()
        {
            TestViewModel vm = new TestViewModel
            {
                Foo = 5
            };
            bool result = InvokeSetProperty(vm, vm.Foo, 6, nameof(TestViewModel.Foo));
            Assert.IsTrue(result);
        }

        private class TestViewModel : ViewModel
        {
            public int Foo { get; set; }
            public Bar Bar { get; set; }
        }

        private class Bar
        {
        }

        private static bool InvokeSetProperty<T>(object target, T originalValue, T newValue, string propertyName)
        {
            return (bool)SetPropertyHandle.MakeGenericMethod(typeof(T))
                .Invoke(target, new object[] { originalValue, newValue, nameof(TestViewModel.Foo) });
        }
    }
}