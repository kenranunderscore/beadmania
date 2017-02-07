namespace beadmania.UI.UnitTests.MVVM
{
    using System;
    using beadmania.UI.MVVM;
    using NUnit.Framework;

    [TestFixture]
    public class RelayCommandTest
    {
        [Test]
        public void Execute_executes_registered_logic()
        {
            int count = -1;
            RelayCommand cmd = new RelayCommand(c => count += (int)c);
            cmd.Execute(4);
            Assert.AreEqual(3, count);
        }

        [Test]
        public void Can_execute_if_no_predicate_is_specified()
        {
            RelayCommand cmd = new RelayCommand(_ => { });
            Assert.IsTrue(cmd.CanExecute(new object()));
        }

        [Test]
        public void Constructor_throws_if_no_action_is_specified()
        {
            Action create = () => new RelayCommand(null);
            Assert.Throws<ArgumentNullException>(new TestDelegate(create));
        }
    }
}