using beadmania.UI.MVVM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace beadmania.UI.UnitTests.MVVM
{
    [TestClass]
    public class BaseViewModelTest
    {
        private static readonly MethodInfo SetPropertyHandle
            = typeof(ViewModel).GetMethod("SetProperty", BindingFlags.NonPublic | BindingFlags.Instance);

        [TestMethod]
        public void SetProperty_sets_value_for_reference_types()
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
        public void SetProperty_does_nothing_if_references_are_identical_for_reference_types()
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
        public void SetProperty_does_nothing_if_values_are_identical_for_value_types()
        {
            TestViewModel vm = new TestViewModel
            {
                Foo = 5
            };
            bool result = InvokeSetProperty(vm, vm.Foo, vm.Foo, nameof(TestViewModel.Foo));
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void SetProperty_sets_value_for_value_types()
        {
            TestViewModel vm = new TestViewModel
            {
                Foo = 5
            };
            bool result = InvokeSetProperty(vm, vm.Foo, 6, nameof(TestViewModel.Foo));
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void SetProperty_notifies_that_property_changed()
        {
            TestViewModel vm = new TestViewModel();
            int count = 0;
            vm.PropertyChanged += (sender, e) => ++count;
            InvokeSetProperty(vm, 0, 1, nameof(TestViewModel.Foo));
            Assert.AreEqual(1, count);
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