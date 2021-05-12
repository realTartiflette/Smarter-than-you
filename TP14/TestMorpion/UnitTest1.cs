using System;
using System.IO;
using Morpion;
using NUnit.Framework;

namespace TestMorpion
{
    
    /// This class hijacks Console.In so that it can be tested against. Usage:
    ///
    /// <code>
    /// using val output = new ConsoleInput("This is my stdin" + Environment.NewLine);
    /// // Something that takes stuff from stdin...
    /// </code>
    ///
    /// Inspired by: https://stackoverflow.com/a/13397075
    ///
    public class ConsoleInput : IDisposable
    {
        private readonly StringReader _stringReader;
        private readonly TextReader _originalInput;

        public ConsoleInput(string input)
        {
            _stringReader = new StringReader(input);
            _originalInput = Console.In;
            Console.SetIn(_stringReader);
        }

        public void Dispose()
        {
            Console.SetIn(_originalInput);
            _stringReader.Dispose();
        }
    }

    [TestFixture, Timeout(1500)]
    public class TestsDepth2
    {
        [Test]
        public void Test1()
        {
            Game game = Game.load_game("___o_ox_x", 2);
            using var input = new ConsoleInput("2");
            game.play();
            Assert.AreEqual("__oo_oxxx", game.state());
        }
        
        [Test]
        public void Test2()
        {
            Game game = Game.load_game("x____o___", 2);
            using var input = new ConsoleInput("2");
            game.play();
            Assert.AreEqual("x_o__o__x", game.state());
        }
        
        [Test]
        public void Test3()
        {
            Game game = Game.load_game("x_xo_o___", 2);
            using var input = new ConsoleInput("4");
            game.play();
            Assert.AreEqual("x_xooo___", game.state());
        }
    }
    
    
    [TestFixture, Timeout(1500)]
    public class TestsDepth3
    {
        [Test]
        public void Test1()
        {
            Game game = Game.load_game("_________", 3);
            using var input1 = new ConsoleInput("2");
            game.play();
            Assert.AreEqual("x_o______", game.state());
            using var input2 = new ConsoleInput("8");
            game.play();
            Assert.AreEqual("x_o__x__o", game.state());
            using var input3 = new ConsoleInput("6");
            game.play();
            Assert.AreEqual( "xxo__xo_o", game.state());
            using var input4 = new ConsoleInput("4");
            game.play();
            Assert.AreEqual( "xxo_oxo_o", game.state());
        }
        
        [Test]
        public void Test2()
        {
            Game game = Game.load_game("_________", 3);
            using var input1 = new ConsoleInput("0");
            game.play();
            Assert.AreEqual("ox_______", game.state());
            using var input2 = new ConsoleInput("8");
            game.play();
            Assert.AreEqual("ox__x___o", game.state());
            using var input3 = new ConsoleInput("2");
            game.play();
            Assert.AreEqual( "oxo_x__xo", game.state());
        }
        
        [Test]
        public void Test3()
        {
            Game game = Game.load_game("_________", 3);
            using var input1 = new ConsoleInput("0");
            game.play();
            Assert.AreEqual("ox_______", game.state());
            using var input2 = new ConsoleInput("8");
            game.play();
            Assert.AreEqual("ox__x___o", game.state());
            using var input3 = new ConsoleInput("7");
            game.play();
            Assert.AreEqual( "ox__x_xoo", game.state());
            using var input4 = new ConsoleInput("2");
            game.play();
            Assert.AreEqual("oxo_xxxoo", game.state());
            using var input5 = new ConsoleInput("3");
            game.play();
            Assert.AreEqual("oxooxxxoo", game.state());
        }
    }
    
    
    [TestFixture, Timeout(1500)]
    public class TestsDepth5
    {
        [Test]
        public void Test1()
        {
            Game game = Game.load_game("_________", 5);
            using var input1 = new ConsoleInput("4");
            game.play();
            Assert.AreEqual("x___o____", game.state());
        }

        [Test]
        public void Test2()
        {
            Game game = Game.load_game("_________", 5);
            using var input1 = new ConsoleInput("0");
            game.play();
            Assert.AreEqual("o___x____", game.state());
            using var input2 = new ConsoleInput("8");
            game.play();
            Assert.AreEqual("ox__x___o", game.state());
            using var input3 = new ConsoleInput("2");
            game.play();
            Assert.AreEqual( "oxo_x__xo", game.state());
        }
        
        [Test]
        public void Test3()
        {
            Game game = Game.load_game("_________", 5);
            using var input1 = new ConsoleInput("2");
            game.play();
            Assert.AreEqual("__o" +
                            "_x_" +
                            "___", game.state());
            using var input2 = new ConsoleInput("8");
            game.play();
            Assert.AreEqual( "__o" +
                             "_xx" +
                             "__o", game.state());
            using var input3 = new ConsoleInput("3");
            game.play();
            Assert.AreEqual( "x_o" +
                             "oxx" +
                             "__o", game.state());
        }
    }
}