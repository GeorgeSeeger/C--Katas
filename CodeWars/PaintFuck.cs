using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars {
    public class PaintFuck {
        private string code;
        private int codePointer;
        private int iterations;
        private bool[][] canvas;
        private int Y { get; set; }
        private int X { get; set; }
        public string Result { get; private set; }

        public static string Interpret(string code, int iterations, int width, int height) {
            return new PaintFuck(code, iterations, width, height).Result;
        }

        public PaintFuck(string code, int iterations, int width, int height) {
            this.code = code;
            this.codePointer = 0;
            this.iterations = iterations;
            this.canvas = new bool[height].Select(a => new bool[width]).ToArray();
            this.X = 0;
            this.Y = 0;
            this.Interpret();
        }



        private void Interpret() {
            while (this.iterations > 0 && this.codePointer < this.code.Length) {
                var instruction = this.code[codePointer];
                DoInstruction(instruction);
                this.iterations--;
                this.codePointer++;
                ToroidalWrap();
            }
            SaveToResult();
        }

        private void DoInstruction(char c) {
            switch (c) {
                case 'n':
                    this.Y -= 1;
                    return;
                case 's':
                    this.Y += 1;
                    return;
                case 'e':
                    this.X += 1;
                    return;
                case 'w':
                    this.X -= 1;
                    return;
                case '*':
                    this.canvas[this.Y][this.X] = !GetBit();
                    return;
                case '[':
                    if (!GetBit()) {
                        GoToMatching(']');
                    }
                    return;
                case ']':
                    if (GetBit()) {
                        GoToMatching('[');
                    }
                    return;
                default:
                    this.iterations++;
                    return;
            }
        }

        private bool GetBit() {
            return this.canvas[this.Y][this.X];
        }

        private void GoToMatching(char bracket) {
            if (bracket != '[' && bracket != ']') throw new ArgumentException();
            var finder = -1;
            while (finder < 0) {
                if (this.code[this.codePointer] == (bracket == ']' ? '[' : ']')) finder++;
                if (this.code[this.codePointer] == (bracket == ']' ? ']' : '[')) finder--;
                if (bracket == ']' && finder < 0) this.codePointer--;
                else if(bracket == '[' && finder < 0) this.codePointer++;
            }           
        }

        private void ToroidalWrap() {
            if (this.X < 0) this.X = this.canvas.First().Length - 1;
            if (this.X >= this.canvas.First().Length) this.X = 0;
            if (this.Y < 0) this.Y = this.canvas.Length - 1;
            if (this.Y >= this.canvas.Length) this.Y = 0;
        }

        private void SaveToResult() {
            this.Result = string.Join("\r\n", this.canvas.Select(a => string.Join("", a.Select(b => b ? '1' : '0'))));
        }
    }

    public class PaintfuckTests {
        [Fact]
        public void Tests() {
            Assert.Equal("000000\r\n000000\r\n000000\r\n000000\r\n000000\r\n000000\r\n000000\r\n000000\r\n000000",
                PaintFuck.Interpret("*e*e*e*es*es*ws*ws*w*w*w*n*n*n*ssss*s*s*s*", 0, 6, 9));
            Assert.Equal("111100\r\n100010\r\n100001\r\n100010\r\n111100\r\n100000\r\n100000\r\n100000\r\n100000", PaintFuck.Interpret("*e*e*e*es*es*ws*ws*w*w*w*n*n*n*ssss*s*s*s*", 100, 6, 9));
        }
    }
}
