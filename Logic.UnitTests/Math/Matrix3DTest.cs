﻿using beadmania.Logic.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace beadmania.Logic.UnitTests.Math
{
    [TestClass]
    public class Matrix3DTest
    {
        private Matrix3D m = new Matrix3D(new double[,]
        {
            { -1d, 0d, 0.5d },
            { 0d, 2d, 0d },
            { 1d, -1d, 0d },
        });

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_throws_if_first_dimension_is_not_3()
        {
            var array = new double[2, 3];
            Matrix3D matrix = new Matrix3D(array);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_throws_if_second_dimension_is_not_3()
        {
            var array = new double[3, 7];
            Matrix3D matrix = new Matrix3D(array);
        }
    }
}