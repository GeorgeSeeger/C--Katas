using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CodeWars {
    public class EsolangPaintfuck {
        private string code;
        private int codePointer;
        private int iterations;
        private bool[][] canvas;
        private int Y { get; set; }
        private int X { get; set; }
        public string Result { get; private set; }

        public static string Interpret(string code, int iterations, int width, int height) {
            return new EsolangPaintfuck(code, iterations, width, height).Result;
        }

        public EsolangPaintfuck(string code, int iterations, int width, int height) {
            this.code = code;
            this.iterations = iterations;
            this.canvas = new bool[height].Select(a => new bool[width]).ToArray();
            this.X = 0;
            this.Y = 0;
            this.Solve();
        }



        private void Solve() {
            while (this.iterations > 0 || this.codePointer >= this.code.Length) {
                var instruction = this.code[codePointer];
                DoInstruction(instruction);
            }
        }

        private void DoInstruction(char c) {
            switch (c) {
                case 'n':
                    this.Y -= 1;
                    this.iterations--;
                    return;
                case 's':
                    this.Y += 1;
                    this.iterations--;
                    return;
                case 'e':
                    this.X += 1;
                    this.iterations--;
                    return;
                case 'w':
                    this.X -= 1;
                    this.iterations--;
                    return;
                case '*':
                    this.canvas[this.Y][this.X] = !getBit();
                    this.iterations--;
                    return;
                case '[':
                    if (!getBit()) {
                        this.codePointer +=
                            this.code.Substring(this.codePointer).IndexOf("]", StringComparison.Ordinal) + 1;
                    }
                    this.iterations--;
                    return;
                case ']':
                    if (getBit()) {
                        this.codePointer = this.code.Substring(0, this.codePointer).LastIndexOf('['); //this doesn't find the matching pair :/
                    }
                    this.iterations--;
                    return;
            }
        }

        private bool getBit() {
            return this.canvas[this.Y][this.X];
        }
    }
}
