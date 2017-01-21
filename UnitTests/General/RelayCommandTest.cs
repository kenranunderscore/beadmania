using beadmania.UI.MVVM;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace beadmania.UI.Tests.General
{
    [TestClass]
    public class RelayCommandTest
    {
        [TestMethod]
        public void Execute_executes_registered_logic()
        {
            int count = -1;
            RelayCommand cmd = new RelayCommand(c => count += (int)c);
            cmd.Execute(4);
            Assert.AreEqual(3, count);
        }

        [TestMethod]
        public void Can_execute_if_no_predicate_is_specified()
        {
            RelayCommand cmd = new RelayCommand(_ => { });
            Assert.IsTrue(cmd.CanExecute(new object()));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_throws_if_no_action_is_specified()
        {
            RelayCommand cmd = new RelayCommand(null);
        }
    }
}