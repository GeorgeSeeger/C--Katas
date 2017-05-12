using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeWars
{
    public class TextAlign
    {
        public static string Justify(string str, int len)
        {
            var lines = new List<List<string>>();
            var line = new List<string>();
            var words = str.Split(' ');
            for (var k = 0; k < words.Length; k ++)
            {
                var word = words[k];
                var charCount = line.Count > 0?
                    line.Select(s => s.Length).Aggregate((i, a) => i + a)
                    : 0 ;
                if (charCount > len - line.Count - word.Length)
                {
                    k--;
                    lines.Add(line);
                    line = new List<string>();
                } else {
                    line.Add(word);
                }
            }
            lines.Add(line);
            for (var j = 0; j < lines.Count; j++)
            {
                var uLine = lines[j];
                var charCount = uLine.Select(s => s.Length).Aggregate((i, a) => i + a);
                var spaces = len - charCount;
                var spaceList = j == lines.Count - 1
                    ? Enumerable.Repeat(1, uLine.Count - 1).ToList()
                    : RemainderDivide(spaces, uLine.Count - 1);
                for (int i = 0; i < spaceList.Count; i++)
                {
                    var numSpaces = spaceList[i];
                    uLine[i] = uLine[i] + String.Concat(Enumerable.Repeat(" ", numSpaces));
                }
            }
            return string.Join("\n", lines.Select(li => String.Join("", li)));
        }


        public static List<int> RemainderDivide(int num, int count)
        {
            var answer = new List<int>();
            if (num == 0 || count == 0) return answer;
            var divided = (int) Math.Ceiling((double) num / count);
            answer.Add(divided);
            return answer.Concat(RemainderDivide(num - divided, count - 1)).ToList();
        }

        [Fact]
        public void MyTest()
        {
            Assert.Equal(justifiedLorem.Replace("\r", ""), Justify(lorem, 30));
            Assert.Equal("", Justify("", 30));
        }

        private string justifiedLorem = @"Lorem  ipsum  dolor  sit amet,
consectetur  adipiscing  elit.
Vestibulum    sagittis   dolor
mauris,  at  elementum  ligula
tempor  eget.  In quis rhoncus
nunc,  at  aliquet orci. Fusce
at   dolor   sit   amet  felis
suscipit   tristique.   Nam  a
imperdiet   tellus.  Nulla  eu
vestibulum    urna.    Vivamus
tincidunt  suscipit  enim, nec
ultrices   nisi  volutpat  ac.
Maecenas   sit   amet  lacinia
arcu,  non dictum justo. Donec
sed  quam  vel  risus faucibus
euismod.  Suspendisse  rhoncus
rhoncus  felis  at  fermentum.
Donec lorem magna, ultricies a
nunc    sit    amet,   blandit
fringilla  nunc. In vestibulum
velit    ac    felis   rhoncus
pellentesque. Mauris at tellus
enim.  Aliquam eleifend tempus
dapibus. Pellentesque commodo,
nisi    sit   amet   hendrerit
fringilla,   ante  odio  porta
lacus,   ut   elementum  justo
nulla et dolor.";

        private string lorem =
            @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum sagittis dolor mauris, at elementum ligula tempor eget. In quis rhoncus nunc, at aliquet orci. Fusce at dolor sit amet felis suscipit tristique. Nam a imperdiet tellus. Nulla eu vestibulum urna. Vivamus tincidunt suscipit enim, nec ultrices nisi volutpat ac. Maecenas sit amet lacinia arcu, non dictum justo. Donec sed quam vel risus faucibus euismod. Suspendisse rhoncus rhoncus felis at fermentum. Donec lorem magna, ultricies a nunc sit amet, blandit fringilla nunc. In vestibulum velit ac felis rhoncus pellentesque. Mauris at tellus enim. Aliquam eleifend tempus dapibus. Pellentesque commodo, nisi sit amet hendrerit fringilla, ante odio porta lacus, ut elementum justo nulla et dolor.";
    }
}
