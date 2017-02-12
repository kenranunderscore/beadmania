namespace beadmania.UI.UnitTests.MVVM
{
    using System.Reflection;
    using Logic.Model;
    using NUnit.Framework;

    [TestFixture]
    public class PropertyChangedNotifierTest
    {
        private static readonly MethodInfo SetPropertyHandle
            = typeof(PropertyChangedNotifier).GetMethod("SetProperty", BindingFlags.NonPublic | BindingFlags.Instance);

        [Test]
        public void SetProperty_sets_value_for_reference_types()
        {
            Bar bar = new Bar();
            TestNotifier vm = new TestNotifier
            {
                Bar = bar
            };
            bool result = InvokeSetProperty(vm, bar, new Bar(), nameof(TestNotifier.Bar));
            Assert.IsTrue(result);
        }

        [Test]
        public void SetProperty_does_nothing_if_references_are_identical_for_reference_types()
        {
            Bar bar = new Bar();
            TestNotifier vm = new TestNotifier
            {
                Bar = bar
            };
            bool result = InvokeSetProperty(vm, vm.Bar, vm.Bar, nameof(TestNotifier.Bar));
            Assert.IsFalse(result);
        }

        [Test]
        public void SetProperty_does_nothing_if_values_are_identical_for_value_types()
        {
            TestNotifier vm = new TestNotifier
            {
                Foo = 5
            };
            bool result = InvokeSetProperty(vm, vm.Foo, vm.Foo, nameof(TestNotifier.Foo));
            Assert.IsFalse(result);
        }

        [Test]
        public void SetProperty_sets_value_for_value_types()
        {
            TestNotifier vm = new TestNotifier
            {
                Foo = 5
            };
            bool result = InvokeSetProperty(vm, vm.Foo, 6, nameof(TestNotifier.Foo));
            Assert.IsTrue(result);
        }

        [Test]
        public void SetProperty_notifies_that_property_changed()
        {
            TestNotifier vm = new TestNotifier();
            int count = 0;
            vm.PropertyChanged += (sender, e) => ++count;
            InvokeSetProperty(vm, 0, 1, nameof(TestNotifier.Foo));
            Assert.AreEqual(1, count);
        }

        private class TestNotifier : PropertyChangedNotifier
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
                .Invoke(target, new object[] { originalValue, newValue, nameof(TestNotifier.Foo) });
        }
    }
}